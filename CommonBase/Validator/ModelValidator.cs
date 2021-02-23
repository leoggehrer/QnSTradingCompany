//@QnSCodeCopy
//MdStart

using CommonBase.Attributes;
using CommonBase.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CommonBase.Validator
{
    public partial class ModelValidator
    {
        public static IEnumerable<ValidationError> Validate(object model)
        {
            model.CheckArgument(nameof(model));

            var result = new List<ValidationError>();
            Type type = model.GetType();

            while (type != null)
            {
                foreach (var item in type.GetInterfaces())
                {
                    ValidateInterface(model, item, result);
                }
                type = type.BaseType;
            }
            return result;
        }
        protected static void ValidateInterface(object model, Type type, List<ValidationError> errors)
        {
            type.CheckArgument(nameof(type));

            if (type.IsInterface)
            {
                foreach (var item in type.GetProperties())
                {
                    var attribute = item.GetCustomAttribute<MandatoryAttribute>();

                    if (attribute != null)
                    {
                        CheckRequired(model, item, attribute, errors);
                        CheckMinLength(model, item, attribute, errors);
                        CheckMaxLength(model, item, attribute, errors);
                    }
                }
            }
        }

        private static void CheckRequired(object model, PropertyInfo item, MandatoryAttribute attribute, List<ValidationError> errors)
        {
            if (attribute.Required
                && item.CanRead)
            {
                var value = item.GetValue(model);

                if (value == null
                    || value.ToString().Equals(string.Empty))
                {
                    errors.Add(new ValidationError(ValidationType.Required, $"{item.DeclaringType.Name}.{item.Name}", "The field '{0}' is required.", new object[] { item.Name }));
                }
            }
        }
        private static void CheckMinLength(object model, PropertyInfo item, MandatoryAttribute attribute, List<ValidationError> errors)
        {
            if (attribute.MinLength > 0
                && item.CanRead)
            {
                if (item.PropertyType == typeof(string))
                {
                    var value = item.GetValue(model);

                    if (value != null
                        && value.ToString().Length < attribute.MinLength)
                    {
                        errors.Add(new ValidationError(ValidationType.MinLength, $"{item.DeclaringType.Name}.{item.Name}", "The field '{0}' must have a min length with {1} characters.", new object[] { item.Name, attribute.MinLength }));
                    }
                }
            }
        }
        private static void CheckMaxLength(object model, PropertyInfo item, MandatoryAttribute attribute, List<ValidationError> errors)
        {
            if (attribute.MaxLength > 0
                && item.CanRead)
            {
                if (item.PropertyType == typeof(string))
                {
                    var value = item.GetValue(model);

                    if (value != null
                        && value.ToString().Length > attribute.MaxLength)
                    {
                        errors.Add(new ValidationError(ValidationType.MaxLength, $"{item.DeclaringType.Name}.{item.Name}", "The field '{0}' must have a min length with {1} characters.", new object[] { item.Name, attribute.MaxLength }));
                    }
                }
            }
        }
    }
}
//MdEnd
