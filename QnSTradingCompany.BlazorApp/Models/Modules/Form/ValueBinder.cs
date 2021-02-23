//@QnSCodeCopy
//MdStart

using CommonBase.Extensions;
using System;
using System.Reflection;

namespace QnSTradingCompany.BlazorApp.Models.Modules.Form
{
    public class ValueBinder
    {
        public ModelObject Model { get; init; }
        public PropertyInfo Property { get; init; }
        public ModelError LastError { get; private set; }

        public ValueBinder(ModelObject model, PropertyInfo property)
        {
            model.CheckArgument(nameof(model));
            property.CheckArgument(nameof(property));

            Model = model;
            Property = property;
        }

        public virtual object Value
        {
            get => Property.GetValue(Model);
            set
            {
                try
                {
                    var result = value.TryParse(Property.PropertyType, out object typeValue);

                    if (result)
                    {
                        Property.SetValue(Model, typeValue);
                        LastError = null;
                    }
                    else
                    {
                        LastError = new ModelError(Property.Name, "The input for {0} cannot be converted.", new object[] { Property.Name });
                    }
                }
                catch (Exception ex)
                {
                    LastError = new ModelError(Property.Name, ex.Message);
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }
        public virtual string StringValue
        {
            get
            {
                var value = Value;

                return value?.ToString();
            }
            set
            {
                Value = value;
            }
        }
    }
}
//MdEnd
