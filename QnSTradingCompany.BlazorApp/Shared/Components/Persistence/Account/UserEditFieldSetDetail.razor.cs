//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    partial class UserEditFieldSetDetail
    {
        [Parameter]
        public QnSTradingCompany.BlazorApp.Models.Persistence.Account.User EditModel
        {
            get;
            set;
        }
        [Parameter]
        public Func<string, string> LocalTranslate
        {
            get;
            set;
        }
        [Parameter]
        public Func<string, string> LocalTranslateFor
        {
            get;
            set;
        }
    }
}
