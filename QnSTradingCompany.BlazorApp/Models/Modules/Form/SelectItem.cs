//@QnSCodeCopy
//MdStart

namespace QnSTradingCompany.BlazorApp.Models.Modules.Form
{
    public partial class SelectItem
    {
        //
        // Summary:
        //     Gets or sets a value that indicates whether this Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        //     is selected. This property is typically rendered as a
        //     selected="selected"
        //     attribute in the HTML
        //     <option>
        //     element.
        public bool Selected { get; set; }
        //
        // Summary:
        //     Gets or sets a value that indicates the value of this Microsoft.AspNetCore.Mvc.Rendering.SelectListItem.
        //     This property is typically rendered as a
        //     value="..."
        //     attribute in the HTML
        //     <option>
        //     element.
        public string Value { get; init; }
        //
        // Summary:
        //     Gets or sets a value that indicates the display text of this Microsoft.AspNetCore.Mvc.Rendering.SelectListItem.
        //     This property is typically rendered as the inner HTML in the HTML
        //     <option>
        //     element.
        public string Text { get; set; }
    }
}
//MdEnd
