//@QnSCodeCopy
//MdStart
using System;

namespace QnSTradingCompany.BlazorApp.Models
{
    public partial class ServiceResult
    {
        public bool HasError => Exception != null;
        public bool IsSuccessful => HasError == false;
        public bool HasException => Exception != null;

        public Exception Exception { get; private set; }

        public ServiceResult()
        {
            Exception = null;
        }
        public ServiceResult(Exception exception)
        {
            Exception = exception;
        }
    }
}
//MdEnd
