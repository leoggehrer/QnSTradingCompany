//@QnSCodeCopy
//MdStart

using CommonBase.Extensions;
using System;
using System.Collections.Generic;

namespace QnSTradingCompany.BlazorApp.Models.Modules.Form
{
    public static partial class Selector
    {
        public static IEnumerable<SelectItem> LoadEnumLiterals<T>()
        {
            return LoadEnumLiterals<T>(null);
        }
        public static IEnumerable<SelectItem> LoadEnumLiterals<T>(object selectedValue)
        {
            return LoadEnumLiterals<T>(selectedValue, null);
        }
        public static IEnumerable<SelectItem> LoadEnumLiterals<T>(object selectedValue, Func<string, string> translateHandler)
        {
            return LoadEnumLiterals(typeof(T), selectedValue, translateHandler);
        }
        public static IEnumerable<SelectItem> LoadEnumLiterals(Type type, object selectedValue, Func<string, string> translateHandler)
        {
            type.CheckArgument(nameof(type));

            List<SelectItem> items = new List<SelectItem>();

            foreach (var item in Enum.GetNames(type))
            {
                object value = Enum.Parse(type, item);

                items.Add(new SelectItem
                {
                    Value = value != null ? value.ToString() : string.Empty,
                    Text = translateHandler != null ? translateHandler($"{item}") : item,
                    Selected = value != null && selectedValue != null && value.Equals(selectedValue),
                });
            }
            return items;
        }
    }
}
//MdEnd
