//@QnSCodeCopy
//MdStart

using CommonBase.Attributes;

namespace QnSTradingCompany.Contracts.Persistence.Account
{
    [ContractInfo]
    public partial interface IUser : IVersionable, ICopyable<IUser>
    {
        [AutoPropertyInfo(IsUnique = true)]
        int IdentityId { get; set; }
        [AutoPropertyInfo(MaxLength = 64)]
        string Firstname { get; set; }
        [AutoPropertyInfo(MaxLength = 64)]
        string Lastname { get; set; }
    }
}
//MdEnd
