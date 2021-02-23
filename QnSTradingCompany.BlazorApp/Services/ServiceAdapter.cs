//@QnSCodeCopy
//MdStart
using QnSTradingCompany.Adapters;

namespace QnSTradingCompany.BlazorApp.Services
{
	public class ServiceAdapter : IServiceAdapter
    {
        /// <summary>
        /// The base url like https://localhost:5001/api
        /// </summary>
        public static string BaseUri
        {
            get => Factory.BaseUri;
            set => Factory.BaseUri = value;
        }

        public static AdapterType Adapter
        {
            get => Factory.Adapter;
            set => Factory.Adapter = value;
        }

        public Contracts.Client.IAdapterAccess<I> Create<I>() where I : Contracts.IIdentifiable
        {
            return Factory.Create<I>();
        }
        public Contracts.Client.IAdapterAccess<I> Create<I>(string sessionToken) where I : Contracts.IIdentifiable
        {
            return Factory.Create<I>(sessionToken);
        }
    }
}
//MdEnd
