//@QnSCodeCopy
//MdStart

using QnSTradingCompany.BlazorApp.Models.Modules.Form;
using System;
using System.Reflection;
using TModel = QnSTradingCompany.BlazorApp.Models.ModelObject;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class DataGridColumnsComponent : DataGridCommonComponent
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

        public virtual GridModelColumn CreateGridModelColumn(Type modelType, PropertyInfo propertyInfo)
        {
            var displayProperty = GetOrCreateDisplayProperty(modelType, propertyInfo);

            return new GridModelColumn(modelType, propertyInfo, displayProperty);
        }
    }
}
//MdEnd
