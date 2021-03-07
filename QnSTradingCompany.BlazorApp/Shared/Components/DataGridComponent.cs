//@QnSCodeCopy
//MdStart

using CommonBase.Extensions;
using Microsoft.AspNetCore.Components;
using QnSTradingCompany.BlazorApp.Models;
using QnSTradingCompany.BlazorApp.Models.Modules.Form;
using QnSTradingCompany.BlazorApp.Modules.DataGrid;
using Radzen;
using System.Text.Json;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class DataGridComponent : DataGridCommonComponent
    {
        [Inject]
        protected DialogService DialogService { get; init; }

		protected override void InitDisplayProperties(DisplayPropertyContainer displayProperties)
		{
			base.InitDisplayProperties(displayProperties);

            displayProperties.Add(new DisplayProperty(nameof(IdentityModel.Id)) { Readonly = true, Visible = false, IsModelItem = false, Order = 100, ListWidth = "100px" });
            displayProperties.Add(new DisplayProperty(nameof(IdentityModel.Cloneable)) { ScaffoldItem = false });
            displayProperties.Add(new DisplayProperty(nameof(IdentityModel.CloneData)) { ScaffoldItem = false });
            displayProperties.Add(new DisplayProperty(nameof(VersionModel.RowVersion)) { ScaffoldItem = false });

            displayProperties.Add(new DisplayProperty("OneItem") { ScaffoldItem = false });
            displayProperties.Add(new DisplayProperty("OneModel") { ScaffoldItem = false });
            displayProperties.Add(new DisplayProperty("ManyItems") { ScaffoldItem = false });
            displayProperties.Add(new DisplayProperty("ManyModels") { ScaffoldItem = false });
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();

            var jsonValue = Settings.GetValue($"{ComponentName}.Setting", string.Empty);

            if (jsonValue.HasContent())
            {
                DataGridSetting = JsonSerializer.Deserialize<DataGridSetting>(jsonValue);
            }
            else
            {
                DataGridSetting = new DataGridSetting(true, false, true, false, true);
            }

            jsonValue = Settings.GetValue($"{ComponentName}.EditOptions", string.Empty);
            if (jsonValue.HasContent())
            {
                EditOptions = JsonSerializer.Deserialize<DialogOptions>(jsonValue);
            }
            else
            {
                EditOptions = new DialogOptions()
                {
                    ShowTitle = true,
                    ShowClose = true,
                    Width = "800px",
                };
            }

            jsonValue = Settings.GetValue($"{ComponentName}.DeleteOptions", string.Empty);
            if (jsonValue.HasContent())
            {
                DeleteOptions = JsonSerializer.Deserialize<DialogOptions>(jsonValue);
            }
            else
            {
                DeleteOptions = new DialogOptions()
                {
                    ShowTitle = true,
                    ShowClose = true,
                    Width = "800px",
                };
            }
        }

        public DataGridSetting DataGridSetting { get; private set; }
        public DialogOptions EditOptions { get; private set; }
        public DialogOptions DeleteOptions { get; private set; }

        public DataGridComponent()
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();

    }
}
//MdEnd
