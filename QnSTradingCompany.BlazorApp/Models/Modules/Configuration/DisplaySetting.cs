//@QnSCodeCopy
//MdStart

namespace QnSTradingCompany.BlazorApp.Models.Modules.Configuration
{
    public partial class DisplaySetting : ConfigurationModel
    {
        public bool ScaffoldItem { get; set; }
        public bool IsModelItem { get; set; }
        public bool Readonly { get; set; }
        public bool Visible { get; set; }
        public bool DisplayVisible { get; set; }
        public bool EditVisible { get; set; }
        public bool ListVisible { get; set; }
        public bool ListSortable { get; set; }
        public bool ListFilterable { get; set; }
        public string FormatValue { get; set; }
        public string ListWidth { get; set; }
        public int Order { get; set; }
    }
}
