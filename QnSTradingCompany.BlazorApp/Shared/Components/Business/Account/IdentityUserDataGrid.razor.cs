//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Linq;
using System.Threading.Tasks;
using TContract = QnSTradingCompany.Contracts.Business.Account.IIdentityUser;
using TModel = QnSTradingCompany.BlazorApp.Models.Business.Account.IdentityUser;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Business.Account
{
    partial class IdentityUserDataGrid
    {
        [Parameter]
        public IdentityUserDataGridHandler DataGridHandler
        {
            get;
            set;
        }
        public override string ForPrefix => "IdentityUser";
    }
}
