//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Business.Account.IIdentityUser;
using TModel = QnSTradingCompany.BlazorApp.Models.Business.Account.IdentityUser;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Business.Account
{
    public partial class IdentityUserFieldSetHandler : FieldSetHandler<TContract, TModel>
    {
        public IdentityUserFieldSetHandler(Pages.ModelPage modelPage, Contracts.Client.IAdapterAccess<TContract> adapterAccess) : base(modelPage, adapterAccess)
        {
        }
    }
}
