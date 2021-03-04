//@QnSGeneratedCode
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Language;
using TContract = QnSTradingCompany.Contracts.Persistence.Language.ITranslation;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Language.Translation;
namespace QnSTradingCompany.BlazorApp.Pages.Persistence.Language
{
    partial class TranslationPage
    {
        [Inject]
        protected DialogService DialogService
        {
            get;
            private set;
        }
        protected TranslationDataGridHandler DataGridHandler
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
            DataGridHandler = new TranslationDataGridHandler(this);
            DataGridHandler.PageSize = Settings.GetValueTyped<int>($"{ComponentName}.{nameof(DataGridHandler.PageSize)}", DataGridHandler.PageSize);
            return base.OnFirstRenderAsync();
        }
    }
}
