//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Persistence.App.IOrder;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.App.Order;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.App
{
    public partial class OrderFieldSetHandler : FieldSetHandler<TContract, TModel>
    {
        public OrderFieldSetHandler(Pages.ModelPage modelPage, Contracts.Client.IAdapterAccess<TContract> adapterAccess) : base(modelPage, adapterAccess)
        {
        }
    }
}
