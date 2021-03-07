//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using CommonBase.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using QnSTradingCompany.BlazorApp.Models.Modules.Session;
using QnSTradingCompany.BlazorApp.Modules.SessionStorage;
using QnSTradingCompany.BlazorApp.Services.Modules.Authentication;
using QnSTradingCompany.BlazorApp.Services.Modules.Configuration;
using QnSTradingCompany.BlazorApp.Services.Modules.Language;
using Radzen;
using System;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class CommonComponent : ComponentBase, IDisposable
    {
        [Inject]
        protected IAccountService AccountService { get; init; }
        [Inject]
        public ISettingService Settings { get; init; }
        [Inject]
        public ITranslatorService Translator { get; init; }
        [Inject]
        public NavigationManager NavigationManager { get; init; }
        [Inject]
        public NotificationService NotificationService { get; init; }
        [Inject]
        protected IProtectedBrowserStorage ProtectedBrowserStore { get; set; }

        public AuthorizationSession AuthorizationSession
        {
            get => AccountService.CurrentAuthorizationSession;
        }

        protected bool CanRender { get; set; }
        public virtual string ComponentName => GetType().Name;
        public virtual string ForPrefix => ComponentName;

        public virtual string Translate(string key) => Translator.Translate(key);
        public virtual string Translate(string key, string defaultValue) => Translator.Translate(key, defaultValue);
        public virtual string TranslateFor(string key) => Translator.Translate($"{ForPrefix}.{key}");
        public virtual string TranslateFor(string key, string defaultValue) => Translator.Translate($"{ForPrefix}.{key}", defaultValue);

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                NavigationManager.LocationChanged += HandleLocationChanged;
                AccountService.AuthorizationChanged += HandleAuthorizationChanged;
                AccountService.InitAuthorizationSession();
                await StartedFirstRenderAsync().ConfigureAwait(false);
                await OnFirstRenderAsync().ConfigureAwait(false);
                CanRender = true;
                await FinishedFirstRenderAsync().ConfigureAwait(false);
                await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
            }
            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(false);
        }
        protected virtual Task StartedFirstRenderAsync() => Task.FromResult(0);
        protected virtual Task OnFirstRenderAsync() => Task.FromResult(0);
        protected virtual Task FinishedFirstRenderAsync() => Task.FromResult(0);

        public CommonComponent()
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();

        protected virtual void HandleLocationChanged(object sender, LocationChangedEventArgs e)
        {
        }
        protected virtual void HandleAuthorizationChanged(object sender, AuthorizationSession e)
        {
        }

        public virtual void ReloadSetting()
        {
            Settings.Reload();
        }
        public virtual void ReloadTranslator()
        {
            Translator.Reload();
        }

        public Task InvokePageAsync(Action action)
        {
            action.CheckArgument(nameof(action));

            return InvokeAsync(() => action());
        }

        protected static string GetExceptionError(Exception source)
        {
            source.CheckArgument(nameof(source));

            string tab = string.Empty;
            string errMsg = source.Message;
            Exception innerException = source.InnerException;

            while (innerException != null)
            {
                tab += "\t";
                errMsg = $"{errMsg}{Environment.NewLine}{tab}{innerException.Message}";
                innerException = innerException.InnerException;
            }
            return errMsg;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue == false)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    NavigationManager.LocationChanged -= HandleLocationChanged;
                    AccountService.AuthorizationChanged -= HandleAuthorizationChanged;
                    DisposeHelper.DisposeMembers(this);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CommonPage()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
//MdEnd
