//@QnSGeneratedCode
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using QnSTradingCompany.BlazorApp.Shared.Components.Business.Account;
using TContract = QnSTradingCompany.Contracts.Business.Account.IAppAccess;
using TModel = QnSTradingCompany.BlazorApp.Models.Business.Account.AppAccess;
namespace QnSTradingCompany.BlazorApp.Pages.Business.Account
{
    partial class AppAccessPage
    {
        [Inject]
        protected DialogService DialogService
        {
            get;
            private set;
        }
        protected AppAccessDataGridHandler DataGridHandler
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
                DataGridHandler = new AppAccessDataGridHandler(this, AdapterAccess);
                DataGridHandler.PageSize = Settings.GetValueTyped<int>($"{PageName}.{nameof(DataGridHandler.PageSize)}", DataGridHandler.PageSize);
            }
            AfterFirstRender();
            return base.BeforeFirstRenderAsync();
        }
        partial void BeforeFirstRender(ref bool handled);
        partial void AfterFirstRender();
    }
}
