//@QnSCodeCopy
//MdStart

using CommonBase.Extensions;
using QnSTradingCompany.BlazorApp.Models.Modules.Account;
using QnSTradingCompany.BlazorApp.Models.Modules.Session;
using QnSTradingCompany.BlazorApp.Modules.SessionStorage;
using System;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Services.Modules.Authentication
{
    public class AccountService : Adapters.Modules.Account.AccountManager, IAccountService, IDisposable
    {
        private const int checkTimeoutDelay = 60000;
        private volatile bool checkForTimeoutRun = false;
        private AuthorizationSession currentAuthorizationSession;

        public event EventHandler<AuthorizationSession> AuthorizationChanged;
        public AuthorizationSession CurrentAuthorizationSession
        {
            get => currentAuthorizationSession;
            private set
            {
                if (currentAuthorizationSession != value)
                {
                    currentAuthorizationSession = value;
                    AuthorizationChanged?.Invoke(this, value);
                }
            }
        }
        protected IProtectedBrowserStorage ProtectedBrowserStorage { get; set; }
        public AccountService(IProtectedBrowserStorage protectedBrowserStorage)
        {
            ProtectedBrowserStorage = protectedBrowserStorage;
        }
        private bool hasInitialized = false;
        public async void InitAuthorizationSession()
        {
            if (hasInitialized == false)
            {
                hasInitialized = true;
                try
                {
                    var checkSession = await ProtectedBrowserStorage.GetAsync<AuthorizationSession>(StaticLiterals.AuthorizationSessionKey).ConfigureAwait(false);

                    if (checkSession != null)
                    {
                        var alive = await IsSessionAliveAsync(checkSession.Token).ConfigureAwait(false);

                        if (alive)
                        {
                            CurrentAuthorizationSession = checkSession;
                        }
                        else
                        {
                            await ProtectedBrowserStorage.DeleteAsync(StaticLiterals.AuthorizationSessionKey).ConfigureAwait(false);
                            CurrentAuthorizationSession = null;
                        }
                    }
                    CheckAuthorizationSessionForTimeout();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        protected void CheckAuthorizationSessionForTimeout()
        {
            if (checkForTimeoutRun == false)
            {
                checkForTimeoutRun = true;
                Task.Run(async () =>
                {
                    while (checkForTimeoutRun)
                    {
                        await Task.Delay(checkTimeoutDelay).ConfigureAwait(false);

                        try
                        {
                            var checkSession = await ProtectedBrowserStorage.GetAsync<AuthorizationSession>(StaticLiterals.AuthorizationSessionKey).ConfigureAwait(false);

                            if (checkForTimeoutRun && checkSession != null)
                            {
                                var alive = await IsSessionAliveAsync(checkSession.Token).ConfigureAwait(false);

                                if (alive == false)
                                {
                                    await ProtectedBrowserStorage.DeleteAsync(StaticLiterals.AuthorizationSessionKey).ConfigureAwait(false);
                                    CurrentAuthorizationSession = null;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                    checkForTimeoutRun = false;
                });
            }
        }

        public async Task<AuthorizationSession> LogonAsync(LoginModel loginModel)
        {
            try
            {
                var login = await LogonAsync(loginModel.Email, loginModel.Password, loginModel.OptionalInfo).ConfigureAwait(false);
                var result = new AuthorizationSession()
                {
                    IdentityId = login.IdentityId,
                    Token = login.SessionToken,
                    Username = login.Name,
                    Roles = await QueryRolesAsync(login.SessionToken).ConfigureAwait(false),
                    Email = login.Email,
                    FirstName = login.Name,
                    LastName = login.Name,
                    LoginTime = login.LoginTime
                };
                await ProtectedBrowserStorage.SetAsync(StaticLiterals.AuthorizationSessionKey, result).ConfigureAwait(false);
                CurrentAuthorizationSession = result;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ChangePasswordAsync(string oldPassword, string newPassword)
        {
            try
            {
                var authSession = await ProtectedBrowserStorage.GetAsync<AuthorizationSession>(StaticLiterals.AuthorizationSessionKey).ConfigureAwait(false);

                if (authSession != null && string.IsNullOrEmpty(authSession.Token) == false)
                {
                    await ChangePasswordAsync(authSession.Token, oldPassword, newPassword).ConfigureAwait(false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task ChangePasswordForAsync(string email, string newPassword)
        {
            try
            {
                var authorizationSession = await ProtectedBrowserStorage.GetAsync<AuthorizationSession>(StaticLiterals.AuthorizationSessionKey).ConfigureAwait(false);

                if (authorizationSession != null && string.IsNullOrEmpty(authorizationSession.Token) == false)
                {
                    await ChangePasswordForAsync(authorizationSession.Token, email, newPassword).ConfigureAwait(false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                var authorizationSession = await ProtectedBrowserStorage.GetAsync<AuthorizationSession>(StaticLiterals.AuthorizationSessionKey).ConfigureAwait(false);

                if (authorizationSession?.Token != null)
                {
                    await LogoutAsync(authorizationSession.Token).ConfigureAwait(false);
                }
                CurrentAuthorizationSession = null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{System.Reflection.MethodBase.GetCurrentMethod().GetAsyncOriginal()}: ", ex.Message);
            }
            await ProtectedBrowserStorage.DeleteAsync(StaticLiterals.AuthorizationSessionKey).ConfigureAwait(false);
        }

        #region Dispose pattern
        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    checkForTimeoutRun = false;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~AccountService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion Dispose pattern
    }
}
//MdEnd
