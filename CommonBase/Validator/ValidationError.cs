//@QnSCodeCopy
//MdStart

using System;
using System.Collections.Generic;

namespace CommonBase.Extensions
{
    public partial record ValidationError
    {
        public ValidationType ValidationType { get; init; }
        public string Key { get; init; }
        public string Message { get; init; }
        public IEnumerable<object> Parameters { get; init; }

        public ValidationError(ValidationType validationType, string key, string message)
            : this(validationType, key, message, null)
        {
        }

        public ValidationError(ValidationType validationType, string key, string message, object[] parameters)
        {
            key.CheckNotNullOrEmpty(nameof(key));
            message.CheckNotNullOrEmpty(nameof(message));

            ValidationType = validationType;
            Key = key;
            Message = message;
            Parameters = parameters ?? Array.Empty<object>();
        }
    }
}
//MdEnd
