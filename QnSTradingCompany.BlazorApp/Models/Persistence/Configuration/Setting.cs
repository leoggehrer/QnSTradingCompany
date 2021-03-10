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

            try
            {
                if (Value != null && Value.Contains($"\"Type\":\"{nameof(MenuItem)}\""))
                {
                    SubObjects.Add(JsonSerializer.Deserialize<MenuItem>(Value));
                }
                else if (Value != null && Value.Contains($"\"Type\":\"{nameof(DialogOptions)}\""))
                {
                    SubObjects.Add(JsonSerializer.Deserialize<DialogOptions>(Value));
                }
                else if (Value != null && Value.Contains($"\"Type\":\"{nameof(DataGridSetting)}\""))
                {
                    SubObjects.Add(JsonSerializer.Deserialize<DataGridSetting>(Value));
                }
                else if (Value != null && Value.Contains($"\"Type\":\"{nameof(DisplaySetting)}\""))
                {
                    SubObjects.Add(JsonSerializer.Deserialize<DisplaySetting>(Value));
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in {System.Reflection.MethodBase.GetCurrentMethod().Name}: {ex.Message}");
            }
        }
        public override void BeforeSave()
        {
            base.BeforeSave();

            if (SubObjects.Count == 1)
            {
                var serializerOptions = new JsonSerializerOptions { IgnoreReadOnlyProperties = true };

                if (SubObjects[0] is MenuItem mim)
                {
                    Value = JsonSerializer.Serialize(mim, serializerOptions);
                }
                else if (SubObjects[0] is DialogOptions dom)
                {
                    Value = JsonSerializer.Serialize(dom, serializerOptions);
                }
                else if (SubObjects[0] is DataGridSetting dgm)
                {
                    Value = JsonSerializer.Serialize(dgm, serializerOptions);
                }
                else if (SubObjects[0] is DisplaySetting dsm)
                {
                    Value = JsonSerializer.Serialize(dsm, serializerOptions);
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
            else if (displayProperty.OriginName.Equals(nameof(ConfigurationModel.Type)))
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
