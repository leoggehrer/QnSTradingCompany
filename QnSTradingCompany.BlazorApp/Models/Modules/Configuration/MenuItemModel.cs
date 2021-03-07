//@QnSCodeCopy
//MdStart

namespace QnSTradingCompany.BlazorApp.Models.Modules.Configuration
{
    public class MenuItemModel : ModelObject
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }
    }
}
//MdEnd