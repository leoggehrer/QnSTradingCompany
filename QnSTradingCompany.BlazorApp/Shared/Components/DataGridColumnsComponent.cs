//@QnSCodeCopy
//MdStart

using CommonBase.Extensions;
using QnSTradingCompany.BlazorApp.Models.Modules.Form;
using System;
using System.Reflection;
using TModel = QnSTradingCompany.BlazorApp.Models.ModelObject;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class DataGridColumnsComponent : DisplayComponent
    {
        protected virtual Type GetModelType()
        {
            var handled = false;
            var result = default(Type);

            BeforeGetModelType(ref result, ref handled);
            if (handled == false)
            {
                result = typeof(TModel);
            }
            AfterGetModelType(result);
            return result;
        }
        partial void BeforeGetModelType(ref Type modelType, ref bool handled);
        partial void AfterGetModelType(Type modelType);

        protected override void InitDisplayProperties(DisplayPropertyContainer displayProperties)
        {
            base.InitDisplayProperties(displayProperties);

            displayProperties.Add(new DisplayProperty("Id") { Visible = false, IsModelItem = false });
            displayProperties.Add(new DisplayProperty("RowVersion") { Visible = false, IsModelItem = false });
            displayProperties.Add(new DisplayProperty("HasError") { Visible = false, IsModelItem = false });
            displayProperties.Add(new DisplayProperty("Errors") { Visible = false, IsModelItem = false });
        }
        public virtual GridModelColumn CreateGridModelColumn(Type modelType, PropertyInfo propertyInfo, DisplayProperty displayProperty)
        {
            return new GridModelColumn(modelType, propertyInfo, displayProperty);
        }
        public static DisplayProperty CreateDefaultDisplayProperty(PropertyInfo propertyInfo)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var result = new DisplayProperty(propertyInfo.Name);

            if (propertyInfo.PropertyType.IsNumericType())
                result.ListWidth = "60px";
            else if (propertyInfo.PropertyType.IsEnum)
            {
                result.ListWidth = "125px";
                result.ListFilterable = false;
            }
            return result;
        }
    }
}
//MdEnd
