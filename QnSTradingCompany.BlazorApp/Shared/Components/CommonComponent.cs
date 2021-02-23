//@QnSCodeCopy
//MdStart
using Microsoft.AspNetCore.Components;
using QnSTradingCompany.BlazorApp.Models.Modules.Session;
using QnSTradingCompany.BlazorApp.Modules.SessionStorage;
using QnSTradingCompany.BlazorApp.Services.Modules.Authentication;
using QnSTradingCompany.BlazorApp.Services.Modules.Configuration;
using QnSTradingCompany.BlazorApp.Services.Modules.Language;
using System;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class CommonComponent : ComponentBase, IDisposable
    {
        [Inject]
        protected IAccountService AccountService { get; private set; }
        [Inject]
        protected ISettingService Settings { get; private set; }
        [Inject]
        protected ITranslatorService Translator { get; private set; }
        [Inject]
        protected IProtectedBrowserStorage ProtectedBrowserStore { get; set; }

        protected AuthorizationSession AuthorizationSession
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

        protected override void OnInitialized()
        {
            base.OnInitialized();

            AccountService.AuthorizationChanged += AccountService_AuthorizationChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(false);

            if (firstRender)
            {
                AccountService.InitAuthorizationSession();
                await BeforeFirstRenderAsync().ConfigureAwait(false);
                CanRender = true;
                await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
                await AfterFirstRenderAsync().ConfigureAwait(false);
            }
        }
        protected virtual Task BeforeFirstRenderAsync()
        {
            return Task.FromResult(0);
        }
        protected virtual Task AfterFirstRenderAsync()
        {
            return Task.FromResult(0);
        }

        protected virtual void AccountService_AuthorizationChanged(object sender, AuthorizationSession e)
        {
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
                    AccountService.AuthorizationChanged -= AccountService_AuthorizationChanged;
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
