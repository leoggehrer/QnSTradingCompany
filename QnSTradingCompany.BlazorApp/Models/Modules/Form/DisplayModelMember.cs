//@QnSCodeCopy
//MdStart

using QnSTradingCompany.BlazorApp.Pages;
using System;
using System.Reflection;

namespace QnSTradingCompany.BlazorApp.Models.Modules.Form
{
    public partial class DisplayModelMember : ModelMember
    {
        public DisplayModelMember(ModelPage page, ModelObject model, PropertyInfo propertyInfo) 
            : base(page, model, propertyInfo)
        {
        }
        public Func<object, string> ToDisplayValue = v => v != null ? v.ToString() : string.Empty;
        public virtual string DisplayValue => ToDisplayValue?.Invoke(Value);
    }
}
//MdEnd
