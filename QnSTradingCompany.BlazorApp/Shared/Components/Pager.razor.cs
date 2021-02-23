//@QnSCodeCopy
using Microsoft.AspNetCore.Components;
using QnSTradingCompany.BlazorApp.Models.Modules.Pages;
using System;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    partial class Pager
    {
        [Parameter]
        public PagerModel Model { get; set; }

        [Parameter]
        public Action ViewChangedHandler { get; set; }

        protected bool CheckIndex(int index)
        {
            int maxIndex = Model.PageCount / Model.PageSize;

            if (Model.PageCount % Model.PageSize > 0)
            {
                maxIndex++;
            }
            return index >= 0 && index < maxIndex;
        }
        protected int MinIndexRange(int index)
        {
            return Math.Min((index * Model.PageSize) + 1, Model.PageCount);
        }
        protected int MaxIndexRange(int index)
        {
            return Math.Min(((index + 1) * Model.PageSize), Model.PageCount);
        }
        protected void ChangePageIndex(int newIndex)
        {
            if (Model != null)
            {
                Model.PageIndex = newIndex;
            }
            ViewChangedHandler?.Invoke();
        }
        protected void MovePageIndexPrev()
        {
            if (Model != null)
            {
                Model.MovePageIndexPrev();
            }
            ViewChangedHandler?.Invoke();
        }
        protected void MovePageIndexNext()
        {
            if (Model != null)
            {
                Model.MovePageIndexNext();
            }
            ViewChangedHandler?.Invoke();
        }
        protected void ChangePageSize(int newSize)
        {
            if (Model != null)
            {
                Model.PageIndex = 0;
                Model.PageSize = newSize;
            }
            ViewChangedHandler?.Invoke();
        }
    }
}
