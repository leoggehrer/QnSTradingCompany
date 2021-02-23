//@QnSCodeCopy
//MdStart
using System;
using System.Collections.Generic;

namespace CommonBase.Attributes
{
    /// <summary>
    /// These attributes serve to enrich the member with mapping information.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Property)]
    public partial class MappingAttribute : Attribute
    {
        public IEnumerable<string> Names { get; }
        public MappingAttribute()
        {
            Names = new string[0];
        }
        public MappingAttribute(params string[] names)
        {
            Names = names ?? new string[0];
        }
    }
}
//MdEnd
