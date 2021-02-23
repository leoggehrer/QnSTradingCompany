//@QnSGeneratedCode
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
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
        protected override Task BeforeFirstRenderAsync()
        {
            var handled = false;
            BeforeFirstRender(ref handled);
            if (handled == false)
            {
                AdapterAccess = ServiceAdapter.Create<TContract>();
                DataGridHandler = new IdentityXRoleDataGridHandler(this, AdapterAccess);
                DataGridHandler.PageSize = Settings.GetValueTyped<int>($"{PageName}.{nameof(DataGridHandler.PageSize)}", DataGridHandler.PageSize);
            }
            AfterFirstRender();
            return base.BeforeFirstRenderAsync();
        }
        partial void BeforeFirstRender(ref bool handled);
        partial void AfterFirstRender();
    }
}