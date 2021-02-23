//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using System;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class ComponentHandler
    {
        protected static string GetExceptionError(Exception source)
        {
            source.CheckArgument(nameof(source));
            var errMsg = source.Message;
            Exception innerException = source.InnerException;

            while (innerException != null)
            {

                errMsg = $"{errMsg}{Environment.NewLine}\t{innerException.Message}";
                innerException = innerException.InnerException;
            }
            return errMsg;
        }
    }
}
//MdEnd
