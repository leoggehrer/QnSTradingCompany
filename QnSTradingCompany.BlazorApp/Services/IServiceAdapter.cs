//@QnSCodeCopy
//MdStart
using QnSTradingCompany.Contracts;
using QnSTradingCompany.Contracts.Client;

namespace QnSTradingCompany.BlazorApp.Services
{
	public interface IServiceAdapter
    {
        IAdapterAccess<I> Create<I>() where I : IIdentifiable;
        IAdapterAccess<I> Create<I>(string sessionToken) where I : IIdentifiable;
    }
}
//MdEnd
