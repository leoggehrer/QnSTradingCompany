//QnSBaseCode
using QnSTradingCompany.BlazorApp.Models.Modules.Form;

namespace QnSTradingCompany.BlazorApp.Shared.Components.Business.Account
{
    public partial class AppAccessDataGrid
    {
        protected override void InitDisplayProperties(DisplayPropertyContainer displayProperties)
        {
            base.InitDisplayProperties(displayProperties);

            displayProperties.Add(new DisplayProperty(nameof(Contracts.Persistence.Account.IIdentity.Guid)) { ListVisible = false });
            displayProperties.Add(new DisplayProperty(nameof(Contracts.Persistence.Account.IIdentity.Password)) { ListVisible = false });
        }
    }
}
