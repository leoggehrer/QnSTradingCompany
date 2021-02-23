//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Business.Account.IAppAccess;
using TModel = QnSTradingCompany.BlazorApp.Models.Business.Account.AppAccess;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Business.Account
{
    public partial class AppAccessFieldSetHandler : FieldSetHandler<TContract, TModel>
    {
        public AppAccessFieldSetHandler(Pages.ModelPage modelPage, Contracts.Client.IAdapterAccess<TContract> adapterAccess) : base(modelPage, adapterAccess)
        {
        }
    }
}
