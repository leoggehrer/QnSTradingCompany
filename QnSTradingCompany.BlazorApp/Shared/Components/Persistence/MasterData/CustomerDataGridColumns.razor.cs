//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Linq;
using System.Threading.Tasks;
using TContract = QnSTradingCompany.Contracts.Persistence.MasterData.ICustomer;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.MasterData.Customer;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.MasterData
{
    partial class CustomerDataGridColumns
    {
        [Parameter]
        public CustomerDataGridHandler DataGridHandler
        {
            get;
            set;
        }
        public override string ForPrefix => "Customer";
        protected override Task OnFirstRenderAsync()
        {
            DataGridHandler.ModelItems = DataGridHandler.ModelItems.Union(GetAllDisplayProperties().Where(e => e.ScaffoldItem && e.Visible && e.IsModelItem).Select(e => e.PropertyName)).Distinct().ToArray();
            return base.OnFirstRenderAsync();
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
