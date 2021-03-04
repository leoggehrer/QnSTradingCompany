//@QnSGeneratedCode
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using QnSTradingCompany.BlazorApp.Shared.Components.Business.Account;
using TContract = QnSTradingCompany.Contracts.Business.Account.IAppAccess;
using TModel = QnSTradingCompany.BlazorApp.Models.Business.Account.AppAccess;
namespace QnSTradingCompany.BlazorApp.Pages.Business.Account
{
    partial class AppAccessPage
    {
        [Inject]
        protected DialogService DialogService
        {
            get;
            private set;
        }
        protected AppAccessDataGridHandler DataGridHandler
        {
            get;
            private set;
        }
        protected Contracts.Client.IAdapterAccess<TContract> AdapterAccess
        {
            get;
            private set;
        }
        protected override Task OnFirstRenderAsync()
        {
            DataGridHandler = new AppAccessDataGridHandler(this);
            DataGridHandler.PageSize = Settings.GetValueTyped<int>($"{ComponentName}.{nameof(DataGridHandler.PageSize)}", DataGridHandler.PageSize);
            return base.OnFirstRenderAsync();
        }
    }
}
