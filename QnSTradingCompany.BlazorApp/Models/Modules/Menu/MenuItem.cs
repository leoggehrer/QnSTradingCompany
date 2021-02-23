//@QnSCodeCopy
//MdStart

namespace QnSTradingCompany.BlazorApp.Models.Modules.Menu
{
    public class MenuItem
    {
        public MenuItem Parent { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public int Order { get; set; } = 10_000;
    }
}
//MdEnd
