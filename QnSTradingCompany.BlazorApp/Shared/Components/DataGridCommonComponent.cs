//@QnSCodeCopy
//MdStart

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class DataGridCommonComponent : DisplayComponent
    {
        public DataGridCommonComponent()
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
    }
}
//MdEnd
