//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Language
{
    partial class TranslationEditFieldSet
    {
        [Parameter]
        public int Id
        {
            get;
            set;
        }
        [Parameter]
        public TranslationFieldSetHandler FieldSetHandler
        {
            get;
            set;
        }
        [Inject]
        protected DialogService DialogService
        {
            get;
            private set;
        }
        [Inject]
        protected NotificationService NotificationService
        {
            get;
            private set;
        }
    }
}
