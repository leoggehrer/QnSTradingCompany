//@QnSGeneratedCode
using TContract = QnSTradingCompany.Contracts.Persistence.Account.IActionLog;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.Account.ActionLog;
namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Account
{
    public partial class ActionLogFieldSetHandler : FieldSetHandler<TContract, TModel>
    {
        public ActionLogFieldSetHandler(Pages.ModelPage modelPage, Contracts.Client.IAdapterAccess<TContract> adapterAccess) : base(modelPage, adapterAccess)
        {
        }
    }
}
