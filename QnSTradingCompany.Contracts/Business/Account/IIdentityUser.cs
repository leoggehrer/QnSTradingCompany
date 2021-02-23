//@QnSCodeCopy
//MdStart
using QnSTradingCompany.Contracts.Persistence.Account;

namespace QnSTradingCompany.Contracts.Business.Account
{
    public partial interface IIdentityUser : IOneToAnother<IIdentity, IUser>, ICopyable<IIdentityUser>
    {
    }
}
//MdEnd
