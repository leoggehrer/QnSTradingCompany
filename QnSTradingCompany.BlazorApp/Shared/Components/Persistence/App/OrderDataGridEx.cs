using CommonBase.Attributes;
using QnSTradingCompany.BlazorApp.Models.Modules.Form;
using QnSTradingCompany.BlazorApp.Modules.DataGrid;
using QnSTradingCompany.Contracts.Persistence.MasterData;
using TModel = QnSTradingCompany.BlazorApp.Models.Persistence.App.Order;

namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.App
{
    partial class OrderDataGrid
    {
        protected override void InitDisplayProperties(DisplayPropertyContainer displayProperties)
        {
            base.InitDisplayProperties(displayProperties);

            //displayProperties.Add(new DisplayProperty(nameof(TModel.CreatedOn)) { Readonly = true });
            //displayProperties.Add(new DisplayProperty(nameof(TModel.PriceNet)) { Readonly = true });
            //displayProperties.Add(new DisplayProperty(nameof(TModel.Discount)) { Readonly = true });
            //displayProperties.Add(new DisplayProperty(nameof(TModel.ProductName)) { EditVisible = false });
            //displayProperties.Add(new DisplayProperty(nameof(TModel.CustomerName)) { EditVisible = false });
        }
        [DisposeField]
        protected DataGridAssociationItem<TModel, IProduct> associationProduct;
        [DisposeField]
        protected DataGridAssociationItem<TModel, ICustomer> associationCustomer;

        protected override void BeforeInitialized()
        {
            base.BeforeInitialized();

            associationProduct = new DataGridAssociationItem<TModel, IProduct>(this, DataGridHandler, nameof(TModel.ProductId), i => i.Name, (m, i) => m.ProductName = i.Name);
            associationCustomer = new DataGridAssociationItem<TModel, ICustomer>(this, DataGridHandler, nameof(TModel.CustomerId), i => i.Name, (m, i) => m.CustomerName = i.Name);
        }
    }
}
