//@QnSCodeCopy

using QnSTradingCompany.BlazorApp.Models.Modules.Menu;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
	public partial class NavMenu
	{
		protected override void CreateMenu()
		{
			base.CreateMenu();
			if (AuthorizationSession != null)
			{
				CreateNavMenu();
			}
			else
			{
				InitNavMenu();
			}
		}
		private void InitNavMenu()
		{
			var handled = false;

			BeforeInitNavMenu(ref handled);
			if (handled == false)
			{
				MenuItems.Clear();
				var homeMenu = new MenuItem
				{
					Text = "Home",
					Value = "home",
					Path = "/",
					Icon = "home",
					Order = 1
				};
				MenuItems.Add(homeMenu);
			}
			AfterInitNavMenu();
		}
		partial void BeforeInitNavMenu(ref bool handled);
		partial void AfterInitNavMenu();

		private void CreateNavMenu()
		{
			var handled = false;

			BeforeCreateNavMenu(ref handled);
			if (handled == false)
			{
				MenuItems.Clear();
				MenuItems.AddRange(LoadMenuItemsFromSettings());
			}
			AfterCreateNavMenu();
		}
		partial void BeforeCreateNavMenu(ref bool handled);
		partial void AfterCreateNavMenu();

		private IEnumerable<MenuItem> LoadMenuItemsFromSettings()
		{
			var menuItems = new Dictionary<string, MenuItem>();

			foreach (var item in Settings.QueryStoredSettings(p => p.Key.Contains("NavMenu")))
			{
				try
				{
					menuItems.Add(item.Key, JsonSerializer.Deserialize<MenuItem>(item.Value));
				}
				catch (System.Exception ex)
				{
					System.Diagnostics.Debug.WriteLine($"Error in {System.Reflection.MethodBase.GetCurrentMethod().Name}: {ex.Message}");
				}
			}
			foreach (var item in menuItems)
			{
				foreach (var item2 in menuItems.Where(p => p.Key.StartsWith(item.Key)))
				{
					if (item.Key.Equals(item2.Key) == false)
					{
						item2.Value.Parent = item.Value;
					}
				}
			}
			return menuItems.Select(p => p.Value);
		}
	}
}
