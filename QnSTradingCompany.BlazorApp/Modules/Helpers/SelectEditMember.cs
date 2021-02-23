//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using QnSTradingCompany.BlazorApp.Models;
using QnSTradingCompany.BlazorApp.Models.Modules.Form;
using QnSTradingCompany.BlazorApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QnSTradingCompany.BlazorApp.Modules.Helpers
{
    public class SelectEditMember<T> : EditModelMember where T : Contracts.IIdentifiable
    {
        public SelectEditMember(ModelPage page, ModelObject model, PropertyInfo propertyInfo, Func<T, string> toText, Func<T, bool> selector)
            : base(page, model, propertyInfo)
        {
            SelectItems = new SelectItems<T>(page, toText, selector);

            if (model is IdentityModel im)
            {
                if (SelectItems.Any(e => e.Selected) == false)
                {
                    EditValue = SelectItems.ElementAt(0).Value;
                }
            }
            EditCtrlType = Common.ControlType.Select;
        }
        public SelectEditMember(ModelPage page, ModelObject model, PropertyInfo propertyInfo, IEnumerable<T> items, Func<T, string> toText, Func<T, bool> selector)
            : base(page, model, propertyInfo)
        {
            items.CheckArgument(nameof(items));

            SelectItems = new SelectItems<T>(items, toText, selector);

            if (model is IdentityModel im)
            {
                if (SelectItems.Any(e => e.Selected) == false)
                {
                    EditValue = SelectItems.ElementAt(0).Value;
                }
            }
            EditCtrlType = Common.ControlType.Select;
        }
    }
}
//MdEnd
