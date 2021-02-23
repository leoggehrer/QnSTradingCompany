//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Language
{
    partial class TranslationEditFieldSetDetail
    {
        [Parameter]
        public QnSTradingCompany.BlazorApp.Models.Persistence.Language.Translation EditModel
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
