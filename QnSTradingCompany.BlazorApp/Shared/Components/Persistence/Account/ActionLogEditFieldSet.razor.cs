//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    partial class ActionLogEditFieldSet
    {
        [Parameter]
        public int Id
        {
            get;
            set;
        }
        [Parameter]
        public ActionLogFieldSetHandler FieldSetHandler
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
