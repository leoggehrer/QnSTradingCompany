//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Linq;
using System.Threading.Tasks;
using TContract = QnSTradingCompany.Contracts.Persistence.Account.IIdentity;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Account.Identity;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    partial class IdentityDataGrid
    {
        [Parameter]
        public IdentityDataGridHandler DataGridHandler
        {
            get;
            set;
        }
    }
}
