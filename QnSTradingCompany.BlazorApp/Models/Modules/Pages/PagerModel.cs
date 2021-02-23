//@QnSCodeCopy
//MdStart
using System.Linq;

namespace QnSTradingCompany.BlazorApp.Models.Modules.Pages
{
    public class PagerModel : IPagerModel
    {
        private int[] pageSizes;
        private int pageCount = 0;
        private DropdownItem[][] filters;

        public int PageIndex { get; set; } = 0;
        public int PageCount
        {
            get => pageCount;
            set
            {
                if (value > -1)
                {
                    pageCount = value;
                    if (PageIndex * PageSize > value)
                    {
                        PageIndex = 0;
                    }
                }
            }
        }
        public int[] PageSizes
        {
            get { return pageSizes ??= new int[] { 5, 10, 25, 50, 100 }; }
            set
            {
                pageSizes = value;
                if (value != null && value.Length > 0)
                {
                    PageSize = value.Min();
                }
                else
                {
                    PageSize = 0;
                }
            }
        }
        public int PageSize { get; set; } = 5;
        public int MinPageIndex => 0;
        public int MaxPageIndex
        {
            get
            {
                int maxPageIndex = PageCount / PageSize;
                if (PageCount % PageSize == 0)
                {
                    maxPageIndex--;
                }
                return maxPageIndex;
            }
        }

        public bool HasFilters => Filters.Length > 0;
        public DropdownItem[][] Filters
        {
            get => filters ??= System.Array.Empty<DropdownItem[]>();
            set => filters = value;
        }

        public bool MovePageIndexPrev()
        {
            bool result = false;

            if (PageIndex > MinPageIndex)
            {
                result = true;
                PageIndex--;
            }
            return result;
        }
        public bool MovePageIndexNext()
        {
            bool result = false;

            if (PageIndex < MaxPageIndex)
            {
                result = true;
                PageIndex++;
            }
            return result;
        }
    }
}
//MdEnd
