//@QnSGeneratedCode
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account;
using TContract = QnSTradingCompany.Contracts.Persistence.Account.IActionLog;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Account.ActionLog;
namespace QnSTradingCompany.BlazorApp.Pages.Persistence.Account
{
    partial class ActionLogPage
    {
        [Inject]
        protected DialogService DialogService
        {
            get;
            private set;
        }
        protected ActionLogDataGridHandler DataGridHandler
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
            DataGridHandler = new ActionLogDataGridHandler(this);
            DataGridHandler.PageSize = Settings.GetValueTyped<int>($"{ComponentName}.{nameof(DataGridHandler.PageSize)}", DataGridHandler.PageSize);
            return base.OnFirstRenderAsync();
        }
    }
}
