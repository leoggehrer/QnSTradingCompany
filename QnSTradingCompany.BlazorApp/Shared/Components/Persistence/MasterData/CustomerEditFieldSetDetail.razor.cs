//@QnSGeneratedCode
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.MasterData
{
    partial class CustomerEditFieldSetDetail
    {
        [Parameter]
        public QnSTradingCompany.BlazorApp.Models.Persistence.MasterData.Customer EditModel
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
