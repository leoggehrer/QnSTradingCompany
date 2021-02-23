//@QnSCodeCopy
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using QnSTradingCompany.BlazorApp.Services.Modules.Authentication;
using System;
using System.Threading.Tasks;
using TModel = QnSTradingCompany.BlazorApp.Models.Modules.Account.LoginModel;

namespace QnSTradingCompany.BlazorApp.Pages.Modules.Account
{
    partial class LoginPage
    {

        private TModel loginModel = null;
        TModel FormModel
        {
            get => loginModel ??= new TModel();
            set => loginModel = value;
        }
        private EditContext editContext = null;
        private EditContext EditContext
        {
            get => editContext ??= new EditContext(FormModel);
            set => editContext = value;
        }
        private string error = null;
        private string Error
        {
            get => error ??= string.Empty;
            set => error = value;
        }

        protected async Task OnSubmitAsync()
        {
            if (EditContext.Validate())
            {
                Error = string.Empty;
                try
                {
                    await AccountService.LogonAsync(FormModel).ConfigureAwait(false);
                    var toPage = await GetPageBeforeLoginAsync().ConfigureAwait(false);
                    await InvokeAsync((Action)(() =>
                    {
                        NavigationManager.NavigateTo(toPage);
                        StateHasChanged();
                    })).ConfigureAwait(false);
                    //await JSRuntime.InvokeVoidAsync("pageReload").ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Error = Translate(GetExceptionError(ex));
                }
            }
            else
            {
                await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
            }
        }
    }
}
