//@QnSCodeCopy
//MdStart
using System.Collections.Generic;

namespace QnSTradingCompany.AspMvc.Models.Modules.Language
{
    public class AppTranslation : ModelObject
    {
        public string Action { get; set; }
        public List<ActionItem> NavLinks { get; } = new List<ActionItem>();
        public Dictionary<string, TranslationEntry> Entries { get; } = new Dictionary<string, TranslationEntry>();
    }
}
//MdEnd
