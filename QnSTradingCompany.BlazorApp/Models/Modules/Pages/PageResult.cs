//@QnSCodeCopy
//MdStart
using System;
using System.Collections.Generic;

namespace QnSTradingCompany.BlazorApp.Models.Modules.Pages
{
    public partial class PageResult<T> : ServiceResult where T : ModelObject
    {
        public IEnumerable<T> Models { get; private set; }
        public int PageCount { get; private set; }

        public PageResult(Exception exception)
            : this(null, 0, exception)
        {

        }
        public PageResult(IEnumerable<T> models, int pageCount)
            : this(models, pageCount, null)
        {
        }
        public PageResult(IEnumerable<T> models, int pageCount, Exception exception)
            : base(exception)
        {
            Constructing();
            Models = models ?? Array.Empty<T>();
            PageCount = pageCount;
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
    }
}
//MdEnd
