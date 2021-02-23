//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.MasterData
{
    partial class ProductEditFieldSet
    {
        [Parameter]
        public int Id
        {
            get;
            set;
        }
        [Parameter]
        public ProductFieldSetHandler FieldSetHandler
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
