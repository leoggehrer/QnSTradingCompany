//@QnSCodeCopy
//MdStart
using CommonBase.Attributes;
using QnSTradingCompany.Contracts.Modules.Common;

namespace QnSTradingCompany.Contracts.Persistence.Account
{
    [ContractInfo]
    public partial interface IIdentity : IVersionable, ICopyable<IIdentity>
    {
        [AutoPropertyInfo(Required = true, MaxLength = 36)]
        string Guid { get; }
        [AutoPropertyInfo(Required = true, MaxLength = 128, IsUnique = true)]
        string Name { get; set; }
        [AutoPropertyInfo(Required = true, MaxLength = 128, IsUnique = true, ContentType = ContentType.EmailAddress)]
        string Email { get; set; }
        [AutoPropertyInfo(NotMapped = true, ContentType = ContentType.Password)]
        string Password { get; set; }
        [AutoPropertyInfo(DefaultValue = "30")]
        int TimeOutInMinutes { get; set; }
        bool EnableJwtAuth { get; set; }
        int AccessFailedCount { get; set; }
        [AutoPropertyInfo(DefaultValue = "Contracts.Modules.Common.State.Active")]
        State State { get; set; }
    }
}
//MdEnd
