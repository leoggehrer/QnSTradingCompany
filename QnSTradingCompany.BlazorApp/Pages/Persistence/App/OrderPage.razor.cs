//@QnSGeneratedCode
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using QnSTradingCompany.BlazorApp.Shared.Components.Persistence.App;
using TContract = QnSTradingCompany.Contracts.Persistence.App.IOrder;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.App.Order;
namespace QnSTradingCompany.BlazorApp.Pages.Persistence.App
{
    partial class OrderPage
    {
        [Inject]
        protected DialogService DialogService
        {
            get;
            private set;
        }
        protected OrderDataGridHandler DataGridHandler
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
            DataGridHandler = new OrderDataGridHandler(this);
            DataGridHandler.PageSize = Settings.GetValueTyped<int>($"{ComponentName}.{nameof(DataGridHandler.PageSize)}", DataGridHandler.PageSize);
            return base.OnFirstRenderAsync();
        }
    }
}
