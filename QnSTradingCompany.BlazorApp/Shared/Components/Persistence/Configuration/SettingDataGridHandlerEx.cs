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
        protected override void AfterSubmitEdit(Setting item)
        {
            base.AfterSubmitEdit(item);

            InvokePageAsync(() => ReloadDataAsync());
        }
        public override void CommitEditRow(Setting item)
        {
            base.CommitEditRow(item);

            InvokePageAsync(() => ReloadDataAsync());
        }
    }
}
//MdEnd
