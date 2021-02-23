//@QnSCodeCopy
//MdStart
using Microsoft.AspNetCore.Components;
using System;
using System.Reflection;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class PropertyAccess
    {
        [Parameter]
        public object Model { get; set; }

        [Parameter]
        public string PropertyName { get; set; }

        [Parameter]
        public bool IsRequired { get; set; }

        private PropertyInfo property = null;
        private PropertyInfo Property
        {
            get
            {
                if (property == null
                    && Model != null)
                {
                    property = Model.GetType().GetProperty(PropertyName);
                }
                return property;
            }
        }
        public Type ModelType => Model != null ? Model.GetType() : typeof(string);
        public override string ForPrefix => ModelType.Name;
        public string Name => TranslateFor(PropertyName);

        public string Value
        {
            get
            {
                var result = string.Empty;

                if (Model != null)
                {
                    if (Property.CanRead)
                    {
                        var modVal = Property.GetValue(Model);

                        if (modVal != null)
                            result = modVal.ToString();
                    }
                }
                return result;
            }
            set
            {
                if (Model != null)
                {
                    if (Property.CanRead)
                    {
                        Object objValue = Convert.ChangeType(value, Property.PropertyType);

                        Property.SetValue(Model, objValue);
                    }
                }
            }
        }
    }
}
//MdEnd
