//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using QnSTradingCompany.AspMvc.Models.Modules.Language;
using QnSTradingCompany.Contracts.Modules.Common;
using QnSTradingCompany.Contracts.Persistence.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AccountManger = QnSTradingCompany.Adapters.Modules.Account.AccountManager;

namespace QnSTradingCompany.AspMvc.Modules.Language
{
    public partial class Translator
    {
        private static readonly Dictionary<string, TranslationEntry> storedTranslations = new Dictionary<string, TranslationEntry>();
        private static readonly Dictionary<string, TranslationEntry> unstoredTranslations = new Dictionary<string, TranslationEntry>();
        static Translator()
        {
            ClassConstructing();
            Init();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        private static string BaseUri { get; set; } = string.Empty;
        private static string IdentityUri { get; set; } = string.Empty;
        private static string Email { get; set; } = string.Empty;
        private static string Password { get; set; } = string.Empty;

        public static LanguageCode KeyLanguage { get; } = LanguageCode.En;
        public static LanguageCode ValueLanguage { get; } = LanguageCode.De;

        public static IEnumerable<KeyValuePair<string, TranslationEntry>> StoredTranslations => storedTranslations;
        public static IEnumerable<KeyValuePair<string, TranslationEntry>> UnstoredTranslations = unstoredTranslations;
        private static string CreatePredicate() => $"AppName.Equals(\"{nameof(QnSTradingCompany)}\") && {nameof(KeyLanguage)} == {(int)KeyLanguage} && {nameof(ValueLanguage)} == {(int)ValueLanguage}";

        public static void Init()
        {
            Task.Run(async () =>
            {
                var handled = false;
                var settingValue = Logic.Modules.Configuration.Settings.Get("Translator:BaseUri");
                if (settingValue.HasContent())
                {
                    BaseUri = settingValue;
                }
                settingValue = Logic.Modules.Configuration.Settings.Get("Translator:IdentityUri");
                if (settingValue.HasContent())
                {
                    IdentityUri = settingValue;
                }
                settingValue = Logic.Modules.Configuration.Settings.Get("Translator:Email");
                if (settingValue.HasContent())
                {
                    Email = settingValue;
                }
                settingValue = Logic.Modules.Configuration.Settings.Get("Translator:Password");
                if (settingValue.HasContent())
                {
                    Password = settingValue;
                }

                BeginLoadTranslations(ref handled);
                if (handled == false)
                {
                    await LoadTranslationsAsync().ConfigureAwait(false);
                }
            }).Wait();
            EndLoadTranslations();
        }

        private static async Task LoadTranslationsAsync()
        {
            try
            {
                Adapters.Connector.BaseUri = BaseUri;
                var predicate = CreatePredicate();
                var connector = Adapters.Connector.Create<Contracts.Modules.Language.ITranslation, Translation>();
                var query = await connector.QueryAllAsync(predicate).ConfigureAwait(false);

                storedTranslations.Clear();
                foreach (var item in query)
                {
                    storedTranslations.Add(item.Key, new TranslationEntry { Id = item.Id, Value = item.Value });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {MethodBase.GetCurrentMethod().Name}: {ex.Message}");
            }
        }
        static partial void BeginLoadTranslations(ref bool handled);
        static partial void EndLoadTranslations();

        public static string Translate(string key)
        {
            return Translate(key, key);
        }
        public static string Translate(string key, string defaultValue)
        {
            key.CheckArgument(nameof(key));

            var result = defaultValue;
            var hasTranslation = storedTranslations.TryGetValue(key, out TranslationEntry traVal);

            if (hasTranslation)
            {
                result = traVal.Value;
            }
            else if (unstoredTranslations.ContainsKey(key) == false)
            {
                int nextId = unstoredTranslations.Any() ? unstoredTranslations.Max(i => i.Value.Id) + 1 : 1;

                unstoredTranslations.Add(key, new TranslationEntry { Id = nextId, Value = defaultValue });
            }

            if (hasTranslation == false)
            {
                var splitKey = key.Split(".");

                if (splitKey.Length == 2)
                {
                    if (storedTranslations.TryGetValue(splitKey[1], out traVal))
                    {
                        result = traVal.Value;
                    }
                }
            }
            return result;
        }

        public static async Task<ILoginSession> TryLogonAsync(string identityUri, string email, string password)
        {
            var trAccMngr = new AccountManger
            {
                BaseUri = BaseUri,
                Adapter = Adapters.AdapterType.Service,
            };
            ILoginSession tryLogin;

            if (identityUri.HasContent())
            {
                var idAccMngr = new AccountManger
                {
                    BaseUri = identityUri,
                    Adapter = Adapters.AdapterType.Service,
                };
                var idLogin = await idAccMngr.LogonAsync(email, password).ConfigureAwait(false);

                tryLogin = await trAccMngr.LogonAsync(idLogin.JsonWebToken).ConfigureAwait(false);
                await idAccMngr.LogoutAsync(idLogin.SessionToken).ConfigureAwait(false);
            }
            else
            {
                tryLogin = await trAccMngr.LogonAsync(email, password).ConfigureAwait(false);
            }
            return tryLogin;
        }
        public static Task LogoutAsync(ILoginSession login)
        {
            login.CheckArgument(nameof(login));

            var trAccMngr = new AccountManger
            {
                BaseUri = BaseUri,
                Adapter = Adapters.AdapterType.Service,
            };
            return trAccMngr.LogoutAsync(login.SessionToken);
        }

        public static async Task UpdateTranslationsAsync(IEnumerable<KeyValuePair<string, TranslationEntry>> keyValuePairs)
        {
            keyValuePairs.CheckArgument(nameof(keyValuePairs));

            var handled = false;

            BeginUpdateTranslations(keyValuePairs, ref handled);
            if (handled == false)
            {
                foreach (var item in keyValuePairs)
                {
                    if (item.Key.HasContent()
                        && item.Value != null
                        && item.Value.Id > 0)
                    {
                        var query = storedTranslations.Where(e => e.Value.Id == item.Value.Id);

                        if (query.Any())
                        {
                            var translation = query.ElementAt(0);

                            var changed = translation.Key.Equals(item.Key) == false
                                          || translation.Value.Value.Equals(item.Value.Value) == false;

                            storedTranslations.Remove(translation.Key);
                            storedTranslations.Add(item.Key, new TranslationEntry { Changed = changed, Id = item.Value.Id, Value = item.Value.Value });
                        }
                    }
                }
                // Update exits translations
                Adapters.Connector.BaseUri = BaseUri;
                var login = default(ILoginSession);
                try
                {
                    login = await TryLogonAsync(IdentityUri, Email, Password).ConfigureAwait(false);
                    var connector = Adapters.Connector.Create<Contracts.Modules.Language.ITranslation, Translation>(login.SessionToken);

                    foreach (var item in storedTranslations.Where(i => i.Value.Changed))
                    {
                        var entity = await connector.GetByIdAsync(item.Value.Id).ConfigureAwait(false);

                        if (entity != null)
                        {
                            entity.Key = item.Key;
                            entity.Value = item.Value.Value;
                            await connector.UpdateAsync(entity).ConfigureAwait(false);
                        }
                    }
                    await LoadTranslationsAsync().ConfigureAwait(false);
                }
                finally
                {
                    if (login != null)
                        await LogoutAsync(login).ConfigureAwait(false);
                }
            }
            EndUpdateTranslations();
        }
        static partial void BeginUpdateTranslations(IEnumerable<KeyValuePair<string, TranslationEntry>> keyValuePairs, ref bool handled);
        static partial void EndUpdateTranslations();

        public static async Task StoreTranslationsAsync(IEnumerable<KeyValuePair<string, TranslationEntry>> keyValuePairs)
        {
            keyValuePairs.CheckArgument(nameof(keyValuePairs));

            var handled = false;

            BeginStoreTranslations(keyValuePairs, ref handled);
            if (handled == false)
            {
                // Insert missing translations
                Adapters.Connector.BaseUri = BaseUri;
                var login = default(ILoginSession);
                try
                {
                    login = await TryLogonAsync(IdentityUri, Email, Password).ConfigureAwait(false);
                    var connector = Adapters.Connector.Create<Contracts.Modules.Language.ITranslation, Translation>(login.SessionToken);

                    foreach (var item in keyValuePairs.Where(i => i.Key.HasContent()))
                    {
                        var entity = await connector.CreateAsync().ConfigureAwait(false);

                        if (entity != null)
                        {
                            entity.AppName = nameof(QnSTradingCompany);
                            entity.KeyLanguage = KeyLanguage;
                            entity.Key = item.Key;
                            entity.ValueLanguage = ValueLanguage;
                            entity.Value = item.Value.Value;
                            await connector.InsertAsync(entity).ConfigureAwait(false);

                            var entry = unstoredTranslations.SingleOrDefault(i => i.Value.Id == item.Value.Id);

                            if (entry.Value.Id == item.Value.Id)
                            {
                                unstoredTranslations.Remove(entry.Key);
                            }
                        }
                    }
                    await LoadTranslationsAsync().ConfigureAwait(false);
                }
                finally
                {
                    if (login != null)
                        await LogoutAsync(login).ConfigureAwait(false);
                }
            }
            EndStoreTranslations();
        }
        static partial void BeginStoreTranslations(IEnumerable<KeyValuePair<string, TranslationEntry>> keyValuePairs, ref bool handled);
        static partial void EndStoreTranslations();
    }
}
//MdEnd
