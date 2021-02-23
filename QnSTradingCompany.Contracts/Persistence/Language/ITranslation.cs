//@QnSCodeCopy
//MdStart
using CommonBase.Attributes;

namespace QnSTradingCompany.Contracts.Persistence.Language
{
    [ContractInfo]
    public interface ITranslation : Modules.Base.ITranslation, IVersionable, ICopyable<ITranslation>
    {
    }
}
//MdEnd
