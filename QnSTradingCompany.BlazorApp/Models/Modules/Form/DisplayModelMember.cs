//@QnSCodeCopy
//MdStart

using System;
using System.Reflection;

namespace QnSTradingCompany.BlazorApp.Models.Modules.Form
{
    public partial class DisplayModelMember : ModelMember
    {
        public bool Visible => Display.DisplayVisible;
        public Func<object, string> ToDisplayValue = v => v != null ? v.ToString() : string.Empty;
        public virtual string DisplayValue => ToDisplayValue?.Invoke(Value);
        public DisplayModelMember(ModelObject model, PropertyInfo propertyInfo, DisplayProperty displayProperty) 
            : base(model, propertyInfo, displayProperty)
        {
        }
    }
}
//MdEnd
