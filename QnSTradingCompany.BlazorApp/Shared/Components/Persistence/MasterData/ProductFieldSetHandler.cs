//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Persistence.MasterData.IProduct;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.MasterData.Product;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.MasterData
{
    public partial class ProductFieldSetHandler : FieldSetHandler<TContract, TModel>
    {
        public ProductFieldSetHandler(Pages.ModelPage modelPage, Contracts.Client.IAdapterAccess<TContract> adapterAccess) : base(modelPage, adapterAccess)
        {
        }
    }
}
