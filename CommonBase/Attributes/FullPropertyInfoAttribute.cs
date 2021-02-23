//@QnSCodeCopy
//MdStart
using System;

namespace CommonBase.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public partial class FullPropertyInfoAttribute : ContractPropertyInfoAttribute
    {
        public FullPropertyInfoAttribute()
        {
            IsAutoProperty = false;
        }
    }
}
//MdEnd
