//@QnSCodeCopy
//MdStart

using System.Collections.Generic;

namespace QnSTradingCompany.BlazorApp.Models
{
    public class DropdownItem
    {
        public string Display { get; set; }
        public string Action { get; set; } = string.Empty;
        public IDictionary<string, object> Params { get; } = new Dictionary<string, object>();
        public bool Active { get; set; } = false;
    }
}
//MdEnd
