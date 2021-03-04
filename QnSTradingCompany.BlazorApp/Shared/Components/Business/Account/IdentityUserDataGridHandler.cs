//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Business.Account.IIdentityUser;
using TModel = QnSTradingCompany.BlazorApp.Models.Business.Account.IdentityUser;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Business.Account
{
    public partial class IdentityUserDataGridHandler : Modules.DataGrid.DataGridHandler<TContract, TModel>
    {
        public IdentityUserDataGridHandler(Pages.ModelPage modelPage) : base(modelPage)
        {
        }
    }
}
