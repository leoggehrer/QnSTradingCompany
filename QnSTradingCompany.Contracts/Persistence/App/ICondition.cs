using CommonBase.Attributes;
using QnSTradingCompany.Contracts.Modules.Common;

namespace QnSTradingCompany.Contracts.Persistence.App
{
    [ContractInfo]
    public interface ICondition : IVersionable, ICopyable<ICondition>
    {
        int ProductId { get; set; }
        int CustomerId { get; set; }
        ConditionType ConditionType { get; set; }
        double Quantity { get; set; }
        decimal Value { get; set; }
        [ContractPropertyInfo(MaxLength = 1024)]
        string Note { get; set; }
    }
}
