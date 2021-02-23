//@QnSCodeCopy
//MdStart

namespace QnSTradingCompany.BlazorApp.Models.Persistence.Language
{
    partial class Translation
    {
        public string State => Id > 0 ? "Stored" : "Unstored";
    }
}
//MdEnd
