//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Configuration
{
    partial class SettingEditFieldSetDetail
    {
        [Parameter]
        public QnSTradingCompany.BlazorApp.Models.Persistence.Configuration.Setting EditModel
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
