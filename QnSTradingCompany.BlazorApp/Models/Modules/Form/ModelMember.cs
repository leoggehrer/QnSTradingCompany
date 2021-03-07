//@QnSCodeCopy
//MdStart

using CommonBase.Extensions;
using System.Reflection;

namespace QnSTradingCompany.BlazorApp.Models.Modules.Form
{
    public abstract partial class ModelMember : ModelProperty
    {
        private int order = -1;

        public ModelObject Model { get; init; }
        public DisplayProperty Display { get; init; }
        public bool ScaffoldItem => Display.ScaffoldItem;

        public int Order 
        {
            get => order < 0 ? Display.Order : order; 
            set => order = value; 
        }
        public virtual object Value => Property.GetValue(Model);

        public ModelMember(ModelObject model, PropertyInfo propertyInfo, DisplayProperty displayProperty)
            : base(model?.GetType(), propertyInfo)
        {
            model.CheckArgument(nameof(model));
            displayProperty.CheckArgument(nameof(displayProperty));

            Model = model;
            Display = displayProperty;
        }
    }
}
//MdEnd
