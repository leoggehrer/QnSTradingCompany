using QnSTradingCompany.BlazorApp.Models.Modules.Form;
using QnSTradingCompany.BlazorApp.Shared.Components.Persistence.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.App.Order;

namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.MasterData
{
    partial class ProductDataGridDetail
    {
        protected override void InitDisplayProperties(DisplayPropertyContainer displayProperties)
        {
            base.InitDisplayProperties(displayProperties);

            displayProperties.Add(new DisplayProperty(nameof(TModel.ProductId)) { ListVisible = false });
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
                AccessFilter = $"{nameof(TModel.ProductId)}={ParentModel.Id}"
            };
        }
    }
}
