//@QnSCodeCopy
//MdStart
using System;

namespace CommonBase.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public partial class AutoPropertyInfoAttribute : ContractPropertyInfoAttribute
    {
        public AutoPropertyInfoAttribute()
        {
            IsAutoProperty = true;
        }
    }
}
//MdEnd
