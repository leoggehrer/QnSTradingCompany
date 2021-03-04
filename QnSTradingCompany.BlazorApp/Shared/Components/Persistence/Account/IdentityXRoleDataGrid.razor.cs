//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Linq;
using System.Threading.Tasks;
using TContract = QnSTradingCompany.Contracts.Persistence.Account.IIdentityXRole;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Account.IdentityXRole;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    partial class IdentityXRoleDataGrid
    {
        [Parameter]
        public IdentityXRoleDataGridHandler DataGridHandler
        {
            get;
            set;
        }
        public override string ForPrefix => "IdentityXRole";
    }
}
