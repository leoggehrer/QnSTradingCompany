//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Business.Account.IAppAccess;
using TModel = QnSTradingCompany.BlazorApp.Models.Business.Account.AppAccess;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Business.Account
{
    public partial class AppAccessDataGridHandler : Modules.DataGrid.DataGridHandler<TContract, TModel>
    {
        public AppAccessDataGridHandler(Pages.ModelPage modelPage) : base(modelPage)
        {
        }
    }
}
