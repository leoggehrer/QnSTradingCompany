//@QnSCodeCopy
//MdStart

namespace QnSTradingCompany.Transfer
{
    public abstract partial class IdentityModel : TransferModel, Contracts.IIdentifiable
    {
        public virtual int Id { get; set; }
    }
}
//MdEnd
