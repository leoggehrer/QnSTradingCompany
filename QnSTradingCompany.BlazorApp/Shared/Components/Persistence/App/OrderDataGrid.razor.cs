//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Linq;
using System.Threading.Tasks;
using TContract = QnSTradingCompany.Contracts.Persistence.App.IOrder;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.App.Order;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.App
{
    partial class OrderDataGrid
    {
        [Parameter]
        public OrderDataGridHandler DataGridHandler
        {
            get;
            set;
        }
        public override string ForPrefix => "Order";
    }
}
