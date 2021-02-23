//@QnSCodeCopy
//MdStart

using CommonBase.Extensions;
using System;
using System.Collections.Generic;

namespace QnSTradingCompany.BlazorApp.Models
{
    public partial record ModelError
    {
        public string Key { get; init; }
        public string Message { get; set; }
        public IEnumerable<object> Parameters { get; init; }

        public ModelError(string key, string message)
            : this(key, message, null)
        {
        }

        public ModelError(string key, string message, object[] parameters)
        {
            key.CheckNotNullOrEmpty(nameof(key));
            message.CheckNotNullOrEmpty(nameof(message));

            Key = key;
            Message = message;
            Parameters = parameters ?? Array.Empty<object>();
        }

        public override string ToString()
        {
            return string.Format($"{Key}: {Message}", Parameters);
        }
    }
}
//MdEnd
