//@QnSCodeCopy
//MdStart
using QnSTradingCompany.BlazorApp.Models.Modules.Account;
using QnSTradingCompany.BlazorApp.Models.Modules.Session;
using System;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Services.Modules.Authentication
{
    public interface IAccountService
    {
        event EventHandler<AuthorizationSession> AuthorizationChanged;
        AuthorizationSession CurrentAuthorizationSession { get; }

        void InitAuthorizationSession();

        Task<AuthorizationSession> LogonAsync(LoginModel loginModel);
        Task<bool> IsSessionAliveAsync(string sessionToken);
        Task ChangePasswordAsync(string oldPassword, string newPassword);
        Task ChangePasswordForAsync(string email, string newPassword);
        Task LogoutAsync();
    }
}
//MdEnd
