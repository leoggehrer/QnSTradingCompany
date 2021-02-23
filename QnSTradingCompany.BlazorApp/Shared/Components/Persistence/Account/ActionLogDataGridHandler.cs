//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Persistence.Account.IActionLog;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Account.ActionLog;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    public partial class ActionLogDataGridHandler : Modules.DataGrid.DataGridHandler<TContract, TModel>
    {
        public ActionLogDataGridHandler(Pages.ModelPage modelPage, Contracts.Client.IAdapterAccess<TContract> adapterAccess) : base(modelPage, adapterAccess)
        {
        }
    }
}
