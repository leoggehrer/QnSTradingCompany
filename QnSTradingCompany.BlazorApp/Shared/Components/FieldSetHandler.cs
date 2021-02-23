//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using CommonBase.Validator;
using QnSTradingCompany.BlazorApp.Models;
using Radzen;
using Radzen.Blazor;
using System;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class FieldSetHandler<TContract, TModel> : ComponentHandler
        where TContract : Contracts.IIdentifiable, Contracts.ICopyable<TContract>
        where TModel : ModelObject, TContract, new()
    {
        public RadzenTemplateForm<TModel> Form { get; set; }
        public int Id { get; set; }
        public Pages.ModelPage ModelPage { get; private set; }
        public Contracts.Client.IAdapterAccess<TContract> AdapterAccess { get; private set; }

        public virtual string ForPrefix => typeof(TModel).Name;
        public Func<string, string> Translate { get; }
        public string TranslateFor(string key) => Translate($"{ForPrefix}.{key}");
        public Action<NotificationMessage> ShowNotification { get; set; }

        public FieldSetHandler(Pages.ModelPage modelPage, Contracts.Client.IAdapterAccess<TContract> adapterAccess)
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

        public bool AllowEdit { get; private set; } = true;

        public TModel EditModel { get; private set; }

        protected virtual void ValidateModel(object obj)
        {
            ModelValidator.Validate(obj);
        }

        protected virtual TModel ToModel(TContract entity)
        {
            var handled = false;
            var model = new TModel();

            BeforeToModel(entity, model, ref handled);
            if (handled == false)
            {
                model.CopyProperties(entity);
                UpdateReferences(model);
            }
            AfterToModel(model);
            return model;
        }
        protected virtual void BeforeToModel(TContract entity, TModel model, ref bool handled)
        {
        }
        protected virtual void UpdateReferences(TModel model)
        {
        }
        protected virtual void AfterToModel(TModel model)
        {
        }

        private volatile bool loadDataActive = false;
        public async Task LoadDataAsync()
        {
            if (loadDataActive == false)
            {
                var handled = false;

                loadDataActive = true;
                PrepareLoadDataArgs();
                BeforeLoadData(ref handled);
                if (handled == false)
                {
                    EditModel ??= new TModel();
                    AdapterAccess.SessionToken = ModelPage.AuthorizationSession.Token;
                    if (Id == 0)
                    {
                        var entity = await AdapterAccess.CreateAsync().ConfigureAwait(false);

                        EditModel.CopyProperties(entity);
                    }
                    else
                    {
                        var entity = await AdapterAccess.GetByIdAsync(Id).ConfigureAwait(false);

                        EditModel.CopyProperties(entity);
                    }
                }
                AfterLoadData();
                loadDataActive = false;
            }
        }
        protected virtual void PrepareLoadDataArgs()
        {
        }
        protected virtual void BeforeLoadData(ref bool handled)
        {
        }
        protected virtual void AfterLoadData()
        {
        }

        public virtual async Task SubmitEditAsync()
        {
            var handled = false;

            BeforeSubmitItem(EditModel, ref handled);
            if (handled == false)
            {
                try
                {
                    ValidateModel(EditModel);
                    if (EditModel.Id == 0)
                    {
                        await InsertModelAsync(EditModel).ConfigureAwait(false);
                    }
                    else
                    {
                        await UpdateModelAsync(EditModel).ConfigureAwait(false);
                    }
                }
                catch (Exception ex)
                {
                    if (ShowNotification != null)
                    {
                        ShowNotification(new NotificationMessage()
                        {
                            Severity = NotificationSeverity.Error,
                            Summary = Translate(EditModel.Id == 0 ? "Error create" : "Error update"),
                            Detail = GetExceptionError(ex),
                            Duration = 4000
                        });
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            AfterSubmitItem(EditModel);
        }
        protected virtual void BeforeSubmitItem(TModel item, ref bool handled)
        {
        }
        protected virtual void AfterSubmitItem(TModel item)
        {
        }

        public virtual async Task CancelEditAsync()
        {
            var handled = false;

            BeforeCancelEdit(EditModel, ref handled);
            if (handled == false)
            {
                await LoadDataAsync().ConfigureAwait(false);
            }
            AfterCancelEdit(EditModel);
        }
        protected virtual void BeforeCancelEdit(TModel model, ref bool handled)
        {
        }
        protected virtual void AfterCancelEdit(TModel model)
        {
        }

        public virtual async Task InsertModelAsync(TModel model)
        {
            var handled = false;

            BeforeInsertModel(model, ref handled);
            if (handled == false)
            {
                ValidateModel(model);
                var curItem = await AdapterAccess.InsertAsync(model).ConfigureAwait(false);
                model.CopyProperties(curItem);
                UpdateReferences(model);
            }
            AfterInsertModel(model);
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
                ValidateModel(model);
                var curItem = await AdapterAccess.UpdateAsync(model).ConfigureAwait(false);
                model.CopyProperties(curItem);
                UpdateReferences(model);
            }
            AfterUpdateModel(model);
        }
        protected virtual void BeforeUpdateModel(TModel model, ref bool handled)
        {
        }
        protected virtual void AfterUpdateModel(TModel model)
        {
        }
    }
}
//MdEnd
