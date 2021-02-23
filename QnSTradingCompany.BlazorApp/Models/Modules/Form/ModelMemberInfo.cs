//@QnSCodeCopy
//MdStart
using System.Reflection;

namespace QnSTradingCompany.BlazorApp.Models.Modules.Form
{
    public abstract class ModelMemberInfo
    {
        public ModelObject Model { get; init; }
        public PropertyInfo Property { get; init; }
        public bool Created { get; set; }
    }
}
//MdEnd
