//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Linq;
using System.Threading.Tasks;
using TContract = QnSTradingCompany.Contracts.Persistence.Account.IUser;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Account.User;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    partial class UserDataGrid
    {
        [Parameter]
        public UserDataGridHandler DataGridHandler
        {
            get;
            set;
        }
    }
}
