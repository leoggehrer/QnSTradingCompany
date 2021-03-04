using CommonBase.Attributes;
using System;

namespace QnSTradingCompany.Contracts.Persistence.App
{
    [ContractInfo]
    public interface IOrder : IVersionable, ICopyable<IOrder>
    {
        int ProductId { get; set; }
        int CustomerId { get; set; }
        [ContractPropertyInfo(DefaultValue = "DateTime.Now")]
        DateTime CreatedOn { get; }
        int Count { get; }
        decimal PriceNet { get; }
        decimal Discount { get; }
    }
}
