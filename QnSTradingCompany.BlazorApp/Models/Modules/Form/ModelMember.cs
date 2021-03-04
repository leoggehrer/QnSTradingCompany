//@QnSCodeCopy
//MdStart

using CommonBase.Extensions;
using System.Reflection;

namespace QnSTradingCompany.BlazorApp.Models.Modules.Form
{
    public abstract partial class ModelMember : ModelProperty
    {
        public DisplayProperty DisplayProperty { get; init; }
        public ModelObject Model { get; init; }

        public bool Visible { get; set; } = true;
        public virtual object Value => Property.GetValue(Model);
        public int Order { get; set; } = 10_000;

        public ModelMember(ModelObject model, PropertyInfo propertyInfo, DisplayProperty displayProperty)
            : base(model?.GetType(), propertyInfo)
        {
            model.CheckArgument(nameof(model));
            displayProperty.CheckArgument(nameof(displayProperty));

            Model = model;
            DisplayProperty = displayProperty;
        }
    }
}
//MdEnd
