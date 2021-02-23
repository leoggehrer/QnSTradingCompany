//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using QnSTradingCompany.BlazorApp.Models.Modules.Language;
using QnSTradingCompany.Contracts.Modules.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Services.Modules.Language
{
    public sealed partial class TranslatorService : ITranslatorService
    {
        private readonly Dictionary<string, TranslationEntry> storedEntries = new Dictionary<string, TranslationEntry>();
        private readonly Dictionary<string, TranslationEntry> unstoredEntries = new Dictionary<string, TranslationEntry>();

        public LanguageCode KeyLanguage { get; } = LanguageCode.En;
        public LanguageCode ValueLanguage { get; } = LanguageCode.De;
        private string CreatePredicate() => $"AppName.Equals(\"{nameof(QnSTradingCompany)}\") && {nameof(KeyLanguage)} == {(int)KeyLanguage} && {nameof(ValueLanguage)} == {(int)ValueLanguage}";

        private IServiceAdapter ServiceAdapter { get; init; }
        public TranslatorService(IServiceAdapter serviceAdapter)
        {
            ServiceAdapter = serviceAdapter;
            Init();
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
                var dataAccess = ServiceAdapter.Create<Contracts.Persistence.Language.ITranslation>();
                var items = await dataAccess.QueryAllAsync(CreatePredicate()).ConfigureAwait(false);

                storedEntries.Clear();
                foreach (var item in items)
                {
                    storedEntries.Add(item.Key, new TranslationEntry { Id = item.Id, Value = item.Value });
                    if (unstoredEntries.ContainsKey(item.Key))
                    {
                        unstoredEntries.Remove(item.Key);
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
            return storedEntries.ContainsKey(key);
        }
        public string Translate(string key)
        {
            return Translate(key, key);
        }
        public string Translate(string key, string defaultValue)
        {
            key.CheckArgument(nameof(key));

            var result = defaultValue;
            try
            {
                var hasTranslation = storedEntries.TryGetValue(key, out TranslationEntry entry);

                if (hasTranslation)
                {
                    result = entry.Value;
                }
                else if (unstoredEntries.ContainsKey(key) == false)
                {
                    int nextId = unstoredEntries.Any() ? unstoredEntries.Max(i => i.Value.Id) + 1 : 1;

                    unstoredEntries.Add(key, new TranslationEntry { Id = nextId, Value = defaultValue });
                }

                if (hasTranslation == false)
                {
                    var splitKey = key.Split(".");

                    if (splitKey.Length == 2)
                    {
                        var keyOne = $"{splitKey[0]}.";

                        if (storedEntries.TryGetValue(keyOne, out entry))
                        {
                            var newKey = $"{entry.Value}{splitKey[1]}";
                            hasTranslation = storedEntries.TryGetValue(newKey, out entry);
                            if (hasTranslation)
                            {
                                result = entry.Value;
                            }
                            else
                            {
                                result = newKey;
                            }
                        }
                        if (hasTranslation == false)
                        {
                            hasTranslation = storedEntries.TryGetValue(splitKey[1], out entry);
                            if (hasTranslation)
                            {
                                result = entry.Value;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public IEnumerable<KeyValuePair<string, string>> GetUnstoredTranslations()
        {
            return unstoredEntries.Select(e => new KeyValuePair<string, string>(e.Key, e.Value.Value));
        }
    }
}
//MdEnd
