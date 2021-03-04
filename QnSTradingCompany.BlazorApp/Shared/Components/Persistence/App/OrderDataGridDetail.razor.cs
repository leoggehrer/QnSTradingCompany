//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.App.Order;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.App
{
    partial class OrderDataGridDetail
    {
        [Parameter]
        public OrderDataGridHandler MasterDataGridHandler
        {
            get;
            set;
        }
        public override string ForPrefix => "Order";
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
