//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Account.LoginSession;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    partial class LoginSessionDataGridDetail
    {
        [Parameter]
        public LoginSessionDataGridHandler MasterDataGridHandler
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
        = "LoginSession";
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
