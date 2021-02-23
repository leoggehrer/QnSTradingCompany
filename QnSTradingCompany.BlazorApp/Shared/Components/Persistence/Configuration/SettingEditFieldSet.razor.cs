//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Configuration
{
    partial class SettingEditFieldSet
    {
        [Parameter]
        public int Id
        {
            get;
            set;
        }
        [Parameter]
        public SettingFieldSetHandler FieldSetHandler
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
