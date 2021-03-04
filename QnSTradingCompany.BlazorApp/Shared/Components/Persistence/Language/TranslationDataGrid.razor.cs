//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Linq;
using System.Threading.Tasks;
using TContract = QnSTradingCompany.Contracts.Persistence.Language.ITranslation;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Language.Translation;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Language
{
    partial class TranslationDataGrid
    {
        [Parameter]
        public TranslationDataGridHandler DataGridHandler
        {
            get;
            set;
        }
        public override string ForPrefix => "Translation";
    }
}
