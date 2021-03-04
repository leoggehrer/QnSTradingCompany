//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Persistence.Configuration.ISetting;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Configuration.Setting;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Configuration
{
    public partial class SettingDataGridHandler : Modules.DataGrid.DataGridHandler<TContract, TModel>
    {
        public SettingDataGridHandler(Pages.ModelPage modelPage) : base(modelPage)
        {
        }
    }
}
