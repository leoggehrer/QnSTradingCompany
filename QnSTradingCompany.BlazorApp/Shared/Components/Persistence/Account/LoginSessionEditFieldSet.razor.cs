//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    partial class LoginSessionEditFieldSet
    {
        [Parameter]
        public int Id
        {
            get;
            set;
        }
        [Parameter]
        public LoginSessionFieldSetHandler FieldSetHandler
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
