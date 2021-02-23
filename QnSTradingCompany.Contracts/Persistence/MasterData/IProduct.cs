using CommonBase.Attributes;

namespace QnSTradingCompany.Contracts.Persistence.MasterData
{
    [ContractInfo]
    public interface IProduct : IVersionable, ICopyable<IProduct>
    {
        [ContractPropertyInfo(Required = true, IsUnique = true, MaxLength = 8)]
        string Number { get; set; }
        [ContractPropertyInfo(Required = true, MaxLength = 256)]
        string Name { get; set; }
        decimal Price { get; set; }
    }
}
