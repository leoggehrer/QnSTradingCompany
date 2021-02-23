//@QnSCodeCopy
//MdStart

using System;

namespace CSharpCodeGenerator.Logic.Generation
{
    partial class ControllerGenerator
    {
        static partial void ConvertGenericPersistenceControllerName(Type type, ref string name)
        {
            if (type.FullName.EndsWith(".Data.IBinaryData"))
                name = $"{name}WithRun";
        }
    }
}
//MdEnd
