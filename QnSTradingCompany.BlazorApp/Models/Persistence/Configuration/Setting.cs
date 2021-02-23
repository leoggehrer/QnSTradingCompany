//@QnSCodeCopy
//MdStart

namespace QnSTradingCompany.BlazorApp.Models.Persistence.Configuration
{
    partial class Setting
    {
        public string State => Id > 0 ? "Stored" : "Unstored";
    }
}
//MdEnd
