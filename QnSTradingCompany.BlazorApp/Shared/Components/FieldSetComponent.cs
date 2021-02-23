//@QnSCodeCopy
//MdStart

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class FieldSetComponent : DisplayComponent
    {
        public virtual bool CanFieldCreate(string propertyName)
        {
            var result = true;

            BeforeCanFieldCreate(propertyName, ref result);
            AfterCanFieldCreate(propertyName, result);
            return result;
        }
        partial void BeforeCanFieldCreate(string propertyName, ref bool value);
        partial void AfterCanFieldCreate(string propertyName, bool value);
    }
}
//MdEnd
