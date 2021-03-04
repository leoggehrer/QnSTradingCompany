//@QnSCodeCopy
using Microsoft.AspNetCore.Components.Routing;
using QnSTradingCompany.BlazorApp.Models.Modules.Menu;
using QnSTradingCompany.BlazorApp.Models.Modules.Session;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class CommonMenu : CommonComponent
    {
        protected List<MenuItem> MenuItems { get; } = new List<MenuItem>();

        protected override async Task OnFirstRenderAsync()
        {
            await base.OnFirstRenderAsync().ConfigureAwait(false);

            if (AuthorizationSession != null)
            {
                CreateMenu();
            }
            else
            {
                ResetMenu();
            }
        }
        protected virtual void CreateMenu()
        {
        }
        protected virtual void ResetMenu()
        {
            MenuItems.Clear();
            MenuItems.Add(new MenuItem
            {
                Text = "Home",
                Value = "home",
                Icon = "home",
                Path = "/"
            });
            MenuItems.Add(new MenuItem
            {
                Text = TranslateFor("Login"),
                Value = "login",
                Icon = "login",
                Path = "login"
            });
        }

        protected override void HandleLocationChanged(object sender, LocationChangedEventArgs e)
        {
            base.HandleLocationChanged(sender, e);

            StateHasChanged();
        }
        protected override async void HandleAuthorizationChanged(object sender, AuthorizationSession e)
        {
            base.HandleAuthorizationChanged(sender, e);

            if (AuthorizationSession != null)
            {
                CreateMenu();
            }
            else
            {
                ResetMenu();
            }
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
        }
    }
}
