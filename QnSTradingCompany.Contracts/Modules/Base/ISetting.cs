//@QnSCodeCopy
//MdStart
using CommonBase.Attributes;

namespace QnSTradingCompany.Contracts.Modules.Base
{
	public partial interface ISetting : IIdentifiable
	{
        [Mandatory(HasUniqueIndexWithName = true, IndexName = "UX_SETTING_APPNAME_KEY", IndexColumnOrder = 0, MaxLength = 128, DefaultValue = "nameof(QnSTradingCompany)")]
        string AppName { get; set; }
        [Mandatory(HasUniqueIndexWithName = true, IndexName = "UX_SETTING_APPNAME_KEY", IndexColumnOrder = 1, MaxLength = 512)]
        string Key { get; set; }
        [ContractPropertyInfo(MaxLength = 4096, DefaultValue = "string.Empty")]
        string Value { get; set; }
    }
}
//MdEnd
