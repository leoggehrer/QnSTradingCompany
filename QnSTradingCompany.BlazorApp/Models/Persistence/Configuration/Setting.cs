//@QnSCodeCopy
//MdStart

using QnSTradingCompany.BlazorApp.Models.Modules.Configuration;
using QnSTradingCompany.BlazorApp.Models.Modules.Form;
using System.Text.Json;

namespace QnSTradingCompany.BlazorApp.Models.Persistence.Configuration
{
    partial class Setting
    {
        public string State => Id > 0 ? "Stored" : "Unstored";

        public override void BeforeEdit()
        {
            base.BeforeEdit();

            SubObjects.Clear();

            if (Value != null && Value.Contains("\"Type\":\"MenuItem\""))
            {
                SubObjects.Add(JsonSerializer.Deserialize<MenuItemModel>(Value));
            }
            else if (Value != null && Value.Contains("\"Type\":\"DialogOptions\""))
            {
                SubObjects.Add(JsonSerializer.Deserialize<DialogOptionsModel>(Value));
            }
            else if (Value != null && Value.Contains("\"Type\":\"DataGridSetting\""))
            {
                SubObjects.Add(JsonSerializer.Deserialize<DataGridSettingModel>(Value));
            }
            else if (Value != null && Value.Contains("\"Type\":\"DisplaySetting\""))
            {
                SubObjects.Add(JsonSerializer.Deserialize<DisplaySettingModel>(Value));
            }
        }
        public override void BeforeSave()
        {
            base.BeforeSave();

            if (SubObjects.Count == 1)
            {
                var serializerOptions = new JsonSerializerOptions { IgnoreReadOnlyProperties = true };

                if (SubObjects[0] is MenuItemModel mim)
                {
                    Value = JsonSerializer.Serialize(mim, serializerOptions).Replace("{", "{\"Type\":\"MenuItem\",");
                }
                else if (SubObjects[0] is DialogOptionsModel dom)
                {
                    Value = JsonSerializer.Serialize(dom, serializerOptions).Replace("{", "{\"Type\":\"DialogOptions\",");
                }
                else if (SubObjects[0] is DataGridSettingModel dgm)
                {
                    Value = JsonSerializer.Serialize(dgm, serializerOptions).Replace("{", "{\"Type\":\"DataGridSetting\",");
                }
                else if (SubObjects[0] is DisplaySettingModel dsm)
                {
                    Value = JsonSerializer.Serialize(dsm, serializerOptions).Replace("{", "{\"Type\":\"DisplaySetting\",");
                }
            }
        }
        public override void EvaluateDisplayProperty(DisplayProperty displayProperty)
        {
            base.EvaluateDisplayProperty(displayProperty);

            if (displayProperty.OriginName.Equals(nameof(Value)))
            {
                if (SubObjects.Count == 1)
                {
                    displayProperty.EditVisible = false;
                }
                else
                {
                    displayProperty.EditVisible = true;
                }
            }
        }
    }
}
//MdEnd
