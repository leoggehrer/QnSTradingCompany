//@QnSCodeCopy
//MdStart
using Microsoft.AspNetCore.Components;
using QnSTradingCompany.BlazorApp.Services;
using Radzen;
using System;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class EditFormComponent<TContract, TModel> : DisplayComponent
        where TContract : Contracts.IIdentifiable, Contracts.ICopyable<TContract>
        where TModel : Models.ModelObject, TContract, new()
    {
        private TModel editModel;

        [Inject]
        public IServiceAdapter ServiceAdapter { get; init; }

        [Parameter]
        public TModel EditModel 
        {
            get => editModel;
            set
            {
                editModel = value;
                editModel?.BeforeDisplay();
                editModel?.BeforeEdit();
            }
        }
        public bool HasHeader { get; set; }
        public bool HasFooter { get; set; } = true;

        public EditFormComponent()
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();

        public virtual async void SubmitChangesAsync()
        {
            var handled = false;

            BeforeSubmitChanges(EditModel, ref handled);
            if (handled == false)
            {
                EditModel?.BeforeSave();
                try
                {
                    using var ctrl = ServiceAdapter.Create<TContract>(AuthorizationSession.Token);

                    if (EditModel.Id == 0)
                    {
                        var entity = await ctrl.InsertAsync(EditModel).ConfigureAwait(false);

                        EditModel.CopyProperties(entity);
                    }
                    else
                    {
                        var entity = await ctrl.UpdateAsync(EditModel).ConfigureAwait(false);

                        EditModel.CopyProperties(entity);
                    }
                }
                catch (Exception ex)
                {
                    ShowException(EditModel.Id == 0 ? "Error create" : "Error update", ex);
                }
            }
            EditModel?.AfterSave();
            AfterSubmitChanges(EditModel);
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
            await ParentComponent.InvokePageAsync(() => StateHasChanged()).ConfigureAwait(false);
        }
        partial void BeforeSubmitChanges(TModel editModel, ref bool handled);
        partial void AfterSubmitChanges(TModel editModel);
        public async void CancelChanges()
        {
            EditModel?.CancelEdit();
            if (EditModel.Id == 0)
            {
                EditModel = new TModel();
            }
            else
            {
                using var ctrl = ServiceAdapter.Create<TContract>(AuthorizationSession.Token);
                var entity = await ctrl.GetByIdAsync(EditModel.Id).ConfigureAwait(false);

                EditModel = new TModel();
                EditModel.CopyProperties(entity);
            }
        }

        protected void ShowException(string title, System.Exception exception)
        {
            ShowError(title, GetExceptionError(exception));
        }
        protected void ShowError(string title, string message)
        {
            NotificationService.Notify(new NotificationMessage()
            {
                Severity = NotificationSeverity.Error,
                Summary = Translate(title),
                Detail = message,
                Duration = 4000
            });
        }
    }
}
//MdEnd
