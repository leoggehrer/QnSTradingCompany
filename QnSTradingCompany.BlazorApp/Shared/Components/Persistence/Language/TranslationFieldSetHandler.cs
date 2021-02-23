//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Persistence.Language.ITranslation;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Language.Translation;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Language
{
    public partial class TranslationFieldSetHandler : FieldSetHandler<TContract, TModel>
    {
        public TranslationFieldSetHandler(Pages.ModelPage modelPage, Contracts.Client.IAdapterAccess<TContract> adapterAccess) : base(modelPage, adapterAccess)
        {
        }
    }
}
