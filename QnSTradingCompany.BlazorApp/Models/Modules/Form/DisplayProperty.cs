//@QnSCodeCopy
//MdStart

using System;

namespace QnSTradingCompany.BlazorApp.Models.Modules.Form
{
    public partial class DisplayProperty
    {
        public string Key => $"{ModelName}{OriginName}";
        public string ModelName { get; set; }
        public string OriginName { get; set; }
        public string MappingName { get; set; } = string.Empty;
        public string PropertyName => string.IsNullOrEmpty(MappingName) ? OriginName : MappingName;

        public bool ScaffoldItem { get; set; } = true;
        public bool IsModelItem { get; set; }
        public bool Readonly { get; set; }
        public string FormatValue { get; set; } = string.Empty;

        public bool Visible { get; set; } = true;
        private bool displayVisible = true;
        public bool DisplayVisible
        {
            get { return Visible && displayVisible; }
            set { displayVisible = value; }
        }
        private bool editVisible = true;
        public bool EditVisible
        {
            get { return Visible && editVisible; }
            set { editVisible = value; }
        }


        private bool listVisible = true;
        public bool ListVisible
        {
            get { return Visible && listVisible; }
            set { listVisible = value; }
        }
        public bool ListSortable { get; set; } = true;
        public bool ListFilterable { get; set; } = true;
        public string ListWidth { get; set; } = "100%";

        public int Order { get; set; } = 10_000;
        public Func<object, object, string> ToDisplay { get; set; }
        public Func<string, string> GetFooterText { get; set; }

        public DisplayProperty()
            : this(string.Empty, string.Empty)
        {
        }
        public DisplayProperty(string originName)
            : this(string.Empty, originName)
        {
        }
        public DisplayProperty(string modelName, string originName)
        {
            ModelName = modelName;
            OriginName = originName;
            ToDisplay = (m, v) => v?.ToString();
            GetFooterText = n => string.Empty;
        }

        public override string ToString() => OriginName;
    }
}
//MdEnd
