//@QnSCodeCopy
//MdStart
using CommonBase.Attributes;

namespace QnSTradingCompany.Contracts.Persistence.Account
{
    [ContractInfo]
    public partial interface IRole : IVersionable, ICopyable<IRole>
    {
        [AutoPropertyInfo(IsUnique = true, Required = true, MaxLength = 64)]
        string Designation { get; set; }
        [AutoPropertyInfo(MaxLength = 256)]
        string Description { get; set; }
    }
}
//MdEnd
