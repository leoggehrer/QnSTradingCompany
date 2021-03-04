//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Linq;
using System.Threading.Tasks;
using TContract = QnSTradingCompany.Contracts.Persistence.Account.IRole;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Account.Role;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    partial class RoleDataGrid
    {
        [Parameter]
        public RoleDataGridHandler DataGridHandler
        {
            get;
            set;
        }
        public override string ForPrefix => "Role";
    }
}
