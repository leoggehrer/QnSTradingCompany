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
        public override string ForPrefix => "Identity";
        protected Pages.ModelPage ModelPage => MasterDataGridHandler.ModelPage;
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
