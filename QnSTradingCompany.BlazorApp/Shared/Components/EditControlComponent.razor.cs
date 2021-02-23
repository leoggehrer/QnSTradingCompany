//@QnSCodeCopy
//MdStart

using Microsoft.AspNetCore.Components;
using QnSTradingCompany.BlazorApp.Models.Modules.Form;
using TMember = QnSTradingCompany.BlazorApp.Models.Modules.Form.EditModelMember;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class EditControlComponent
    {
        [Parameter]
        public TMember ModelMember { get; set; }
        [Parameter]
        public HtmlInfo HtmlInfo { get; set; }
    }
}
//MdEnd
