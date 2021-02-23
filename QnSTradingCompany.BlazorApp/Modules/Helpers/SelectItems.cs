//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using QnSTradingCompany.BlazorApp.Models.Modules.Form;
using QnSTradingCompany.BlazorApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Modules.Helpers
{
    public class SelectItems<T> : List<SelectItem>
        where T : Contracts.IIdentifiable
    {
        public SelectItems(IEnumerable<T> items, Func<T, string> toText, Func<T, bool> selector)
        {
            items.CheckArgument(nameof(items));

            AddRange(items.Select(e => new SelectItem
            {
                Value = e.Id.ToString(),
                Text = toText != null ? toText(e) : e.ToString(),
                Selected = selector != null && selector.Invoke(e)
            }).OrderBy(e => e.Text));
        }
        public SelectItems(ModelPage modelPage, Func<T, string> toText, Func<T, bool> selector)
        {
            modelPage.CheckArgument(nameof(modelPage));

            using var access = modelPage.CreateService<T>();
            var items = Task.Run(async () => await access.GetAllAsync().ConfigureAwait(false))
                            .Result
                            .Select(e => new SelectItem
                            {
                                Value = e.Id.ToString(),
                                Text = toText != null ? toText(e) : e.ToString(),
                                Selected = selector != null && selector.Invoke(e)
                            });
            AddRange(items.OrderBy(e => e.Text));
        }
    }
}
//MdEnd
