//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Configuration.Setting;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Configuration
{
    partial class SettingDataGridDetail
    {
        [Parameter]
        public SettingDataGridHandler MasterDataGridHandler
        {
            get;
            set;
        }
        protected Pages.ModelPage ModelPage => MasterDataGridHandler.ModelPage;
        protected string TitleValue
        {
            get;
            set;
        }
        = "Setting";
        private TModel parentModel;
        protected TModel ParentModel
        {
            get
            {
                if (parentModel == null)
                {
                    parentModel = MasterDataGridHandler.ExpandModel;
                }
                return parentModel;
            }
        }
    }
}
