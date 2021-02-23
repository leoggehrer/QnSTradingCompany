//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace QnSTradingCompany.BlazorApp.Models
{
    public partial class ModelObject
    {
        private readonly List<ModelError> errors = new List<ModelError>();

        public IEnumerable<ModelError> Errors => errors;

        public bool HasError => Errors.Any();
        public void ClearErrors() => errors.Clear();

        public bool ContainsError(string key)
        {
            key.CheckNotNullOrEmpty(nameof(key));

            return errors.Find(e => e.Key.Equals(key)) != null;
        }
        public ModelError GetError(string key)
        {
            key.CheckNotNullOrEmpty(nameof(key));

            return errors.Find(e => e.Key.Equals(key));
        }
        public void SetError(ModelError modelError)
        {
            modelError.CheckArgument(nameof(modelError));

            var error = errors.Find(e => e.Key.Equals(modelError.Key));

            if (error == null)
            {
                errors.Add(modelError);
            }
            else
            {
                error.Message = modelError.Message;
            }
        }
        public void RemoveError(string key)
        {
            key.CheckNotNullOrEmpty(nameof(key));

            var error = errors.Find(e => e.Key.Equals(key));

            if (error != null)
            {
                errors.Remove(error);
            }
        }

        protected static bool IsEqualsWith(object obj1, object obj2)
        {
            bool result = false;

            if (obj1 == null && obj2 == null)
            {
                result = true;
            }
            else if (obj1 != null && obj2 != null)
            {
                if (obj1 is IEnumerable objEnum1 && obj2 is IEnumerable objEnum2)
                {
                    var enumerable1 = objEnum1.Cast<object>().ToList();
                    var enumerable2 = objEnum2.Cast<object>().ToList();

                    result = enumerable1.SequenceEqual(enumerable2);
                }
                else
                {
                    result = obj1.Equals(obj2);
                }
            }
            return result;
        }
    }
}
//MdEnd
