//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Account.Role;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    partial class RoleDataGridDetail
    {
        [Parameter]
        public RoleDataGridHandler MasterDataGridHandler
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
        = "Role";
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
