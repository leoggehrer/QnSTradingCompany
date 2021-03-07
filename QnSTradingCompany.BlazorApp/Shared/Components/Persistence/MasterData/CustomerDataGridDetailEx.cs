using QnSTradingCompany.BlazorApp.Models.Modules.Form;
using QnSTradingCompany.BlazorApp.Shared.Components.Persistence.App;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.App.Order;

namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.MasterData
{
    partial class CustomerDataGridDetail
    {
        protected override void InitDisplayProperties(DisplayPropertyContainer displayProperties)
        {
            base.InitDisplayProperties(displayProperties);

            displayProperties.Add(new DisplayProperty(nameof(TModel.CustomerId)) { ListVisible = false });
        }
        protected OrderDataGridHandler OrderDataGridHandler
        {
            get;
            private set;
        }

        protected override void BeforeInitialized()
        {
            base.BeforeInitialized();

            OrderDataGridHandler = new OrderDataGridHandler(ModelPage)
            {
                AllowAdd = false,
                AllowEdit = false,
                AllowDelete = false,
                AllowSorting = true,
                AllowFiltering = true,
                AccessFilter = $"{nameof(TModel.CustomerId)}={ParentModel.Id}"
            };
        }
    }
}
