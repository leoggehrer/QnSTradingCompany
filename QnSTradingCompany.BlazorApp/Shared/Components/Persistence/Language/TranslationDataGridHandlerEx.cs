//@QnSCodeCopy
//MdStart
using QnSTradingCompany.BlazorApp.Models.Persistence.Language;
using Radzen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Language
{
    public partial class TranslationDataGridHandler
    {
        protected override Task<bool> BeforeLoadDataAsync(LoadDataArgs args)
        {
            ModelPage.ReloadTranslator();

            return base.BeforeLoadDataAsync(args);
        }
        protected override async Task<Translation[]> QueriedDataAsync(Translation[] models)
        {
            var result = new List<Translation>(models);

            if (models.Length < PageSize || models.Length > PageSize)
            {
                foreach (var item in ModelPage.Translator.GetUnstoredTranslations())
                {
                    var entity = await AdapterAccess.CreateAsync().ConfigureAwait(false);

                    entity.Key = item.Key;
                    entity.Value = item.Value;
                    result.Add(ToModel(entity));
                }
            }
            return await base.QueriedDataAsync(result.ToArray()).ConfigureAwait(false);
        }
        private bool hasInserted = false;
        protected override void BeforeSubmitChanges(Translation item, ref bool handled)
        {
            base.BeforeSubmitChanges(item, ref handled);

            hasInserted = item.Id == 0;
        }
        protected override void AfterSubmitChanges(Translation item)
        {
            base.AfterSubmitChanges(item);

            if (hasInserted)
            {
                InvokePageAsync(() => ReloadDataAsync());
            }
        }
        protected override void BeforeCommitEditRow(Translation item, ref bool handled)
        {
            base.BeforeCommitEditRow(item, ref handled);

            hasInserted = item.Id == 0;
        }
        protected override void AfterCommitEditRow(Translation item)
        {
            base.AfterCommitEditRow(item);

            if (hasInserted)
            {
                InvokePageAsync(() => ReloadDataAsync());
            }
        }
    }
}
//MdEnd
