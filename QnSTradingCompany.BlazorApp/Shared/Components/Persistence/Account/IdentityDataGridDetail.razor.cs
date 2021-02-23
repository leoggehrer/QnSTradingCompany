//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Account.Identity;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    partial class IdentityDataGridDetail
    {
        [Parameter]
        public IdentityDataGridHandler MasterDataGridHandler
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
        = "Identity";
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
