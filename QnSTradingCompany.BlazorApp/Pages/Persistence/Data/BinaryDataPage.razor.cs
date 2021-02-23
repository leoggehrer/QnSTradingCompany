//@QnSGeneratedCode
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Data;
using TContract = QnSTradingCompany.Contracts.Persistence.Data.IBinaryData;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Data.BinaryData;
namespace QnSTradingCompany.BlazorApp.Pages.Persistence.Data
{
    partial class BinaryDataPage
    {
        [Inject]
        protected DialogService DialogService
        {
            get;
            private set;
        }
        protected BinaryDataDataGridHandler DataGridHandler
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
                DataGridHandler = new BinaryDataDataGridHandler(this, AdapterAccess);
                DataGridHandler.PageSize = Settings.GetValueTyped<int>($"{PageName}.{nameof(DataGridHandler.PageSize)}", DataGridHandler.PageSize);
            }
            AfterFirstRender();
            return base.BeforeFirstRenderAsync();
        }
        partial void BeforeFirstRender(ref bool handled);
        partial void AfterFirstRender();
    }
}
