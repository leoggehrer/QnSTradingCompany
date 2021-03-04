//@QnSCodeCopy
//MdStart
using Microsoft.AspNetCore.Components;
using QnSTradingCompany.BlazorApp.Services;
using Radzen;

namespace QnSTradingCompany.BlazorApp.Pages
{
    public partial class ModelPage : CommonPage
    {
        [Inject]
        public IServiceAdapter ServiceAdapter { get; private set; }

        public virtual Contracts.Client.IAdapterAccess<T> CreateService<T>()
            where T : Contracts.IIdentifiable
        {
            return ServiceAdapter.Create<T>(AuthorizationSession.Token);
        }

        public virtual void OnMenuItemClick(MenuItemEventArgs args)
        {
        }
    }
}
//MdEnd
