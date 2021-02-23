//@QnSCodeCopy
//MdStart
using QnSTradingCompany.BlazorApp.Models.Modules.Form;

namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Configuration
{
    partial class SettingDataGridColumns
    {
        protected override void InitDisplayProperties(DisplayPropertyContainer displayProperties)
        {
            base.InitDisplayProperties(displayProperties);

            displayProperties.Add(new DisplayProperty(nameof(Models.Persistence.Configuration.Setting.State))
            {
                Order = 1_000,
                IsModelItem = true,
            }); ;
        }
    }
}
//MdEnd
