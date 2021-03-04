//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Persistence.MasterData.IProduct;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.MasterData.Product;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.MasterData
{
    public partial class ProductDataGridHandler : Modules.DataGrid.DataGridHandler<TContract, TModel>
    {
        public ProductDataGridHandler(Pages.ModelPage modelPage) : base(modelPage)
        {
        }
    }
}
