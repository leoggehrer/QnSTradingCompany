//@QnSCodeCopy
//MdStart

namespace CSharpCodeGenerator.Logic.Models.Configuration
{
    internal record DataGridSetting
    {
#pragma warning disable CA1822 // Mark members as static
        public string Type => nameof(DataGridSetting);
#pragma warning restore CA1822 // Mark members as static
        public bool HasDataGridProgress { get; set; }
        public bool HasEditDialogHeader { get; set; }
        public bool HasEditDialogFooter { get; set; }
        public bool HasDeleteDialogHeader { get; set; }
        public bool HasDeleteDialogFooter { get; set; }
    }
}
//MdEnd
