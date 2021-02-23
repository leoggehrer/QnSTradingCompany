//@QnSCodeCopy
//MdStart

using CommonBase.Extensions;
using QnSTradingCompany.BlazorApp.Pages;
using System.Reflection;

namespace QnSTradingCompany.BlazorApp.Models.Modules.Form
{
    public abstract partial class ModelMember : ModelProperty
    {
        public ModelPage Page { get; init; }
        public ModelObject Model { get; init; }
        public bool IsVisible { get; set; } = true;
        public virtual object Value => Property.GetValue(Model);
        public int Order { get; set; } = 10_000;
        public ModelMember(ModelPage page, ModelObject model, PropertyInfo propertyInfo)
            : base(model?.GetType(), propertyInfo)
        {
            page.CheckArgument(nameof(page));
            model.CheckArgument(nameof(model));

            Page = page;
            Model = model;
        }
    }
}
//MdEnd
