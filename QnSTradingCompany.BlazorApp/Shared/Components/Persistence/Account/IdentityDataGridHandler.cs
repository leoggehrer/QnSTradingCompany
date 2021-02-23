//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Persistence.Account.IIdentity;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Account.Identity;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    public partial class IdentityDataGridHandler : Modules.DataGrid.DataGridHandler<TContract, TModel>
    {
        public IdentityDataGridHandler(Pages.ModelPage modelPage, Contracts.Client.IAdapterAccess<TContract> adapterAccess) : base(modelPage, adapterAccess)
        {
        }
    }
}
