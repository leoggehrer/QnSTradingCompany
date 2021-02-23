using QnSTradingCompany.Contracts.Business.Account;
using System;
using System.Reflection;
using System.Threading.Tasks;
using AccountManager = QnSTradingCompany.Adapters.Modules.Account.AccountManager;

namespace QnSTradingCompany.ConApp
{
    // Search pattern for async calls without ConfigAwait: (?=\bawait\b(?!.*\bConfigureAwait\b))
    internal partial class Program
    {
        private static string SaUser => "SysAdmin";

        private static string SaEmail => "SysAdmin.QnSTradingCompany@gmx.at";

        private static string SaPwd => "Sys2189!Admin";

        private static string AaUser => "AppAdmin";

        private static string AaEmail => "AppAdmin.QnSTradingCompany@gmx.at";

        private static string AaPwd => "App2189!Admin";

        private static string AaRole => "AppAdmin";

        private static bool AaEnableJwt => true;

        private static async Task Main(string[] args)
        {
            await Task.Run(() => Console.WriteLine("QnSTradingCompany"));

            Console.WriteLine(DateTime.Now);
            BeforeExecuteMain(args);

            var rmAccountManager = new AccountManager
            {
                //                BaseUri = "http://localhost:5000/api",
                BaseUri = "http://localhost:5000/api",
                Adapter = Adapters.AdapterType.Service,
            };
            var appAccountManager = new AccountManager
            {
                BaseUri = "http://localhost:5000/api",
                Adapter = Adapters.AdapterType.Controller,
            };

            Adapters.Factory.BaseUri = "http://localhost:5000/api";
            Adapters.Factory.Adapter = Adapters.AdapterType.Controller;

            try
            {
                await InitAppAccessAsync().ConfigureAwait(false);
                await AddAppAccessAsync(AaUser, AaEmail, AaPwd, AaEnableJwt, AaRole).ConfigureAwait(false);

                //await appAccountManager.LogoutAsync(appLogin.SessionToken);
                //await rmAccountManager.LogoutAsync(rmLogin.SessionToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {MethodBase.GetCurrentMethod().Name}: {ex.Message}");
            }

            EndExecuteMain();
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Press any key to end!");
            Console.ReadKey();
        }
        private static async Task InitAppAccessAsync()
        {
            await Logic.Factory.CreateAccountManager().InitAppAccessAsync(SaUser, SaEmail, SaPwd, true).ConfigureAwait(false);
        }
        private static async Task<IAppAccess> AddAppAccessAsync(string user, string email, string pwd, bool enableJwtAuth, params string[] roles)
        {
            var accMngr = new AccountManager();
            var login = await accMngr.LogonAsync(SaEmail, SaPwd, string.Empty).ConfigureAwait(false);
            using var ctrl = Adapters.Factory.Create<IAppAccess>(login.SessionToken);
            var entity = await ctrl.CreateAsync();

            entity.OneItem.Name = user;
            entity.OneItem.Email = email;
            entity.OneItem.Password = pwd;
            entity.OneItem.EnableJwtAuth = enableJwtAuth;

            foreach (var item in roles)
            {
                var role = entity.CreateManyItem();

                role.Designation = item;
                entity.AddManyItem(role);
            }
            var identity = await ctrl.InsertAsync(entity).ConfigureAwait(false);
            await accMngr.LogoutAsync(login.SessionToken).ConfigureAwait(false);
            return identity;
        }

        static partial void BeforeExecuteMain(string[] args);
        static partial void EndExecuteMain();
    }
}
