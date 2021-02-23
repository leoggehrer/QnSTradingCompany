//@QnSCodeCopy
//MdStart
using QnSTradingCompany.BlazorApp.Models.Modules.Menu;
using System.Collections.Generic;

namespace QnSTradingCompany.BlazorApp.Pages
{
	public partial class DataGridPage : ModelPage
    {
        protected List<MenuItem> MenuItems { get; } = new List<MenuItem>();
    }
}
//MdEnd
