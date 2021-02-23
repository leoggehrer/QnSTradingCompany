//@QnSCodeCopy
//MdStart
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Modules.SessionStorage
{
    // Open design question: should the data protection purpose be scoped to the
    // user? Otherwise user A could give one of their values to use B (intentionally
    // or via malicious actions from user B). However it's not wise to just embed
    // User.Identity.Name into the purpose string, because there are cases where the
    // value has to be retained across a login flow, such as in the Blazing Pizzas
    // case where a logged-out user builds an order then logs in.
    //
    // Perhaps GetAsync/SetAsync should accept an optional "scope" or "purpose" string
    // that, if given, gets combined with the purpose we'd auto-generate anyway.

    /// <summary>
    /// Provides mechanisms for storing and retrieving data in the browser storage.
    /// </summary>
    public abstract class ProtectedBrowserStorage : IProtectedBrowserStorage
    {
        private const string JsFunctionsPrefix = "protectedBrowserStorage";

        private readonly string _storeName;
        private readonly string _userSecretsId;
        private readonly IJSRuntime _jsRuntime;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly ConcurrentDictionary<string, IDataProtector> _cachedDataProtectorsByPurpose
            = new ConcurrentDictionary<string, IDataProtector>();

        // Stylistically, it doesn't matter at all what options we choose, since the values
        // will be opaque after data protection. All that matters is that some fixed set of
        // options exists and remains constant forever. We should choose whatever options
        // maximize the ability to round-trip .NET objects reliably.
        private readonly static JsonSerializerOptions SerializerOptions = new JsonSerializerOptions();
        private string CreateKey(string key) => $"{_userSecretsId}-{key}";
        private string CreatePurposeByKey(string key) => $"{_userSecretsId}:{_storeName}:{key}";

        /// <summary>
        /// Constructs an instance of <see cref="ProtectedBrowserStorage"/>.
        /// </summary>
        /// <param name="storeName">The name of the store in which the data should be stored.</param>
        /// <param name="jsRuntime">The <see cref="IJSRuntime"/>.</param>
        /// <param name="dataProtectionProvider">The <see cref="IDataProtectionProvider"/>.</param>
        protected ProtectedBrowserStorage(string storeName, IJSRuntime jsRuntime, IDataProtectionProvider dataProtectionProvider)
        {
            if (string.IsNullOrEmpty(storeName))
                throw new ArgumentException("The value cannot be null or empty", nameof(storeName));

            var assembly = typeof(Program).Assembly;
            var attribute = (UserSecretsIdAttribute)assembly.GetCustomAttributes(typeof(UserSecretsIdAttribute), true)[0];

            _userSecretsId = attribute.UserSecretsId;
            _storeName = storeName;
            _jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
            _dataProtectionProvider = dataProtectionProvider ?? throw new ArgumentNullException(nameof(dataProtectionProvider));
        }

        /// <summary>
        /// <para>
        /// Asynchronously stores the specified data.
        /// </para>
        /// <para>
        /// Since no data protection purpose is specified with this overload, the purpose is derived from <paramref name="key"/> and the store name. This is a good default purpose to use if the keys come from a fixed set known at compile-time.
        /// </para>
        /// </summary>
        /// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use.</param>
        /// <param name="value">A JSON-serializable value to be stored.</param>
        /// <returns>A <see cref="Task"/> representing the completion of the operation.</returns>
        public ValueTask SetAsync(string key, object value)
            => SetAsync(CreatePurposeByKey(key), key, value);

        /// <summary>
        /// Asynchronously stores the supplied data.
        /// </summary>
        /// <param name="purpose">A string that defines a scope for the data protection. The protected data can only be unprotected by code that specifies the same purpose.</param>
        /// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use.</param>
        /// <param name="value">A JSON-serializable value to be stored.</param>
        /// <returns>A <see cref="Task"/> representing the completion of the operation.</returns>
        public ValueTask SetAsync(string purpose, string key, object value)
        {
            if (string.IsNullOrEmpty(purpose))
                throw new ArgumentException("Cannot be null or empty", nameof(purpose));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Cannot be null or empty", nameof(key));

            var json = JsonSerializer.Serialize(value, options: SerializerOptions);
            var protector = GetOrCreateCachedProtector(purpose);
            var protectedJson = protector.Protect(json);
            return _jsRuntime.InvokeVoidAsync(
                $"{JsFunctionsPrefix}.set",
                _storeName,
                CreateKey(key),
                protectedJson);
        }

        /// <summary>
        /// <para>
        /// Asynchronously retrieves the specified data.
        /// </para>
        /// <para>
        /// Since no data protection purpose is specified with this overload, the purpose is derived from <paramref name="key"/> and the store name. This is a good default purpose to use if the keys come from a fixed set known at compile-time.
        /// </para>
        /// </summary>
        /// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use.</param>
        /// <returns>A <see cref="Task"/> representing the completion of the operation.</returns>
        public ValueTask<T> GetAsync<T>(string key)
            => GetAsync<T>(CreatePurposeByKey(key), key);

        /// <summary>
        /// Asynchronously retrieves the specified data.
        /// </summary>
        /// <param name="purpose">A string that defines a scope for the data protection. The protected data can only be unprotected if the same purpose was previously specified when calling <see cref="SetAsync(string, string, object)"/>.</param>
        /// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use.</param>
        /// <returns>A <see cref="Task"/> representing the completion of the operation.</returns>
        public async ValueTask<T> GetAsync<T>(string purpose, string key)
        {
            string protectedJson = null;
            try
            {
                protectedJson = await _jsRuntime.InvokeAsync<string>(
                    $"{JsFunctionsPrefix}.get",
                    _storeName,
                    CreateKey(key)).ConfigureAwait(false);

                // We should consider having both TryGetAsync and GetValueOrDefaultAsync
                // It should be possible to distinguish between the value 'null' being stored
                // for a given key versus no value being stored for that key. However we should
                // still data-protect the 'null' value so it's indistinguishable to the end
                // user from non-null values.
                if (protectedJson == null)
                {
                    return default;
                }

                var protector = GetOrCreateCachedProtector(purpose);
                var json = protector.Unprotect(protectedJson);
                return JsonSerializer.Deserialize<T>(json, options: SerializerOptions);
            }
            catch (CryptographicException e)
            {
                if (e.Message == "The payload was invalid.")
                {
                    await DeleteAsync(key).ConfigureAwait(false);
                }
            }
            return default;
        }

        /// <summary>
        /// Asynchronously deletes any data stored for the specified key.
        /// </summary>
        /// <param name="key">A <see cref="string"/> value specifying the name of the storage slot whose value should be deleted.</param>
        /// <returns>A <see cref="Task"/> representing the completion of the operation.</returns>
        public ValueTask DeleteAsync(string key)
        {
            return _jsRuntime.InvokeVoidAsync(
                $"{JsFunctionsPrefix}.delete",
                _storeName,
                CreateKey(key));
        }

        // IDataProtect isn't disposable, so we're fine holding these indefinitely.
        // Only a bounded number of them will be created, as the 'key' values should
        // come from a bounded set known at compile-time. There's no use case for
        // letting runtime data determine the 'key' values.
        private IDataProtector GetOrCreateCachedProtector(string purpose)
            => _cachedDataProtectorsByPurpose.GetOrAdd(
                purpose,
                _dataProtectionProvider.CreateProtector);
    }
}
//MdEnd
