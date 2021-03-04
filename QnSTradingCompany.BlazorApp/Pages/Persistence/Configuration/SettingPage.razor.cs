//@QnSGeneratedCode
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Configuration;
using TContract = QnSTradingCompany.Contracts.Persistence.Configuration.ISetting;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Configuration.Setting;
namespace QnSTradingCompany.BlazorApp.Pages.Persistence.Configuration
{
    partial class SettingPage
    {
        [Inject]
        protected DialogService DialogService
        {
            get;
            private set;
        }
        protected SettingDataGridHandler DataGridHandler
        {
            get;
            private set;
        }
        protected Contracts.Client.IAdapterAccess<TContract> AdapterAccess
        {
            get;
            private set;
        }
        protected override Task OnFirstRenderAsync()
        {
            DataGridHandler = new SettingDataGridHandler(this);
            DataGridHandler.PageSize = Settings.GetValueTyped<int>($"{ComponentName}.{nameof(DataGridHandler.PageSize)}", DataGridHandler.PageSize);
            return base.OnFirstRenderAsync();
        }
    }
}
