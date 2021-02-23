//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
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

namespace QnSTradingCompany.BlazorApp.Pages
{
    public partial class CommonPage : ComponentBase, IDisposable
    {
        [Inject]
        public IAccountService AccountService { get; private set; }
        [Inject]
        public ISettingService Settings { get; private set; }
        [Inject]
        public ITranslatorService Translator { get; private set; }
        [Inject]
        public NotificationService NotificationService
        {
            get;
            private set;
        }
        [Inject]
        protected NavigationManager NavigationManager { get; private set; }
        [Inject]
        protected IProtectedBrowserStorage ProtectedBrowserStore { get; private set; }

        public AuthorizationSession AuthorizationSession
        {
            get => AccountService.CurrentAuthorizationSession;
        }

        protected bool CanRender { get; set; }
        public virtual string PageName => GetType().Name.Replace("Page", string.Empty);
        public virtual string ForPrefix => PageName;

        public virtual string Translate(string key) => Translator.Translate(key);
        public virtual string Translate(string key, string defaultValue) => Translator.Translate(key, defaultValue);
        public virtual string TranslateFor(string key) => Translator.Translate($"{ForPrefix}.{key}");
        public virtual string TranslateFor(string key, string defaultValue) => Translator.Translate($"{ForPrefix}.{key}", defaultValue);

        public virtual void ReloadSetting()
        {
            Settings.Reload();
        }
        public virtual void ReloadTranslator()
        {
            Translator.Reload();
        }

        protected bool HasLocationChanged { get; private set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();

            NavigationManager.LocationChanged += NavigationManager_LocationChanged;
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


        protected async Task ClearPageHistoryAsync()
        {
            var localSessionHistory = await ProtectedBrowserStore.GetAsync<SessionHistory>(StaticLiterals.SessionHistoryKey).ConfigureAwait(false);

            if (localSessionHistory != null)
            {
                localSessionHistory.ClearHistory();
                await ProtectedBrowserStore.SetAsync(StaticLiterals.SessionHistoryKey, localSessionHistory).ConfigureAwait(false);
            }
        }
        protected async Task ClearPageBeforeLoginAsync()
        {
            await ProtectedBrowserStore.SetAsync(StaticLiterals.BeforeLoginPageKey, string.Empty).ConfigureAwait(false);
        }
        protected async Task SetPageBeforeLoginAsync(string pageName)
        {
            await ProtectedBrowserStore.SetAsync(StaticLiterals.BeforeLoginPageKey, pageName).ConfigureAwait(false);
        }
        protected async Task<string> GetPageBeforeLoginAsync()
        {
            var result = await ProtectedBrowserStore.GetAsync<string>(StaticLiterals.BeforeLoginPageKey).ConfigureAwait(false);

            if (string.IsNullOrEmpty(result))
            {
                result = "/";
            }
            else if (result.StartsWith("/") == false)
            {
                result = $"/{result}";
            }
            return result;
        }
        private async void NavigationManager_LocationChanged(object sender, LocationChangedEventArgs e)
        {
            var shortLocationUrl = e.Location.Replace(NavigationManager.BaseUri, "/");
            var localSessionHistory = await ProtectedBrowserStore.GetAsync<SessionHistory>(StaticLiterals.SessionHistoryKey).ConfigureAwait(false);

            if (localSessionHistory == null)
            {
                localSessionHistory = new SessionHistory();
            }

            if (localSessionHistory.PeekHistory().Equals(shortLocationUrl) == false)
            {
                localSessionHistory.PushHistory(shortLocationUrl.Equals($"/{StaticLiterals.LoginPage}") ? "/" : shortLocationUrl);
                await ProtectedBrowserStore.SetAsync(StaticLiterals.SessionHistoryKey, localSessionHistory).ConfigureAwait(false);
                HasLocationChanged = true;
            }
        }
        private async void AccountService_AuthorizationChanged(object sender, AuthorizationSession e)
        {
            if (e == null)
            {
                await SetPageBeforeLoginAsync(PageName).ConfigureAwait(false);
                NavigationManager.NavigateTo($"/{StaticLiterals.LoginPage}");
            }
            else
            {
                await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
            }
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
                    NavigationManager.LocationChanged -= NavigationManager_LocationChanged;
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
