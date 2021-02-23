//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Persistence.Account.IUser;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Account.User;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    public partial class UserFieldSetHandler : FieldSetHandler<TContract, TModel>
    {
        public UserFieldSetHandler(Pages.ModelPage modelPage, Contracts.Client.IAdapterAccess<TContract> adapterAccess) : base(modelPage, adapterAccess)
        {
        }
    }
}
