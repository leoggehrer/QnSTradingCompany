//@QnSCodeCopy
using Microsoft.AspNetCore.Components;
using QnSTradingCompany.BlazorApp.Services.Modules.Authentication;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Pages.Modules.Account
{
    partial class LogoutPage
    {
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(false);

            await AccountService.LogoutAsync().ConfigureAwait(false);
            await ClearPageHistoryAsync().ConfigureAwait(false);
            await ClearPageBeforeLoginAsync().ConfigureAwait(false);
            NavigationManager.NavigateTo("/");
        }
    }
}
