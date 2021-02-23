using CommonBase.Attributes;

namespace QnSTradingCompany.Contracts.Persistence.MasterData
{
    [ContractInfo]
    public interface ICustomer : IVersionable, ICopyable<ICustomer>
    {
        [ContractPropertyInfo(Required = true, IsUnique = true, MaxLength = 8)]
        string Number { get; set; }
        [ContractPropertyInfo(Required = true, MaxLength = 256)]
        string Name { get; set; }
    }
}
