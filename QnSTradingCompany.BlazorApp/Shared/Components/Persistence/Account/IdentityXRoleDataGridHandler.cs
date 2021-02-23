//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Persistence.Account.IIdentityXRole;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Account.IdentityXRole;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    public partial class IdentityXRoleDataGridHandler : Modules.DataGrid.DataGridHandler<TContract, TModel>
    {
        public IdentityXRoleDataGridHandler(Pages.ModelPage modelPage, Contracts.Client.IAdapterAccess<TContract> adapterAccess) : base(modelPage, adapterAccess)
        {
        }
    }
}
