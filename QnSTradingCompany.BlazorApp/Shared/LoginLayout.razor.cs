//@QnSCodeCopy

using Radzen.Blazor;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Shared
{
    partial class LoginLayout
    {
        protected RadzenBody LayoutBody { get; set; }

        protected RadzenSidebar LayoutSidebar { get; set; }

        protected async Task SidebarToggleClick(dynamic args)
        {
            await InvokeAsync(() => { LayoutSidebar.Toggle(); });

            await InvokeAsync(() => { LayoutBody.Toggle(); });
        }
    }
}
