//@QnSCodeCopy
//MdStart
using QnSTradingCompany.BlazorApp.Models.Persistence.Configuration;
using Radzen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QnSTradingCompany.BlazorApp.Shared.Components.Persistence.Configuration
{
    public partial class SettingDataGridHandler
    {
        protected override Task<bool> BeforeLoadDataAsync(LoadDataArgs args)
        {
            ModelPage.ReloadSetting();

            return base.BeforeLoadDataAsync(args);
        }
        protected override async Task<Setting[]> QueriedDataAsync(Setting[] models)
        {
            var result = new List<Setting>(models);

            if (models.Length < PageSize || models.Length > PageSize)
            {
                foreach (var item in ModelPage.Settings.GetUnstoredSettings())
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
        protected override void BeforeSubmitChanges(Setting item, ref bool handled)
        {
            base.BeforeSubmitChanges(item, ref handled);

            hasInserted = item.Id == 0;
        }
        protected override void AfterSubmitChanges(Setting item)
        {
            base.AfterSubmitChanges(item);

            if (hasInserted)
            {
                InvokePageAsync(() => ReloadDataAsync());
            }
        }
        protected override void BeforeCommitEditRow(Setting item, ref bool handled)
        {
            base.BeforeCommitEditRow(item, ref handled);

            hasInserted = item.Id == 0;
        }
        protected override void AfterCommitEditRow(Setting item)
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
