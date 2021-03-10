//@QnSCodeCopy
//MdStart

namespace CSharpCodeGenerator.Logic.Models.Configuration
{
    internal record DisplaySetting
    {
#pragma warning disable CA1822 // Mark members as static
        public string Type => nameof(DisplaySetting);
#pragma warning restore CA1822 // Mark members as static
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
//MdEnd
