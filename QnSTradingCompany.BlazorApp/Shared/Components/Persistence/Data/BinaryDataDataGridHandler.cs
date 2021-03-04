//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Persistence.Data.IBinaryData;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Data.BinaryData;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Data
{
    public partial class BinaryDataDataGridHandler : Modules.DataGrid.DataGridHandler<TContract, TModel>
    {
        public BinaryDataDataGridHandler(Pages.ModelPage modelPage) : base(modelPage)
        {
        }
    }
}
