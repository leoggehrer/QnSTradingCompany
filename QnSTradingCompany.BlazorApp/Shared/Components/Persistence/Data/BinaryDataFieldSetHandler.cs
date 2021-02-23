//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Persistence.Data.IBinaryData;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Data.BinaryData;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Data
{
    public partial class BinaryDataFieldSetHandler : FieldSetHandler<TContract, TModel>
    {
        public BinaryDataFieldSetHandler(Pages.ModelPage modelPage, Contracts.Client.IAdapterAccess<TContract> adapterAccess) : base(modelPage, adapterAccess)
        {
        }
    }
}
