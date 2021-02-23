//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Persistence.MasterData.ICustomer;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.MasterData.Customer;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.MasterData
{
    public partial class CustomerFieldSetHandler : FieldSetHandler<TContract, TModel>
    {
        public CustomerFieldSetHandler(Pages.ModelPage modelPage, Contracts.Client.IAdapterAccess<TContract> adapterAccess) : base(modelPage, adapterAccess)
        {
        }
    }
}
