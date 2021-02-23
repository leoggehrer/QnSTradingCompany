//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Account.ActionLog;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    partial class ActionLogDataGridDetail
    {
        [Parameter]
        public ActionLogDataGridHandler MasterDataGridHandler
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
        = "ActionLog";
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
