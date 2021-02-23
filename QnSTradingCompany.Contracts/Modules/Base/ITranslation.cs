//@QnSCodeCopy
//MdStart

using CommonBase.Attributes;
using QnSTradingCompany.Contracts.Modules.Common;

namespace QnSTradingCompany.Contracts.Modules.Base
{
	public partial interface ITranslation : IIdentifiable
    {
        [Mandatory(MaxLength = 128, DefaultValue = "nameof(QnSTradingCompany)", HasUniqueIndexWithName = true, IndexName = "UX_Translation", IndexColumnOrder = 1)]
        string AppName { get; set; }
        [ContractPropertyInfo(DefaultValue = "Contracts.Modules.Common.LanguageCode.En", HasUniqueIndexWithName = true, IndexName = "UX_Translation", IndexColumnOrder = 2)]
        LanguageCode KeyLanguage { get; set; }
        [Mandatory(MaxLength = 512, HasUniqueIndexWithName = true, IndexName = "UX_Translation", IndexColumnOrder = 3)]
        string Key { get; set; }
        [ContractPropertyInfo(DefaultValue = "Contracts.Modules.Common.LanguageCode.De")]
        LanguageCode ValueLanguage { get; set; }
        [ContractPropertyInfo(MaxLength = 1024, DefaultValue = "string.Empty")]
        string Value { get; set; }
    }
}
//MdEnd
