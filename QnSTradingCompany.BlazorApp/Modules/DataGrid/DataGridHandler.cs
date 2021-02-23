//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using CommonBase.Validator;
using QnSTradingCompany.BlazorApp.Models;
using QnSTradingCompany.BlazorApp.Pages;
using QnSTradingCompany.BlazorApp.Shared.Components;
using QnSTradingCompany.Contracts.Client;
using Radzen;
using Radzen.Blazor;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Modules.DataGrid
{
	public partial class DataGridHandler<TContract, TModel> : ComponentHandler, IDataGridHandler<TModel> 
        where TContract : Contracts.IIdentifiable, Contracts.ICopyable<TContract>
        where TModel : ModelObject, TContract, new()
    {
        public ModelPage ModelPage { get; protected set; }
        public IAdapterAccess<TContract> AdapterAccess { get; protected set; }

        public virtual string ForPrefix => typeof(TModel).Name;
        public Func<string, string> Translate { get; init; }
        public string TranslateFor(string key) => Translate($"{ForPrefix}.{key}");

        public Action<NotificationMessage> ShowNotification { get; set; }
        public Func<Task> ShowEditItemAsync { get; set; }
        public Func<Task> ShowConfirmDeleteAsync { get; set; }

        public DataGridHandler(ModelPage modelPage, IAdapterAccess<TContract> adapterAccess)
        {
            Constructing();
            modelPage.CheckArgument(nameof(modelPage));
            adapterAccess.CheckArgument(nameof(adapterAccess));

            ModelPage = modelPage;
            AdapterAccess = adapterAccess;
            Translate = ModelPage.Translate;
            Constructed();
        }
        protected virtual void Constructing()
        {
        }
        protected virtual void Constructed()
        {
        }

        #region EventHandler
        public event EventHandler<LoadDataArgs> BeforeLoadDataHandler;

        public event EventHandler<TModel[]> LoadModelDataHandler;

        public event EventHandler<TModel> BeforeRowSelectedHandler;
        public event EventHandler<TModel> AfterRowSelectedHandler;

        public event EventHandler<TModel> BeforeRowCollapseHandler;
        public event EventHandler<TModel> AfterRowCollapseHandler;

        public event EventHandler<TModel> BeforeRowExpandHandler;
        public event EventHandler<TModel> AfterRowExpandHandler;

        public event EventHandler<TModel> AfterCreateModelHandler;

        public event EventHandler<TModel> BeforeInsertModelHandler;
        public event EventHandler<TModel> AfterInsertModelHandler;

        public event EventHandler<TModel> BeforeUpdateModelHandler;
        public event EventHandler<TModel> AfterUpdateModelHandler;

        public event EventHandler<TModel> BeforeDeleteModelHandler;
        public event EventHandler<TModel> AfterDeleteModelHandler;

        public event EventHandler<TModel> BeforeEditModelHandler;
        public event EventHandler<TModel> AfterEditModelHandler;
        #endregion EventHandler

        public bool AllowPaging { get; set; } = true;
        public bool AllowSorting { get; set; } = true;
        public bool AllowFiltering { get; set; } = true;
        public bool AllowAdd { get; set; } = true;
        public bool AllowEdit { get; set; } = true;
        public bool AllowDelete { get; set; } = true;
        public bool AllowInlineEdit { get; set; } = true;

        public int Count { get; protected set; }
        public int From { get; protected set; }
        public int To { get; protected set; }
        public int PageSize { get; set; } = 50;
        public RadzenGrid<TModel> RadzenGrid { get; set; }

        private string[] modelItems = null;
        public string[] ModelItems
        {
            get => modelItems ?? Array.Empty<string>();
            set => modelItems = value ?? Array.Empty<string>();
        }

        public TModel[] Models { get; protected set; }
        public TModel ExpandModel { get; protected set; }
        public TModel SelectedModel { get; protected set; }
        public TModel EditModel { get; protected set; }
        public TModel DeleteModel { get; protected set; }

        public bool HasRowDetail { get; set; } = true;
        public string AccessFilter { get; set; }
        protected virtual bool IsModelOrder(string orderBy)
        {
            return orderBy.HasContent() && ModelItems.Any(e => orderBy.Contains(e));
        }
        protected virtual string GetGridAccessFilter(string filter)
        {
            var result = new StringBuilder();

            foreach (var item in filter.Split("and"))
            {
                if (ModelItems.Any(e => item.Contains(e)) == false)
                {
                    if (result.Length > 0)
                        result.Append(" and ");

                    result.Append(item.Trim());
                }
            }
            return result.ToString();
        }
        protected virtual string GetGridModelFilter(string filter)
        {
            var result = new StringBuilder();

            foreach (var item in filter.Split("and"))
            {
                if (ModelItems.Any(e => item.Contains(e)))
                {
                    if (result.Length > 0)
                        result.Append(" and ");

                    var newFilter = item.Replace(".Contains", ".ToLower().Contains");
                    var tags = newFilter.GetAllTags(".Contains(", ")");

                    if (tags != null && tags.Count() == 1)
                    {
                        var tag = tags.ElementAt(0);

                        newFilter = tag.GetText($"{tag.InnerText}.ToLower()");
                    }
                    result.Append(newFilter.Trim());
                }
            }
            return result.ToString();
        }

        protected virtual bool ValidateModel(ModelObject model)
        {
            model.CheckArgument(nameof(model));

            var result = true;

            foreach (var item in model.Errors)
            {
                result = false;
                if (ShowNotification != null)
                {
                    var message = string.Format(Translate(item.Message), item.Parameters.Select(e => Translate(e.ToString())).ToArray());

                    ShowNotification(new NotificationMessage()
                    {
                        Severity = NotificationSeverity.Error,
                        Summary = Translate(item.Key),
                        Detail = message,
                        Duration = 4000
                    });
                }
            }
            foreach (var item in ModelValidator.Validate(model))
            {
                result = false;
                if (ShowNotification != null)
                {
                    var message = string.Format(Translate(item.Message), item.Parameters.Select(e => Translate(e.ToString())).ToArray());

                    ShowNotification(new NotificationMessage()
                    {
                        Severity = NotificationSeverity.Error,
                        Summary = Translate(item.Key),
                        Detail = message,
                        Duration = 4000
                    });
                }
            }
            return result;
        }

        protected Task InvokePageAsync(Action action)
        {
            return ModelPage.InvokePageAsync(action);
        }

        #region Model operations
        protected virtual TModel ToModel(TContract entity)
        {
            var handled = false;
            var model = new TModel();

            BeforeToModel(entity, model, ref handled);
            if (handled == false)
            {
                model.CopyProperties(entity);
            }
            AfterToModel(model);
            return model;
        }
        partial void BeforeToModel(TContract entity, TModel model, ref bool handled);
        partial void AfterToModel(TModel model);

        public Task ReloadDataAsync()
        {
            return RadzenGrid.Reload();
        }
        public async Task ReloadDataAsync(TModel model)
        {
            model.CheckArgument(nameof(model));

            await ReloadDataAsync().ConfigureAwait(false);

            var item = RadzenGrid.Data.FirstOrDefault(m => m.Id == model.Id);

            if (item != null)
            {
                await InvokePageAsync(() => RadzenGrid.SelectRow(item)).ConfigureAwait(false);
            }
        }
        public void ReloadModel(TModel model)
        {
            model.CheckArgument(nameof(model));

            var item = RadzenGrid.Data.FirstOrDefault(m => m.Id == model.Id);

            if (item != null)
            {
                item.CopyFrom(model);
            }
        }

        private volatile bool loadDataActive = false;
        public virtual async Task LoadDataAsync(LoadDataArgs args)
        {
            if (loadDataActive == false)
            {
                try
                {
                    var handled = default(bool);

                    loadDataActive = true;
                    PrepareLoadDataArgs(args);
                    BeforeLoadDataHandler?.Invoke(this, args);
                    handled = await BeforeLoadDataAsync(args).ConfigureAwait(false);
                    if (handled == false)
                    {
                        var pageIndex = args.Skip / args.Top;
                        var skip = pageIndex.GetValueOrDefault() * PageSize;
                        var orderBy = default(string);
                        var accessFilter = AccessFilter;
                        var modelFilter = default(string);

                        SelectedModel = null;
                        AdapterAccess.SessionToken = ModelPage.AuthorizationSession.Token;
                        if (string.IsNullOrWhiteSpace(args.Filter) == false)
                        {
                            var gridAccessFilter = GetGridAccessFilter(args.Filter);

                            if (gridAccessFilter.HasContent())
                            {
                                if (accessFilter.HasContent() == false)
                                {
                                    accessFilter = $"{gridAccessFilter}";
                                }
                                else
                                {
                                    accessFilter = $"{accessFilter} and {gridAccessFilter}";
                                }
                            }

                            modelFilter = GetGridModelFilter(args.Filter);
                        }
                        if (string.IsNullOrWhiteSpace(args.OrderBy) == false)
                        {
                            orderBy = args.OrderBy;
                        }
                        // A:
                        if (accessFilter.HasContent() == false
                          && modelFilter.HasContent() == false
                          && orderBy.HasContent() == false)
                        {
                            Count = await AdapterAccess.CountAsync()
                                           .ConfigureAwait(false);
                            var query = (await AdapterAccess.GetPageListAsync(pageIndex.GetValueOrDefault(), PageSize)
                                           .ConfigureAwait(false))
                                           .Select(e => ToModel(e))
                                           .ToArray();
                            var saveCount = query.Length;
                            query = await QueriedDataAsync(query).ConfigureAwait(false);
                            LoadModelDataHandler?.Invoke(this, query);
                            Models = query;
                            Count = Count + query.Length - saveCount;
                        }
                        // B:
                        else if (accessFilter.HasContent()
                             && modelFilter.HasContent() == false
                             && orderBy.HasContent() == false)
                        {
                            Count = await AdapterAccess.CountByAsync(accessFilter)
                                           .ConfigureAwait(false);
                            var query = (await AdapterAccess.QueryPageListAsync(accessFilter, pageIndex.GetValueOrDefault(), PageSize)
                                           .ConfigureAwait(false))
                                           .Select(e => ToModel(e))
                                           .ToArray();
                            var saveCount = query.Length;
                            query = await QueriedDataAsync(query).ConfigureAwait(false);
                            LoadModelDataHandler?.Invoke(this, query);
                            Models = query;
                            Count = Count + query.Length - saveCount;
                        }
                        // C:
                        else if (accessFilter.HasContent()
                             && modelFilter.HasContent() == false
                             && orderBy.HasContent()
                             && IsModelOrder(orderBy) == false)
                        {
                            Count = await AdapterAccess.CountByAsync(accessFilter)
                                           .ConfigureAwait(false);
                            var query = (await AdapterAccess.QueryPageListAsync(accessFilter, pageIndex.GetValueOrDefault(), PageSize)
                                           .ConfigureAwait(false))
                                           .Select(e => ToModel(e))
                                           .ToArray();
                            var saveCount = query.Length;
                            query = await QueriedDataAsync(query).ConfigureAwait(false);
                            LoadModelDataHandler?.Invoke(this, query);
                            Models = query.AsQueryable()
                                    .OrderBy(orderBy)
                                    .ToArray();
                            Count = Count + query.Length - saveCount;
                        }
                        // D:
                        else if (accessFilter.HasContent()
                             && modelFilter.HasContent() == false
                             && orderBy.HasContent()
                             && IsModelOrder(orderBy))
                        {
                            Count = await AdapterAccess.CountByAsync(accessFilter)
                                           .ConfigureAwait(false);
                            var query = (await AdapterAccess.QueryPageListAsync(accessFilter, pageIndex.GetValueOrDefault(), PageSize)
                                           .ConfigureAwait(false))
                                           .Select(e => ToModel(e))
                                           .ToArray();
                            var saveCount = query.Length;
                            query = await QueriedDataAsync(query).ConfigureAwait(false);
                            LoadModelDataHandler?.Invoke(this, query);
                            Models = query.AsQueryable()
                                    .OrderBy(orderBy)
                                    .ToArray();
                            Count = Count + query.Length - saveCount;
                        }
                        // E:
                        else if (accessFilter.HasContent()
                             && modelFilter.HasContent()
                             && orderBy.HasContent() == false)
                        {
                            var query = (await AdapterAccess.QueryAllAsync(accessFilter)
                                            .ConfigureAwait(false))
                                            .Select(e => ToModel(e))
                                            .ToArray();
                            query = await QueriedDataAsync(query).ConfigureAwait(false);
                            LoadModelDataHandler?.Invoke(this, query);
                            var modelQuery = query.AsQueryable()
                                        .Where(modelFilter);
                            Count = modelQuery.Count();
                            Models = modelQuery.Skip(skip).Take(PageSize).ToArray();
                        }
                        // F:
                        else if (accessFilter.HasContent()
                             && modelFilter.HasContent()
                             && orderBy.HasContent()
                             && IsModelOrder(orderBy) == false)
                        {
                            var query = (await AdapterAccess.QueryAllAsync(accessFilter)
                                            .ConfigureAwait(false))
                                            .Select(e => ToModel(e))
                                            .ToArray();
                            query = await QueriedDataAsync(query).ConfigureAwait(false);
                            LoadModelDataHandler?.Invoke(this, query);
                            var modelQuery = query.AsQueryable()
                                        .Where(modelFilter)
                                        .OrderBy(orderBy);
                            Count = modelQuery.Count();
                            Models = modelQuery.Skip(skip).Take(PageSize).ToArray();
                        }
                        // G:
                        else if (accessFilter.HasContent()
                             && modelFilter.HasContent()
                             && orderBy.HasContent()
                             && IsModelOrder(orderBy))
                        {
                            var query = (await AdapterAccess.QueryAllAsync(accessFilter)
                                            .ConfigureAwait(false))
                                            .Select(e => ToModel(e))
                                            .ToArray();
                            query = await QueriedDataAsync(query).ConfigureAwait(false);
                            LoadModelDataHandler?.Invoke(this, query);
                            var modelQuery = query.AsQueryable()
                                        .Where(modelFilter)
                                        .OrderBy(orderBy);
                            Count = modelQuery.Count();
                            Models = modelQuery.Skip(skip).Take(PageSize).ToArray();
                        }
                        // H:
                        else if (accessFilter.HasContent() == false
                             && modelFilter.HasContent()
                             && orderBy.HasContent() == false)
                        {
                            var query = (await AdapterAccess.GetAllAsync()
                                            .ConfigureAwait(false))
                                            .Select(e => ToModel(e))
                                            .ToArray();
                            query = await QueriedDataAsync(query).ConfigureAwait(false);
                            LoadModelDataHandler?.Invoke(this, query);
                            var modelQuery = query.AsQueryable()
                                        .Where(modelFilter);
                            Count = modelQuery.Count();
                            Models = modelQuery.Skip(skip).Take(PageSize).ToArray();
                        }
                        // I:
                        else if (accessFilter.HasContent() == false
                             && modelFilter.HasContent()
                             && orderBy.HasContent()
                             && IsModelOrder(orderBy) == false)
                        {
                            var query = (await AdapterAccess.GetAllAsync()
                                            .ConfigureAwait(false))
                                            .Select(e => ToModel(e))
                                            .ToArray();
                            query = await QueriedDataAsync(query).ConfigureAwait(false);
                            LoadModelDataHandler?.Invoke(this, query);
                            var modelQuery = query.AsQueryable()
                                        .Where(modelFilter)
                                        .OrderBy(orderBy);
                            Count = modelQuery.Count();
                            Models = modelQuery.Skip(skip).Take(PageSize).ToArray();
                        }
                        // J:
                        else if (accessFilter.HasContent() == false
                             && modelFilter.HasContent()
                             && orderBy.HasContent()
                             && IsModelOrder(orderBy))
                        {
                            var query = (await AdapterAccess.GetAllAsync()
                                            .ConfigureAwait(false))
                                            .Select(e => ToModel(e))
                                            .ToArray();
                            query = await QueriedDataAsync(query).ConfigureAwait(false);
                            LoadModelDataHandler?.Invoke(this, query);
                            var modelQuery = query.AsQueryable()
                                        .Where(modelFilter)
                                        .OrderBy(orderBy);
                            Count = modelQuery.Count();
                            Models = modelQuery.Skip(skip).Take(PageSize).ToArray();
                        }
                        // K:
                        else if (accessFilter.HasContent() == false
                             && modelFilter.HasContent() == false
                             && orderBy.HasContent()
                             && IsModelOrder(orderBy) == false)
                        {
                            var query = (await AdapterAccess.GetAllAsync()
                                             .ConfigureAwait(false))
                                             .Select(e => ToModel(e))
                                             .ToArray();
                            query = await QueriedDataAsync(query).ConfigureAwait(false);
                            LoadModelDataHandler?.Invoke(this, query);
                            var modelQuery = query.AsQueryable()
                                        .OrderBy(orderBy);
                            Count = modelQuery.Count();
                            Models = modelQuery.Skip(skip).Take(PageSize).ToArray();
                        }
                        // L:
                        else if (accessFilter.HasContent() == false
                             && modelFilter.HasContent() == false
                             && orderBy.HasContent()
                             && IsModelOrder(orderBy))
                        {
                            var query = (await AdapterAccess.GetAllAsync()
                                            .ConfigureAwait(false))
                                            .Select(e => ToModel(e))
                                            .ToArray();
                            query = await QueriedDataAsync(query).ConfigureAwait(false);
                            LoadModelDataHandler?.Invoke(this, query);
                            var modelQuery = query.AsQueryable()
                                        .OrderBy(orderBy);
                            Count = modelQuery.Count();
                            Models = modelQuery.Skip(skip).Take(PageSize).ToArray();
                        }

                        if (Count > 0)
                        {
                            From = pageIndex.Value * PageSize + 1;
                            To = (From + PageSize) > Count ? Count : (From + PageSize);
                        }
                        else
                        {
                            From = 0;
                            To = 0;
                        }
                    }
                    await AfterLoadDataAsync(args).ConfigureAwait(false);
                }
                catch (System.Exception ex)
                {
                    ShowException($"Error {nameof(LoadDataAsync)}", ex);
                }
                finally
                {
                    loadDataActive = false;
                }
            }
        }
        protected virtual void PrepareLoadDataArgs(LoadDataArgs args)
        {
        }
        protected virtual Task<bool> BeforeLoadDataAsync(LoadDataArgs args)
        {
            return Task.FromResult(false);
        }
        protected virtual Task<TModel[]> QueriedDataAsync(TModel[] models)
        {
            return Task.FromResult(models);
        }
        protected virtual Task AfterLoadDataAsync(LoadDataArgs args)
        {
            return Task.FromResult(0);
        }
        #endregion Model operations

        public virtual void ValueChanged(TModel item)
        {
            var handled = false;

            BeforeValueChanged(item, ref handled);
            if (handled == false)
            {
            }
            AfterValueChanged(item);
        }
        partial void BeforeValueChanged(TModel item, ref bool handled);
        partial void AfterValueChanged(TModel item);

        public virtual void RowRender(RowRenderEventArgs<TModel> args)
        {
            var handled = false;

            BeforeRowRender(args, ref handled);
            if (handled == false)
            {
                args.Expandable = HasRowDetail;
            }
            AfterRowRender(args);
        }
        partial void BeforeRowRender(RowRenderEventArgs<TModel> args, ref bool handled);
        partial void AfterRowRender(RowRenderEventArgs<TModel> args);

        public virtual void RowCollapse(TModel item)
        {
            bool handled = false;

            BeforeRowCollapse(item, ref handled);
            if (handled == false)
            {
                BeforeRowCollapseHandler?.Invoke(this, item);
                ExpandModel = null;
                AfterRowCollapseHandler?.Invoke(this, item);
            }
            AfterRowCollapse(item);
        }
        partial void BeforeRowCollapse(TModel item, ref bool handled);
        partial void AfterRowCollapse(TModel item);

        public virtual void RowExpand(TModel item)
        {
            var handled = false;

            BeforeRowExpand(item, ref handled);
            if (handled == false)
            {
                BeforeRowExpandHandler?.Invoke(this, item);
                ExpandModel = item;
                AfterRowExpandHandler?.Invoke(this, item);
            }
            AfterRowExpand(item);
        }
        partial void BeforeRowExpand(TModel item, ref bool handled);
        partial void AfterRowExpand(TModel item);

        public virtual Task RowSelectedAsync(TModel item)
        {
            return InvokePageAsync(() =>
            {
                var handled = false;

                BeforeRowSelected(item, ref handled);
                if (handled == false)
                {
                    BeforeRowSelectedHandler?.Invoke(this, item);
                    SelectedModel ??= new TModel();
                    SelectedModel.CopyProperties(item);
                }
                AfterRowSelectedHandler?.Invoke(this, SelectedModel);
                AfterRowSelected(item);
            });
        }
        partial void BeforeRowSelected(TModel item, ref bool handled);
        partial void AfterRowSelected(TModel item);

        public virtual async Task RowDoubleClickAsync(TModel item)
        {
            var handled = false;

            BeforeRowDoubleClick(item, ref handled);
            if (handled == false && AllowEdit && EditModel == null)
            {
                try
                {
                    EditModel = new TModel();

                    if (item.Id > 0)
                    {
                        var entity = await AdapterAccess.GetByIdAsync(item.Id).ConfigureAwait(false);

                        EditModel.CopyProperties(entity);
                    }
                    else
                    {
                        EditModel.CopyProperties(item);
                    }
                    BeforeEditModelHandler?.Invoke(this, EditModel);
                    LoadModelDataHandler?.Invoke(this, new[] { EditModel });
                    if (ShowEditItemAsync != null)
                    {
                        BeforeShowEditItem(EditModel);
                        await ShowEditItemAsync().ConfigureAwait(false);
                    }
                }
                catch (System.Exception ex)
                {
                    ShowException("Error update", ex);
                }
            }
            AfterRowDoubleClick(item);
        }
        partial void BeforeRowDoubleClick(TModel item, ref bool handled);
        partial void AfterRowDoubleClick(TModel item);

        #region Dialog operations
        public virtual async Task AddItemAsync()
        {
            var handled = false;

            BeforeAddItem(ref handled);
            if (handled == false && AllowAdd && EditModel == null)
            {
                EditModel = await CreateModelAsync();
                AfterCreateModelHandler?.Invoke(this, EditModel);
                LoadModelDataHandler?.Invoke(this, new[] { EditModel });
                if (ShowEditItemAsync != null)
                {
                    BeforeShowEditItem(EditModel);
                    await ShowEditItemAsync().ConfigureAwait(false);
                }
            }
            AfterEditModelHandler?.Invoke(this, EditModel);
            AfterAddItem(EditModel);
        }
        protected virtual void BeforeAddItem(ref bool handled)
        {
        }
        protected virtual void BeforeShowEditItem(TModel item)
        {
        }
        protected virtual void AfterAddItem(TModel item)
        {
        }

        public virtual async Task SubmitEditAsync(DialogService dialogService)
        {
            var saved = false;
            var handled = false;

            BeforeSubmitEdit(EditModel, ref handled);
            if (handled == false)
            {
                try
                {
                    if (ValidateModel(EditModel))
                    {
                        if (EditModel.Id == 0)
                        {
                            await InsertModelAsync(EditModel).ConfigureAwait(false);
                        }
                        else
                        {
                            await UpdateModelAsync(EditModel).ConfigureAwait(false);
                        }
                        dialogService.Close();
                        saved = true;
                    }
                }
                catch (System.Exception ex)
                {
                    ShowException(EditModel.Id == 0 ? "Error create" : "Error update", ex);
                }
            }
            AfterSubmitEdit(EditModel);
            EditModel = saved ? null : EditModel;
        }
        protected virtual void BeforeSubmitEdit(TModel item, ref bool handled)
        {
        }
        protected virtual void AfterSubmitEdit(TModel item)
        {
        }

        public void CancelEditDialog(DialogService dialogService)
        {
            dialogService.Close();
            EditModel = null;
        }

        public virtual async Task ConfirmDeleteItemAsync(DialogService dialogService)
        {
            var handled = false;

            BeforeConfirmDeleteItem(DeleteModel, ref handled);
            if (handled == false)
            {
                try
                {
                    await DeleteModelAsync(DeleteModel).ConfigureAwait(false);
                }
                catch (System.Exception ex)
                {
                    ShowException("Error delete", ex);
                }
                finally
                {
                    dialogService.Close();
                }
            }
            AfterConfirmDeleteItem(DeleteModel);
            DeleteModel = null;
        }
        protected virtual void BeforeConfirmDeleteItem(TModel item, ref bool handled)
        {
        }
        protected virtual void AfterConfirmDeleteItem(TModel item)
        {
        }

        public void CancelDeleteDialog(DialogService dialogService)
        {
            dialogService.Close();
            DeleteModel = null;
        }

        public void OnCloseDialog(dynamic result)
        {
            EditModel = null;
            DeleteModel = null;
        }
        #endregion Dialog operations

        #region Model operations
        protected virtual async Task<TModel> CreateModelAsync()
        {
            var result = new TModel();
            var entity = await AdapterAccess.CreateAsync().ConfigureAwait(false);

            result.CopyProperties(entity);

            return result;
        }
        public virtual async Task InsertModelAsync(TModel model)
        {
            var handled = false;

            BeforeInsertModel(model, ref handled);
            if (handled == false)
            {
                BeforeInsertModelHandler?.Invoke(this, model);
                if (ValidateModel(model))
                {
                    var curItem = await AdapterAccess.InsertAsync(model).ConfigureAwait(false);

                    model.CopyProperties(curItem);
                }
            }
            AfterInsertModelHandler?.Invoke(this, model);
            AfterInsertModel(model);
            await InvokePageAsync(async () => await ReloadDataAsync(model).ConfigureAwait(false)).ConfigureAwait(false);
        }
        protected virtual void BeforeInsertModel(TModel model, ref bool handled)
        {
        }
        protected virtual void AfterInsertModel(TModel model)
        {
        }

        public virtual async Task UpdateModelAsync(TModel model)
        {
            var handled = false;

            BeforeUpdateModel(model, ref handled);
            if (handled == false)
            {
                BeforeUpdateModelHandler?.Invoke(this, model);
                if (ValidateModel(model))
                {
                    var curItem = await AdapterAccess.UpdateAsync(model).ConfigureAwait(false);

                    model.CopyProperties(curItem);
                    LoadModelDataHandler?.Invoke(this, new[] { model });
                }
            }
            AfterUpdateModelHandler?.Invoke(this, model);
            AfterUpdateModel(model);
            await InvokePageAsync(() => ReloadModel(model)).ConfigureAwait(false);
        }
        protected virtual void BeforeUpdateModel(TModel model, ref bool handled)
        {
        }
        protected virtual void AfterUpdateModel(TModel model)
        {
        }

        public virtual async Task DeleteModelAsync(TModel model)
        {
            var handeld = false;

            BeforeDeleteModel(model, ref handeld);
            if (handeld == false)
            {
                BeforeDeleteModelHandler?.Invoke(this, model);
                await AdapterAccess.DeleteAsync(model.Id).ConfigureAwait(false);
            }
            AfterDeleteModelHandler?.Invoke(this, model);
            AfterDelteModel(model);
            await InvokePageAsync(() => RadzenGrid.Reload()).ConfigureAwait(false);
        }
        protected virtual void BeforeDeleteModel(TModel model, ref bool handled)
        {
        }
        protected virtual void AfterDelteModel(TModel model)
        {
        }
        #endregion Model operations

        #region DataGridColumns operations
        public virtual async Task EditRowAsync(TModel item)
        {
            var handled = false;
            BeforeEditRow(item, ref handled);
            if (handled == false && AllowEdit && EditModel == null)
            {
                EditModel = new TModel();
                EditModel.CopyProperties(item);

                if (AllowInlineEdit)
                {
                    await RadzenGrid.EditRow(item).ConfigureAwait(false);
                }
                else
                {
                    if (ShowEditItemAsync != null)
                    {
                        await ShowEditItemAsync().ConfigureAwait(false);
                    }
                }
            }
            AfterEditRow(item);
        }
        partial void BeforeEditRow(TModel item, ref bool handled);
        partial void AfterEditRow(TModel item);

        public virtual async Task UpdateRowAsync(TModel item)
        {
            var handled = false;

            BeforeUpdateRow(item, ref handled);
            if (handled == false)
            {
                try
                {
                    if (EditModel.Id == 0)
                    {
                        await InsertModelAsync(item).ConfigureAwait(false);
                    }
                    else
                    {
                        await UpdateModelAsync(item).ConfigureAwait(false);
                    }
                }
                catch (System.Exception ex)
                {
                    ShowException("Error update", ex);
                }
                EditModel = null;
            }
            AfterUpdateRow(item);
        }
        partial void BeforeUpdateRow(TModel item, ref bool handled);
        partial void AfterUpdateRow(TModel item);

        public virtual async Task DeleteRowAsync(TModel model)
        {
            var handeld = false;

            BeforeDeleteRow(model, ref handeld);
            if (handeld == false)
            {

                DeleteModel = new TModel();
                DeleteModel.CopyFrom(model);
                if (ShowConfirmDeleteAsync != null)
                {
                    await ShowConfirmDeleteAsync().ConfigureAwait(false);
                }
            }
            AfterDelteRow(model);
        }
        partial void BeforeDeleteRow(TModel model, ref bool handled);
        partial void AfterDelteRow(TModel model);

        public virtual void CommitEditRow(TModel item)
        {
            var handled = false;

            BeforeCommitEditRow(item, ref handled);
            if (handled == false)
            {
                RadzenGrid.UpdateRow(item);
            }
            AfterCommitEditRow(item);
        }
        partial void BeforeCommitEditRow(TModel item, ref bool handled);
        partial void AfterCommitEditRow(TModel item);

        public virtual void CancelEditRow(TModel item)
        {
            var handled = false;

            BeforeCancelEditRow(item, ref handled);
            if (handled == false)
            {
                if (EditModel != null)
                {
                    item.CopyProperties(EditModel);
                }
                RadzenGrid.CancelEditRow(item);
                EditModel = null;
            }
            AfterCancelEditRow(item);
        }
        partial void BeforeCancelEditRow(TModel item, ref bool handled);
        partial void AfterCancelEditRow(TModel item);
        #endregion DataGridColumns operations

        private void ShowException(string title, System.Exception exception)
        {
            if (ShowError(title, GetExceptionError(exception)) == false)
                throw exception;
        }
        private bool ShowError(string title, string message)
        {
            var result = ShowNotification != null;

            ShowNotification?.Invoke(new NotificationMessage()
            {
                Severity = NotificationSeverity.Error,
                Summary = Translate(title),
                Detail = message,
                Duration = 4000
            });
            return result;
        }
    }
}
//MdEnd
