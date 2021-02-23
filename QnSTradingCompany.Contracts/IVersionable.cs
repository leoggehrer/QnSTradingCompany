//@QnSCodeCopy
//MdStart
using CommonBase.Attributes;

namespace QnSTradingCompany.Contracts
{
    /// <summary>
    /// Defines the basic properties of versionables components.
    /// </summary>
    public partial interface IVersionable : IIdentifiable
    {
        /// <summary>
        /// Gets the row stamp.
        /// </summary>
        [ContractPropertyInfo(Order = 200)]
        byte[] RowVersion { get; }
    }
}
//MdEnd
