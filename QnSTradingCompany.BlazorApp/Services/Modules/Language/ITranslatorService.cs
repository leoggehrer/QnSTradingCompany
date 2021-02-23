//@QnSCodeCopy

using System.Collections.Generic;

namespace QnSTradingCompany.BlazorApp.Services.Modules.Language
{
    public interface ITranslatorService
    {
        void Reload();
        bool ContainsKey(string key);

        string Translate(string key);
        string Translate(string key, string defaultValue);

        IEnumerable<KeyValuePair<string, string>> GetUnstoredTranslations();
    }
}
