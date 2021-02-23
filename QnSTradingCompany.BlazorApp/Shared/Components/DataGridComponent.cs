//@QnSCodeCopy
//MdStart

using CommonBase.Extensions;
using Microsoft.AspNetCore.Components;
using QnSTradingCompany.BlazorApp.Models.Modules.Form;
using QnSTradingCompany.BlazorApp.Modules.DataGrid;
using Radzen;
using System.Text.Json;

namespace QnSTradingCompany.BlazorApp.Shared.Components
{
    public partial class DataGridComponent : DisplayComponent
    {
        [Inject]
        protected DialogService DialogService
        {
            get;
            private set;
        }
        [Inject]
        protected NotificationService NotificationService
        {
            get;
            private set;
        }

        protected override void BeforeInitialized()
        {
            base.BeforeInitialized();

            var jsonValue = Settings.GetValue($"{ComponentName}.Setting", string.Empty);

            if (jsonValue.HasContent())
            {
                DataGridSetting = JsonSerializer.Deserialize<DataGridSetting>(jsonValue);
            }
            else
            {
                DataGridSetting = new DataGridSetting(true, true, true, true, true);
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
        protected override void InitDisplayProperties(DisplayPropertyContainer displayProperties)
        {
            base.InitDisplayProperties(displayProperties);

            displayProperties.Add(new DisplayProperty("Id") { Visible = false, IsModelItem = false });
            displayProperties.Add(new DisplayProperty("RowVersion") { Visible = false, IsModelItem = false });

            displayProperties.Add(new DisplayProperty("HasError") { ScaffoldItem = false });
            displayProperties.Add(new DisplayProperty("Errors") { ScaffoldItem = false });

            displayProperties.Add(new DisplayProperty("OneItem") { ScaffoldItem = false });
            displayProperties.Add(new DisplayProperty("OneModel") { ScaffoldItem = false });
            displayProperties.Add(new DisplayProperty("ManyItems") { ScaffoldItem = false });
            displayProperties.Add(new DisplayProperty("ManyModels") { ScaffoldItem = false });
        }

        public DataGridSetting DataGridSetting { get; private set; }
        public DialogOptions EditOptions { get; private set; }
        public DialogOptions DeleteOptions { get; private set; }
    }
}
//MdEnd
