//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Linq;
using System.Threading.Tasks;
using TContract = QnSTradingCompany.Contracts.Persistence.Configuration.ISetting;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Configuration.Setting;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Configuration
{
    partial class SettingDataGrid
    {
        [Parameter]
        public SettingDataGridHandler DataGridHandler
        {
            get;
            set;
        }
        public override string ForPrefix => "Setting";
    }
}
