//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Persistence.Account.IRole;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Account.Role;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    public partial class RoleDataGridHandler : Modules.DataGrid.DataGridHandler<TContract, TModel>
    {
        public RoleDataGridHandler(Pages.ModelPage modelPage, Contracts.Client.IAdapterAccess<TContract> adapterAccess) : base(modelPage, adapterAccess)
        {
        }
    }
}
