//@QnSGeneratedCode
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using QnSTradingCompany.BlazorApp.Shared.Components.Persistence.App;
using TContract = QnSTradingCompany.Contracts.Persistence.App.ICondition;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.App.Condition;
namespace QnSTradingCompany.BlazorApp.Pages.Persistence.App
{
    partial class ConditionPage
    {
        [Inject]
        protected DialogService DialogService
        {
            get;
            private set;
        }
        protected ConditionDataGridHandler DataGridHandler
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
            DataGridHandler = new ConditionDataGridHandler(this);
            DataGridHandler.PageSize = Settings.GetValueTyped<int>($"{ComponentName}.{nameof(DataGridHandler.PageSize)}", DataGridHandler.PageSize);
            return base.OnFirstRenderAsync();
        }
    }
}
