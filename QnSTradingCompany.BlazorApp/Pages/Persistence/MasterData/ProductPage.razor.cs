//@QnSGeneratedCode
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using QnSTradingCompany.BlazorApp.Shared.Components.Persistence.MasterData;
using TContract = QnSTradingCompany.Contracts.Persistence.MasterData.IProduct;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.MasterData.Product;
namespace QnSTradingCompany.BlazorApp.Pages.Persistence.MasterData
{
    partial class ProductPage
    {
        [Inject]
        protected DialogService DialogService
        {
            get;
            private set;
        }
        protected ProductDataGridHandler DataGridHandler
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
            DataGridHandler = new ProductDataGridHandler(this);
            DataGridHandler.PageSize = Settings.GetValueTyped<int>($"{ComponentName}.{nameof(DataGridHandler.PageSize)}", DataGridHandler.PageSize);
            return base.OnFirstRenderAsync();
        }
    }
}
