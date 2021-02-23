using CommonBase.Attributes;

namespace QnSTradingCompany.Contracts.Persistence.App
{
    [ContractInfo]
    public interface IOrder : IVersionable, ICopyable<IOrder>
    {
        int ProductId { get; set; }
        int CustomerId { get; set; }
        int Count { get; set; }
        decimal Discount { get; }
    }
}
