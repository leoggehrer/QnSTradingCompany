//@QnSCodeCopy
//MdStart
using CommonBase.Attributes;
using System;

namespace QnSTradingCompany.Contracts.Persistence.Data
{
    [ContractInfo]
    public partial interface IBinaryData : IVersionable, ICopyable<IBinaryData>
    {
        [ContractPropertyInfo(Required = true, IsUnique = true)]
        Guid Guid { get; set; }
        [ContractPropertyInfo(Required = true)]
        byte[] Data { get; set; }
    }
}
//MdEnd
