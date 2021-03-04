//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Persistence.App.ICondition;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.App.Condition;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.App
{
    public partial class ConditionDataGridHandler : Modules.DataGrid.DataGridHandler<TContract, TModel>
    {
        public ConditionDataGridHandler(Pages.ModelPage modelPage) : base(modelPage)
        {
        }
    }
}
