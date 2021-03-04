//@QnSGeneratedCode
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account;
using TContract = QnSTradingCompany.Contracts.Persistence.Account.IIdentityXRole;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Account.IdentityXRole;
namespace QnSTradingCompany.BlazorApp.Pages.Persistence.Account
{
    partial class IdentityXRolePage
    {
        [Inject]
        protected DialogService DialogService
        {
            get;
            private set;
        }
        protected IdentityXRoleDataGridHandler DataGridHandler
        {
            get;
            private set;
        }
        protected Contracts.Client.IAdapterAccess<TContract> AdapterAccess
        {
            get;
            private set;
        }
        protected override Task OnFirstRenderAsync()
        {
            DataGridHandler = new IdentityXRoleDataGridHandler(this);
            DataGridHandler.PageSize = Settings.GetValueTyped<int>($"{ComponentName}.{nameof(DataGridHandler.PageSize)}", DataGridHandler.PageSize);
            return base.OnFirstRenderAsync();
        }
    }
}
