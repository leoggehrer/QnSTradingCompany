//@QnSCodeCopy
//MdStart

using CommonBase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QnSTradingCompany.BlazorApp.Models.Modules.Form
{
    public class GridModelColumn : ModelProperty
    {
        public DisplayProperty DisplayProperty { get; init; }
        public string OriginName => DisplayProperty.OriginName;
        public string PropertyName => DisplayProperty.PropertyName;
        public bool ScaffoldItem => DisplayProperty.ScaffoldItem;
        public bool Readonly => DisplayProperty.Readonly || CanWrite == false;
        public bool ListVisible => DisplayProperty.ListVisible;
        public string ListWidth => DisplayProperty.ListWidth;
        public bool ListSortable => DisplayProperty.ListSortable;
        public bool ListFilterable => DisplayProperty.ListFilterable;
        public bool CanRead => Property.CanRead;
        public bool CanWrite => Property.CanWrite;
        public int Order => DisplayProperty.Order;
        public bool IsIdColumn => OriginName.EndsWith("Id")
                               && (Property.PropertyType.Equals(typeof(int)) || Property.PropertyType.Equals(typeof(int?)));
        public bool IsEnumColumn => Property.PropertyType.IsEnum;
        public bool IsCheckBoxColumn => (Property.PropertyType.Equals(typeof(bool)) || Property.PropertyType.Equals(typeof(bool?)));

        public GridModelColumn(Type modelType, PropertyInfo propertyInfo, DisplayProperty displayProperty)
          : base(modelType, propertyInfo)
        {
            displayProperty.CheckArgument(nameof(displayProperty));

            DisplayProperty = displayProperty;
        }
        public string ToDisplay(object model, object value) => DisplayProperty.ToDisplay(model, value);
        public string GetFooterText(string propertyName) => DisplayProperty.GetFooterText(propertyName);

        #region Enumerations
        public IEnumerable<SelectItem> CreateEnumSelectItems()
        {
            return CreateEnumSelectItems(s => s);
        }
        public IEnumerable<SelectItem> CreateEnumSelectItems(Func<string, string> translate)
        {
            var result = new List<SelectItem>();

            foreach (var item in Enum.GetValues(Property.PropertyType).OfType<Enum>())
            {
                result.Add(new SelectItem { Value = $"{item.Description()}", Text = translate?.Invoke(item.Description()) });
            }
            return result.OrderBy(e => e.Text);
        }
        #endregion Enumerations
    }
}

//MdEnd
