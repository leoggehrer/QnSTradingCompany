//@QnSCodeCopy
//MdStart
using QnSTradingCompany.Contracts.Modules.Common;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationImporter.ConApp
{
    class Program
    {
        private static bool runProgress = false;
        private static string SaEmail => "SysAdmin.QnSTradingCompany@gmx.at";
        private static string SaPwd => "Sys2189!Admin";
        private static string TranslationFileName => "Translations.csv";
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Running {nameof(ConfigurationImporter)}");

            StartProgress("Import properties and tanslations");

            await ImportProperties("Properties.csv", SaEmail, SaPwd).ConfigureAwait(false);
            await ImportTranslations("Translations.csv", SaEmail, SaPwd).ConfigureAwait(false);

            StopProgress();
            Console.WriteLine();
        }

        private record Translation(string AppName, string KeyLanguage, string Key, string ValueLanguage, string Value);
        private static Translation ToTranslation(string line, string separator)
        {
            var data = line.Split(separator);
            return new Translation(data[0], data[1], data[2], data[3], data[4]);
        }
        static async Task ImportTranslations(string filePath, string user, string password)
        {
            QnSTradingCompany.Adapters.Factory.Adapter = QnSTradingCompany.Adapters.AdapterType.Controller;

            var accMngr = new QnSTradingCompany.Adapters.Modules.Account.AccountManager();
            var login = await accMngr.LogonAsync(user, password);
            using var ctrl = QnSTradingCompany.Adapters.Factory.Create<QnSTradingCompany.Contracts.Persistence.Language.ITranslation>(login.SessionToken);
            var importTranslations = File.ReadAllLines(filePath, Encoding.Default).Skip(1).Select(l => ToTranslation(l, ";"));
            var existsTranslations = await ctrl.QueryAllAsync("AppName.Equals(\"QnSTradingCompany\")").ConfigureAwait(false);

            foreach (var item in importTranslations)
            {
                var keyLanguage = (LanguageCode)Enum.Parse(typeof(LanguageCode), item.KeyLanguage);
                var entry = existsTranslations.SingleOrDefault(e => e.KeyLanguage == keyLanguage && e.Key.Equals(item.Key));

                if (entry == null)
                {
                    var newEntry = await ctrl.CreateAsync().ConfigureAwait(false);

                    newEntry.AppName = item.AppName;
                    newEntry.KeyLanguage = (LanguageCode)Enum.Parse(typeof(LanguageCode), item.KeyLanguage);
                    newEntry.Key = item.Key;
                    newEntry.ValueLanguage = (LanguageCode)Enum.Parse(typeof(LanguageCode), item.ValueLanguage);
                    newEntry.Value = item.Value;
                    await ctrl.InsertAsync(newEntry).ConfigureAwait(false);
                }
                else
                {
                    entry.Value = item.Value;
                    await ctrl.UpdateAsync(entry).ConfigureAwait(false);
                }
            }
            await accMngr.LogoutAsync(login.SessionToken);
        }

        private record Property(string AppName, string ComponentName, string MemberName, string Attribute, string Value);
        private static Property ToProperty(string line, string separator)
        {
            var data = line.Split(separator);
            return new Property(data[0], data[1], data[2], data[3], data[4]);
        }
        static async Task ImportProperties(string filePath, string user, string password)
        {
            QnSTradingCompany.Adapters.Factory.Adapter = QnSTradingCompany.Adapters.AdapterType.Controller;

            var accMngr = new QnSTradingCompany.Adapters.Modules.Account.AccountManager();
            var login = await accMngr.LogonAsync(user, password);
            using var ctrl = QnSTradingCompany.Adapters.Factory.Create<QnSTradingCompany.Contracts.Persistence.Configuration.ISetting>(login.SessionToken);
            var importProperties = File.ReadAllLines(filePath, Encoding.Default).Skip(1).Select(l => ToProperty(l, ";"));
            var existsProperties = await ctrl.QueryAllAsync("AppName.Equals(\"QnSTradingCompany\")").ConfigureAwait(false);

            foreach (var item in importProperties)
            {
                var key = $"{item.ComponentName}.{item.MemberName}{(string.IsNullOrEmpty(item.Attribute) ? string.Empty : $".{item.Attribute}")}";
                var entry = existsProperties.SingleOrDefault(e => e.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));

                if (entry == null)
                {
                    var newEntry = await ctrl.CreateAsync().ConfigureAwait(false);

                    newEntry.AppName = item.AppName;
                    newEntry.Key = key;
                    newEntry.Value = item.Value;
                    await ctrl.InsertAsync(newEntry).ConfigureAwait(false);
                }
                else
                {
                    entry.Value = item.Value;
                    await ctrl.UpdateAsync(entry).ConfigureAwait(false);
                }
            }
            await accMngr.LogoutAsync(login.SessionToken);
        }

        private static void StopProgress() => runProgress = false;
        private static void StartProgress(string header)
        {
            Console.WriteLine();
            Console.WriteLine(header);
            runProgress = true;
            Task.Factory.StartNew(async () =>
            {
                while (runProgress)
                {
                    Console.Write(".");
                    await Task.Delay(150).ConfigureAwait(false);
                }
            });
            Console.WriteLine();
        }

    }
}
//MdEnd
