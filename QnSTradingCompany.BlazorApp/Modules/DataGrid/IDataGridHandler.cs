//@QnSCodeCopy
//MdStart
using QnSTradingCompany.BlazorApp.Models;
using QnSTradingCompany.BlazorApp.Pages;
using QnSTradingCompany.Contracts;
using Radzen;
using Radzen.Blazor;
using System;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Modules.DataGrid
{
    public interface IDataGridHandler<TModel>
        where TModel : ModelObject, IIdentifiable, new()
    {
        string AccessFilter { get; set; }
        bool AllowAdd { get; set; }
        bool AllowDelete { get; set; }
        bool AllowEdit { get; set; }
        bool AllowFiltering { get; set; }
        bool AllowInlineEdit { get; set; }
        bool AllowPaging { get; set; }
        bool AllowSorting { get; set; }
        bool HasRowDetail { get; set; }

        int Count { get; }
        int PageSize { get; set; }
        TModel[] Models { get; }
        TModel DeleteModel { get; }
        TModel EditModel { get; }
        TModel ExpandModel { get; }
        TModel SelectedModel { get; }

        string[] ModelItems { get; set; }
        ModelPage ModelPage { get; }

        event EventHandler<TModel> AfterCreateModelHandler;
        event EventHandler<TModel> AfterDeleteModelHandler;
        event EventHandler<TModel> AfterEditModelHandler;
        event EventHandler<TModel> AfterInsertModelHandler;
        event EventHandler<TModel> AfterRowCollapseHandler;
        event EventHandler<TModel> AfterRowExpandHandler;
        event EventHandler<TModel> AfterRowSelectedHandler;
        event EventHandler<TModel> AfterUpdateModelHandler;
        event EventHandler<TModel> BeforeDeleteModelHandler;
        event EventHandler<TModel> BeforeEditModelHandler;
        event EventHandler<TModel> BeforeInsertModelHandler;
        event EventHandler<TModel> BeforeRowCollapseHandler;
        event EventHandler<TModel> BeforeRowExpandHandler;
        event EventHandler<TModel> BeforeRowSelectedHandler;
        event EventHandler<TModel> BeforeUpdateModelHandler;
        event EventHandler<TModel[]> LoadModelDataHandler;

        Task ReloadDataAsync();
        void ReloadModel(TModel model);
    }
}
//MdEnd
