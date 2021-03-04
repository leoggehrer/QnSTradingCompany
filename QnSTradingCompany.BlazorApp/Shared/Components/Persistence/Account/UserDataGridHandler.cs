//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Persistence.Account.IUser;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Account.User;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    public partial class UserDataGridHandler : Modules.DataGrid.DataGridHandler<TContract, TModel>
    {
        public UserDataGridHandler(Pages.ModelPage modelPage) : base(modelPage)
        {
        }
    }
}
