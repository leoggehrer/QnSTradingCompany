//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Persistence.Account.ILoginSession;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Account.LoginSession;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    public partial class LoginSessionFieldSetHandler : FieldSetHandler<TContract, TModel>
    {
        public LoginSessionFieldSetHandler(Pages.ModelPage modelPage, Contracts.Client.IAdapterAccess<TContract> adapterAccess) : base(modelPage, adapterAccess)
        {
        }
    }
}
