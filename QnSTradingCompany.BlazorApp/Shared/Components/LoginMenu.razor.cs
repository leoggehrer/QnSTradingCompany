//@QnSCodeCopy
using CommonBase.Extensions;
using Microsoft.AspNetCore.Components;
using QnSTradingCompany.BlazorApp.Models.Modules.Menu;
using QnSTradingCompany.BlazorApp.Models.Modules.Session;
using Radzen.Blazor;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class LoginMenu
    {
        private void CreateLogonMenu()
        {
            MenuItems.Clear();
            MenuItems.Add(new MenuItem
            {
                Text = TranslateFor("Logon"),
                Value = "login",
                Path = "login",
                Icon = "input",
            });
        }
        private void CreateAdminMenu()
        {
            MenuItems.Clear();
            MenuItems.Add(
                new MenuItem
                {
                    Text = TranslateFor("Access-Authorization"),
                    Value = "accessauth",
                    Path = "appaccess",
                    Icon = "line_weight",
                }
            );
            MenuItems.Add(
                new MenuItem
                {
                    Text = TranslateFor("Identity-User"),
                    Value = "identityuser",
                    Path = "user",
                    Icon = "line_weight",
                }
            );
            MenuItems.Add(
                new MenuItem
                {
                    Text = TranslateFor("Role-Management"),
                    Value = "rolemanagement",
                    Path = "role",
                    Icon = "line_weight",
                }
            );
            MenuItems.Add(
                new MenuItem
                {
                    Text = TranslateFor("Change password"),
                    Value = "changepwd",
                    Path = "changepassword",
                    Icon = "payment",
                }
            );
            MenuItems.Add(
                new MenuItem
                {
                    Text = TranslateFor("Reset password"),
                    Value = "changepwdfor",
                    Path = "changepasswordfor",
                    Icon = "payment",
                }
            );
            MenuItems.Add(
                new MenuItem
                {
                    Text = TranslateFor("Translation"),
                    Value = "translation",
                    Path = "translation",
                    Icon = "line_weight",
                }
            );
            MenuItems.Add(
                new MenuItem
                {
                    Text = TranslateFor("Settings"),
                    Value = "setting",
                    Path = "setting",
                    Icon = "line_weight",
                }
            );
            MenuItems.Add(
                new MenuItem
                {
                    Text = TranslateFor("Logout"),
                    Value = "logout",
                    Icon = "exit_to_app",
                }
            );
        }
        protected override void ResetMenu()
        {
            CreateLogonMenu();
        }
        protected override void CreateMenu()
        {
            base.CreateMenu();

            if (AuthorizationSession != null)
            {
                if (AuthorizationSession.HasRole("SysAdmin")
                    || AuthorizationSession.HasRole("AppAdmin"))
                {
                    CreateAdminMenu();
                }
            }
            else 
            {
                CreateLogonMenu();
            }
        }

        protected async Task LogoutAsync()
        {
            await AccountService.LogoutAsync().ConfigureAwait(false);
            await ClearPageHistoryAsync().ConfigureAwait(false);
            await ClearPageBeforeLoginAsync().ConfigureAwait(false);
            ResetMenu();
            NavigationManager.NavigateTo("/");
        }
        protected async Task ClearPageHistoryAsync()
        {
            var localSessionHistory = await ProtectedBrowserStore.GetAsync<SessionHistory>(StaticLiterals.SessionHistoryKey).ConfigureAwait(false);

            if (localSessionHistory != null)
            {
                localSessionHistory.ClearHistory();
                await ProtectedBrowserStore.SetAsync(StaticLiterals.SessionHistoryKey, localSessionHistory).ConfigureAwait(false);
            }
        }
        protected async Task ClearPageBeforeLoginAsync()
        {
            await ProtectedBrowserStore.SetAsync(StaticLiterals.BeforeLoginPageKey, string.Empty).ConfigureAwait(false);
        }
        private Task OnClickProfileMenuAsync(RadzenProfileMenuItem item)
        {
            if (item.Value.AreEquals("logout"))
            {
                return LogoutAsync();
            }
            return Task.FromResult(0);
        }
    }
}
