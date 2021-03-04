//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Persistence.Language.ITranslation;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Language.Translation;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Language
{
    public partial class TranslationDataGridHandler : Modules.DataGrid.DataGridHandler<TContract, TModel>
    {
        public TranslationDataGridHandler(Pages.ModelPage modelPage) : base(modelPage)
        {
        }
    }
}
