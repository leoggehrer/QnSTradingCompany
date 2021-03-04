//@QnSCodeCopy
//MdStart
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using QnSTradingCompany.BlazorApp.Models.Modules.Session;
using QnSTradingCompany.BlazorApp.Shared.Components;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Pages
{
	public partial class CommonPage : CommonComponent
	{
		public virtual string PageName => GetType().Name.Replace("Page", string.Empty);
		protected bool HasLocationChanged { get; private set; }

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
		protected override async void HandleLocationChanged(object sender, LocationChangedEventArgs e)
		{
			base.HandleLocationChanged(sender, e);

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
		protected override async void HandleAuthorizationChanged(object sender, AuthorizationSession e)
		{
			base.HandleAuthorizationChanged(sender, e);

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
	}
}
//MdEnd
