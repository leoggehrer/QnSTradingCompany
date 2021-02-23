//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Linq;
using System.Threading.Tasks;
using TContract = QnSTradingCompany.Contracts.Persistence.App.IOrder;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.App.Order;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.App
{
    partial class OrderDataGridColumns
    {
        [Parameter]
        public OrderDataGridHandler DataGridHandler
        {
            get;
            set;
        }
        protected override Task AfterFirstRenderAsync()
        {
            DataGridHandler.ModelItems = DataGridHandler.ModelItems.Union(GetAllDisplayProperties().Where(e => e.ScaffoldItem && e.Visible && e.IsModelItem).Select(e => e.PropertyName)).Distinct().ToArray();
            return base.AfterFirstRenderAsync();
        }
        protected override Type GetModelType()
        {
            var handled = false;
            var result = default(Type);
            BeforeGetModelType(ref result, ref handled);
            if (handled == false)
            {
                result = typeof(TModel);
            }
            AfterGetModelType(result);
            return result;
        }
        static partial void BeforeGetModelType(ref Type modelType, ref bool handled);
        static partial void AfterGetModelType(Type modelType);
    }
}
