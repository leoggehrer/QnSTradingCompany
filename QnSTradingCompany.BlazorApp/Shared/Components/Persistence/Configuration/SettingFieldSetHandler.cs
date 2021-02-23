//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Persistence.Configuration.ISetting;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Configuration.Setting;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Configuration
{
    public partial class SettingFieldSetHandler : FieldSetHandler<TContract, TModel>
    {
        public SettingFieldSetHandler(Pages.ModelPage modelPage, Contracts.Client.IAdapterAccess<TContract> adapterAccess) : base(modelPage, adapterAccess)
        {
        }
    }
}
