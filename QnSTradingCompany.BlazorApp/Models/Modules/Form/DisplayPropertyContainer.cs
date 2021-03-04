//@QnSCodeCopy
//MdStart

using CommonBase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace QnSTradingCompany.BlazorApp.Models.Modules.Form
{
    public partial class DisplayPropertyContainer : IEnumerable<KeyValuePair<string, DisplayProperty>>
    {
        private readonly Dictionary<string, DisplayProperty> displayProperties = new Dictionary<string, DisplayProperty>();
        public DisplayProperty this[string originName]
        {
            get
            {
                return displayProperties[originName];
            }
        }
        public void Add(DisplayProperty displayProperty)
        {
            displayProperty.CheckArgument(nameof(displayProperty));

            displayProperties.Add(displayProperty.OriginName, displayProperty);
        }
        public void AddOrSet(DisplayProperty displayProperty)
        {
            displayProperty.CheckArgument(nameof(displayProperty));
            displayProperty.OriginName.CheckNotNullOrEmpty(nameof(displayProperty.OriginName));

            if (displayProperties.ContainsKey(displayProperty.OriginName))
            {
                displayProperties[displayProperty.OriginName] = displayProperty;
            }
            else
            {
                Add(displayProperty);
            }
        }

        public bool TryGetValue(string key, out DisplayProperty displayProperty)
        {
            return displayProperties.TryGetValue(key, out displayProperty);
        }
        public void SetValue(string originName, Action<DisplayProperty> action)
        {
            action?.Invoke(this[originName]);
        }

        public bool ContainsKey(string key)
        {
            return displayProperties.ContainsKey(key);
        }
        public IEnumerator<KeyValuePair<string, DisplayProperty>> GetEnumerator()
        {
            return displayProperties.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return displayProperties.GetEnumerator();
        }
    }
}
//MdEnd
