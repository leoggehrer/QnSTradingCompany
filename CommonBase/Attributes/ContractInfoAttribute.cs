//@QnSCodeCopy
//MdStart
using System;

namespace CommonBase.Attributes
{
    /// <summary>
    /// These attributes serve to enrich the interface with additional 
    /// information for the generation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public partial class ContractInfoAttribute : Attribute
    {
        public ContextType ContextType { get; set; } = ContextType.Table;
        public string SchemaName { get; set; }
        public string ContextName { get; set; }
        public string KeyName { get; set; }
        public string Description { get; set; }
    }
}
//MdEnd
