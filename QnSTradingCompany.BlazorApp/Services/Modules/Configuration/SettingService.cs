//@QnSCodeCopy

using QnSTradingCompany.BlazorApp.Services.Modules.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Services.Modules.Configuration
{
    public sealed partial class SettingService : ISettingService
    {
        private static string CreatePredicate() => $"AppName.Equals(\"{nameof(QnSTradingCompany)}\")";

        private IAccountService AccountService { get; init; }
        private IServiceAdapter ServiceAdapter { get; init; }

        private Dictionary<string, string> storedEntries = null;
        private Dictionary<string, string> StoredEntries
        {
            get
            {
                if (storedEntries == null)
                {
                    Init();
                }
                return storedEntries ?? new Dictionary<string, string>();
            }
        }
        private Dictionary<string, string> UnstoredEntries { get; } = new Dictionary<string, string>();

        public SettingService(IAccountService accountService, IServiceAdapter serviceAdapter)
        {
            AccountService = accountService;
            ServiceAdapter = serviceAdapter;
        }
        private void Init()
        {
            Task.Run(async () =>
            {
                var handled = false;

                BeginLoadData(ref handled);
                if (handled == false)
                {
                    await LoadDataAsync().ConfigureAwait(false);
                }
            }).Wait();
            EndLoadData();
        }
        partial void BeginLoadData(ref bool handled);
        partial void EndLoadData();

        public void Reload()
        {
            Task.Run(async () =>
            {
                var handled = false;

                BeginLoadData(ref handled);
                if (handled == false)
                {
                    await LoadDataAsync().ConfigureAwait(false);
                }
            }).Wait();
            EndLoadData();
        }
        private async Task LoadDataAsync()
        {
            try
            {
                var authorizationSession = AccountService.CurrentAuthorizationSession;

                if (authorizationSession != null)
                {
                    var dataAccess = ServiceAdapter.Create<Contracts.Persistence.Configuration.ISetting>(authorizationSession.Token);
                    var items = await dataAccess.QueryAllAsync(CreatePredicate()).ConfigureAwait(false);

                    storedEntries = new Dictionary<string, string>();
                    foreach (var item in items)
                    {
                        storedEntries.Add(item.Key, item.Value);
                        if (UnstoredEntries.ContainsKey(item.Key))
                        {
                            UnstoredEntries.Remove(item.Key);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {MethodBase.GetCurrentMethod().Name}: {ex.Message}");
            }
        }

        public bool ContainsKey(string key)
        {
            return StoredEntries.ContainsKey(key);
        }
        public string GetValue(string key, string defaultValue)
        {
            var hasItem = StoredEntries.TryGetValue(key, out string result);

            if (hasItem == false)
            {
                result = defaultValue;
                if (UnstoredEntries.ContainsKey(key) == false)
                {
                    UnstoredEntries.Add(key, defaultValue);
                }
            }
            return result;
        }
        public T GetValueTyped<T>(string key, object defaultValue)
        {
            var value = GetValue(key, defaultValue?.ToString());

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public KeyValuePair<string, string>[] GetUnstoredSettings()
        {
            return UnstoredEntries.ToArray();
        }
        public KeyValuePair<string, string>[] QueryStoredSettings(Func<KeyValuePair<string, string>, bool> predicate)
        {
            return StoredEntries.Where(predicate).ToArray();
        }
        public KeyValuePair<string, string>[] QueryUnstoredSettings(Func<KeyValuePair<string, string>, bool> predicate)
        {
            return UnstoredEntries.Where(predicate).ToArray();
        }
    }
}
