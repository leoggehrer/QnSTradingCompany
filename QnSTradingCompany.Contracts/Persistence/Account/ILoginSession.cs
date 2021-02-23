//@QnSCodeCopy
//MdStart
using CommonBase.Attributes;
using System;

namespace QnSTradingCompany.Contracts.Persistence.Account
{
    [ContractInfo]
    public partial interface ILoginSession : IVersionable, ICopyable<ILoginSession>
    {
        [FullPropertyInfo(IsAutoProperty = false)]
        int IdentityId { get; }
        [FullPropertyInfo(NotMapped = true)]
        bool IsRemoteAuth { get; }
        [FullPropertyInfo(NotMapped = true)]
        string Origin { get; }
        [FullPropertyInfo(NotMapped = true)]
        string Name { get; }
        [FullPropertyInfo(NotMapped = true)]
        string Email { get; }
        [FullPropertyInfo(NotMapped = true)]
        int TimeOutInMinutes { get; }
        [FullPropertyInfo(NotMapped = true)]
        string JsonWebToken { get; }
        [FullPropertyInfo(Required = true, MaxLength = 128)]
        string SessionToken { get; }
        [FullPropertyInfo()]
        DateTime LoginTime { get; }
        [FullPropertyInfo()]
        DateTime LastAccess { get; }
        [FullPropertyInfo()]
        DateTime? LogoutTime { get; }
        [AutoPropertyInfo(MaxLength = 4096)]
        string OptionalInfo { get; set; }
    }
}
//MdEnd
