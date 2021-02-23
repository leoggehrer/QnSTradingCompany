//@QnSCodeCopy
//MdStart
using CommonBase.Attributes;

namespace QnSTradingCompany.Contracts
{
    /// <summary>
    /// Defines the basic properties of identifiable components.
    /// </summary>
    public partial interface IIdentifiable
    {
        /// <summary>
        /// Gets the identity of the component.
        /// </summary>
        /// 
        /// 
        /// 
        [ContractPropertyInfo(Order = 100)]
        int Id { get; }
    }
}
//MdEnd
