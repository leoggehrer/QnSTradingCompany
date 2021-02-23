//@QnSCodeCopy
using CommonBase.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using QnSTradingCompany.BlazorApp.Models.Modules.Session;
using QnSTradingCompany.BlazorApp.Modules.SessionStorage;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Services.Modules.Authentication
{
    public class AuthenticationStateProviderService : AuthenticationStateProvider
    {
        protected IAccountService AccountService { get; }
        protected IProtectedBrowserStorage ProtectedLocalStorage { get; }

        public AuthenticationStateProviderService(IAccountService accountService, IProtectedBrowserStorage protectedLocalStorage)
        {
            AccountService = accountService;
            ProtectedLocalStorage = protectedLocalStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            try
            {
                var authSession = await ProtectedLocalStorage.GetAsync<AuthorizationSession>(StaticLiterals.AuthorizationSessionKey).ConfigureAwait(false);

                if (authSession != null && string.IsNullOrEmpty(authSession.Token) == false)
                {
                    try
                    {
                        if (await AccountService.IsSessionAliveAsync(authSession.Token).ConfigureAwait(false))
                        {
                            var claims = new[]
                            {
                                new Claim(ClaimTypes.Name, authSession.Username),
                                new Claim(ClaimTypes.Email, authSession.Email)
                            }
                            .Concat(authSession.Roles.Select(r => new Claim(ClaimTypes.Role, r)));
                            identity = new ClaimsIdentity(claims, "QnS Authentication");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Request failed:" + ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{System.Reflection.MethodBase.GetCurrentMethod().GetAsyncOriginal()}: ", ex.Message);
            }
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
    }
}
