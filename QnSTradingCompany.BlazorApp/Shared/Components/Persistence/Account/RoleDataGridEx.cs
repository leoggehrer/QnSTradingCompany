//@QnSCodeCopy
using QnSTradingCompany.BlazorApp.Models.Modules.Form;

namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    partial class RoleDataGrid
    {
        protected override void InitDisplayProperties(DisplayPropertyContainer displayProperties)
        {
            base.InitDisplayProperties(displayProperties);

            displayProperties.Add(new DisplayProperty("Assigned") { ScaffoldItem = false });
        }
    }
}
