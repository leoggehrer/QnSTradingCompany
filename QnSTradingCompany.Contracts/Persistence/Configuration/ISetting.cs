//@QnSCodeCopy
//MdStart

using CommonBase.Attributes;

namespace QnSTradingCompany.Contracts.Persistence.Configuration
{
	[ContractInfo]
    public interface ISetting : Modules.Base.ISetting, IVersionable, ICopyable<ISetting>
    {
    }
}
//MdEnd
