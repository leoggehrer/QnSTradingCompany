//@QnSCodeCopy
//MdStart

using QnSTradingCompany.Contracts.Persistence.Language;
using Radzen;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Pages.Persistence.Language
{
    partial class TranslationPage
    {
        private static readonly string CommandSaveUnstored = nameof(CommandSaveUnstored);
        partial void BeforeFirstRender(ref bool handled) => MenuItems.Add(new Models.Modules.Menu.MenuItem()
        {
            Text = TranslateFor("Save unstored items"),
            Value = CommandSaveUnstored,
            Icon = "save",
        });
        public override async void OnMenuItemClick(MenuItemEventArgs args)
        {
            base.OnMenuItemClick(args);

            if (args.Value != null && args.Value.Equals(CommandSaveUnstored))
            {
                await InvokePageAsync(async() => await SaveUnstoredItemsAsync().ConfigureAwait(false)).ConfigureAwait(false);
            }
        }
        private async Task SaveUnstoredItemsAsync()
        {
            using var adapter = ServiceAdapter.Create<ITranslation>(AuthorizationSession.Token);

            foreach (var item in Translator.GetUnstoredTranslations())
            {
                var entity = await adapter.CreateAsync().ConfigureAwait(false);

                entity.Key = item.Key;
                entity.Value = item.Value;
                await adapter.InsertAsync(entity).ConfigureAwait(false);
            }
        }
    }
}
//MdEnd
