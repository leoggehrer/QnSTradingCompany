//@QnSCodeCopy
//MdStart

namespace QnSTradingCompany.Logic.Entities
{
    internal abstract partial class VersionEntity : IdentityEntity, Contracts.IVersionable
    {
        public virtual byte[] RowVersion { get; set; }
    }
}
//MdEnd
