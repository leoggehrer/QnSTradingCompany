//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Linq;
using System.Threading.Tasks;
using TContract = QnSTradingCompany.Contracts.Persistence.Account.IActionLog;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Account.ActionLog;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    partial class ActionLogDataGrid
    {
        [Parameter]
        public ActionLogDataGridHandler DataGridHandler
        {
            get;
            set;
        }
        public override string ForPrefix => "ActionLog";
    }
}
