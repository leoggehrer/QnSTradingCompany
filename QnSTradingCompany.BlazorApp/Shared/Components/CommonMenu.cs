//@QnSCodeCopy
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using QnSTradingCompany.BlazorApp.Models.Modules.Menu;
using QnSTradingCompany.BlazorApp.Models.Modules.Session;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class CommonMenu : CommonComponent
    {
        [Inject]
        protected NavigationManager NavigationManager { get; private set; }

        protected List<MenuItem> MenuItems { get; } = new List<MenuItem>();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            NavigationManager.LocationChanged += NavigationManager_LocationChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(false);

            if (firstRender)
            {
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

        protected virtual void NavigationManager_LocationChanged(object sender, LocationChangedEventArgs e)
        {
            StateHasChanged();
        }
        protected override async void AccountService_AuthorizationChanged(object sender, AuthorizationSession e)
        {
            base.AccountService_AuthorizationChanged(sender, e);

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

        protected override void Dispose(bool disposing)
        {
            NavigationManager.LocationChanged -= NavigationManager_LocationChanged;

            base.Dispose(disposing);
        }
    }
}
