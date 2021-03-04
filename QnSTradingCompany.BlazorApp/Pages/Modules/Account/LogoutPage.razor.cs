//@QnSCodeCopy
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Pages.Modules.Account
{
    partial class LogoutPage
    {
        protected override async Task OnFirstRenderAsync()
        {
            await base.OnFirstRenderAsync().ConfigureAwait(false);

            await AccountService.LogoutAsync().ConfigureAwait(false);
            await ClearPageHistoryAsync().ConfigureAwait(false);
            await ClearPageBeforeLoginAsync().ConfigureAwait(false);
            NavigationManager.NavigateTo("/");
        }
    }
}
