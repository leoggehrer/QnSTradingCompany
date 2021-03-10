//@QnSCodeCopy
//MdStart

namespace QnSTradingCompany.BlazorApp.Models.Modules.Configuration
{
    public partial class DialogOptions : ConfigurationModel
    {
        public bool ShowTitle { get; set; }
        public bool ShowClose { get; set; }
        public string Left { get; set; }
        public string Top { get; set; }
        public string Bottom { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
    }
}
//MdEnd
