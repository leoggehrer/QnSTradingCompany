//@QnSCodeCopy
//MdStart
using QnSTradingCompany.Contracts;
using QnSTradingCompany.Contracts.Client;

namespace QnSTradingCompany.AspMvc
{
    public interface IFactoryWrapper
    {
        IAdapterAccess<I> Create<I>() where I : IIdentifiable;
        IAdapterAccess<I> Create<I>(string sessionToken) where I : IIdentifiable;
    }
}
//MdEnd
