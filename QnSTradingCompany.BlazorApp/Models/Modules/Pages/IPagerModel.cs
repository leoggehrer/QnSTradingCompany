//@QnSCodeCopy
//MdStart
namespace QnSTradingCompany.BlazorApp.Models.Modules.Pages
{
    public interface IPagerModel
    {
        DropdownItem[][] Filters { get; set; }
        bool HasFilters { get; }
        int MaxPageIndex { get; }
        int MinPageIndex { get; }
        int PageCount { get; set; }
        int PageIndex { get; set; }
        int PageSize { get; set; }
        int[] PageSizes { get; set; }

        bool MovePageIndexNext();
        bool MovePageIndexPrev();
    }
}
//MdEnd
