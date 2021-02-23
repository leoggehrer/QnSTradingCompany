//@QnSCodeCopy
//MdStart
using System;
using System.Collections.Generic;

namespace QnSTradingCompany.BlazorApp.Services.Modules.Configuration
{
    public interface ISettingService
    {
        bool ContainsKey(string key);
        string GetValue(string key, string defaultValue);
        T GetValueTyped<T>(string key, object defaultValue);

        void Reload();
        KeyValuePair<string, string>[] GetUnstoredSettings();

        KeyValuePair<string, string>[] QueryStoredSettings(Func<KeyValuePair<string, string>, bool> predicate);
        KeyValuePair<string, string>[] QueryUnstoredSettings(Func<KeyValuePair<string, string>, bool> predicate);
    }
}
//MdEnd
