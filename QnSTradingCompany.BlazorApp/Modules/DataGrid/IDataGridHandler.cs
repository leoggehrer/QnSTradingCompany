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
        int Count { get; }
        TModel DeleteModel { get; }
        TModel EditModel { get; }
        TModel ExpandModel { get; }
        string ForPrefix { get; }
        int From { get; }
        bool HasRowDetail { get; set; }
        string[] ModelItems { get; set; }
        ModelPage ModelPage { get; }
        TModel[] Models { get; }
        int PageSize { get; set; }
        RadzenGrid<TModel> RadzenGrid { get; set; }
        TModel SelectedModel { get; }
        Func<Task> ShowConfirmDeleteAsync { get; set; }
        Func<Task> ShowEditItemAsync { get; set; }
        Action<NotificationMessage> ShowNotification { get; set; }
        int To { get; }
        Func<string, string> Translate { get; init; }

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

        Task AddItemAsync();
        void CancelEditDialog(DialogService dialogService);
        void CancelDeleteDialog(DialogService dialogService);
        void OnCloseDialog(dynamic result);
        void CancelEditRow(TModel item);
        void CommitEditRow(TModel item);
        Task ConfirmDeleteItemAsync(DialogService dialogService);
        Task DeleteModelAsync(TModel model);
        Task DeleteRowAsync(TModel model);
        Task EditRowAsync(TModel item);
        Task InsertModelAsync(TModel model);
        Task LoadDataAsync(LoadDataArgs args);
        Task ReloadDataAsync();
        void ReloadModel(TModel model);
        void RowCollapse(TModel item);
        Task RowDoubleClickAsync(TModel item);
        void RowExpand(TModel item);
        void RowRender(RowRenderEventArgs<TModel> args);
        Task RowSelectedAsync(TModel item);
        Task SubmitEditAsync(DialogService dialogService);
        string TranslateFor(string key);
        Task UpdateModelAsync(TModel model);
        Task UpdateRowAsync(TModel item);
        void ValueChanged(TModel item);
    }
}
//MdEnd
