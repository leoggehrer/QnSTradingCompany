//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Persistence.Account.ILoginSession;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Account.LoginSession;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    public partial class LoginSessionDataGridHandler : Modules.DataGrid.DataGridHandler<TContract, TModel>
    {
        public LoginSessionDataGridHandler(Pages.ModelPage modelPage, Contracts.Client.IAdapterAccess<TContract> adapterAccess) : base(modelPage, adapterAccess)
        {
        }
    }
}
