//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Linq;
using System.Threading.Tasks;
using TContract = QnSTradingCompany.Contracts.Persistence.MasterData.ICustomer;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.MasterData.Customer;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.MasterData
{
    partial class CustomerDataGrid
    {
        [Parameter]
        public CustomerDataGridHandler DataGridHandler
        {
            get;
            set;
        }
    }
}
