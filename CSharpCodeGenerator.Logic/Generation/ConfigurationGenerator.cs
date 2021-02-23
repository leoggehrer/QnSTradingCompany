//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using CSharpCodeGenerator.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace CSharpCodeGenerator.Logic.Generation
{
	internal partial class ConfigurationGenerator : GeneratorObject, Contracts.IConfigurationGenerator
    {
        protected ConfigurationGenerator(SolutionProperties solutionProperties)
            : base(solutionProperties)
        {
        }

        public static ConfigurationGenerator Create(SolutionProperties solutionProperties)
        {
            return new ConfigurationGenerator(solutionProperties);
        }

        public string Separator { get; set; } = ";";

        public IGeneratedItem CreateTranslations()
        {
            return CreateTranslations(Separator);
        }
        public IGeneratedItem CreateProperties()
        {
            return CreateProperties(Separator);
        }

        private Models.GeneratedItem CreateTranslations(string separator)
        {
            var translations = new List<string>();
            var contractsProject = ContractsProject.Create(SolutionProperties);
            var result = new Models.GeneratedItem(Common.UnitType.General, Common.ItemType.Translations)
            {
                FullName = $"Translations",
                FileExtension = ".csv",
            };
            result.SubFilePath = $"{result.FullName}{result.FileExtension}";
            result.Add($"AppName{separator}KeyLanguage{separator}Key{separator}ValueLanguage{separator}Value");

            var types = contractsProject.PersistenceTypes
                                        .Union(contractsProject.BusinessTypes)
                                        .Union(contractsProject.ModuleTypes)
                                        .Union(contractsProject.ShadowTypes);

            foreach (var type in types)
            {
                var entityName = CreateEntityNameFromInterface(type);

                foreach (var pi in type.GetAllPropertyInfos())
                {
                    var key = $"{entityName}.{pi.Name}";
                    var value = $"{pi.Name}";

                    translations.Add($"{SolutionProperties.SolutionName}{separator}En{separator}{key}{separator}De{separator}{value}");
                }
                foreach (var pi in type.GetAllPropertyInfos())
                {
                    var key = $"{entityName}FieldSet.{pi.Name}";
                    var value = $"{pi.Name}";

                    translations.Add($"{SolutionProperties.SolutionName}{separator}En{separator}{key}{separator}De{separator}{value}");
                }
                foreach (var pi in type.GetAllPropertyInfos())
                {
                    var key = $"{entityName}DataGridColumns.{pi.Name}";
                    var value = $"{pi.Name}";

                    translations.Add($"{SolutionProperties.SolutionName}{separator}En{separator}{key}{separator}De{separator}{value}");
                }
            }
            result.Source.AddRange(translations.Distinct());
            return result;
        }
        #region Record definitions
        private record MenuItem(string Text,
                                string Value,
                                string Path,
                                string Icon,
                                int Order);

        private record DialogOptions(bool ShowTitle,
                                     bool ShowClose,
                                     string Left,
                                     string Top,
                                     string Bottom,
                                     string Width,
                                     string Height);

        private record DataGridSetting(bool HasDataGridProgress,
                                       bool HasEditDialogHeader,
                                       bool HasEditDialogFooter,
                                       bool HasDeleteDialogHeader,
                                       bool HasDeleteDialogFooter);

        private record DisplaySetting(bool ScaffoldItem, 
                                      bool IsModelItem, 
                                      bool Readonly, 
                                      string FormatValue, 
                                      bool Visible, 
                                      bool DisplayVisible, 
                                      bool EditVisible, 
                                      bool ListVisible, 
                                      bool ListSortable, 
                                      bool ListFilterable, 
                                      string ListWidth, 
                                      int Order);
        #endregion Record definitions
        private Models.GeneratedItem CreateProperties(string separator)
        {
            var properties = new List<string>();
            var contractsProject = ContractsProject.Create(SolutionProperties);
            var result = new Models.GeneratedItem(Common.UnitType.General, Common.ItemType.Properties)
            {
                FullName = $"Properties",
                FileExtension = ".csv",
            };
            var menuItem = new MenuItem(Text: "Home",
                                        Value: "home",
                                        Path: "/",
                                        Icon: "home",
                                        Order: 1);
            var types = contractsProject.PersistenceTypes
                                        .Union(contractsProject.BusinessTypes)
                                        .Union(contractsProject.ModuleTypes)
                                        .Union(contractsProject.ShadowTypes);

            result.SubFilePath = $"{result.FullName}{result.FileExtension}";
            result.Add($"AppName{separator}ComponentName{separator}MemberName{separator}MemberInfo{separator}Value");
            result.Add($"{SolutionProperties.SolutionName}{separator}NavMenu{separator}Home{separator}{separator}{JsonSerializer.Serialize<MenuItem>(menuItem)}");

            result.AddRange(CreateTypeProperties(separator, types));
            result.Source.AddRange(properties.Distinct());
            return result;
        }

        private IEnumerable<string> CreateTypeProperties(string separator, IEnumerable<Type> types)
        {
            types.CheckArgument(nameof(types));

            var result = new List<string>();

            foreach (var type in types)
            {
                var entityName = CreateEntityNameFromInterface(type);
                var categoryKey = $"{SolutionProperties.SolutionName}{separator}{entityName}";

                if (result.Any(e => e.StartsWith(categoryKey)) == false)
                {
                    var dialogOptions = new DialogOptions(ShowTitle: true,
                                                          ShowClose: true,
                                                          Left: string.Empty,
                                                          Top: string.Empty,
                                                          Bottom: string.Empty,
                                                          Width: "800px",
                                                          Height: string.Empty);
                    var dataGridItem = new DataGridSetting(HasDataGridProgress: true,
                                                           HasEditDialogHeader: false,
                                                           HasEditDialogFooter: true,
                                                           HasDeleteDialogHeader: false,
                                                           HasDeleteDialogFooter: true);

                    result.Add($"{categoryKey}{separator}PageSize{separator}{separator}50");
                    result.Add($"{categoryKey}DataGrid{separator}Setting{separator}{separator}{JsonSerializer.Serialize<DataGridSetting>(dataGridItem)}");
                    result.Add($"{categoryKey}DataGrid{separator}EditOptions{separator}{separator}{JsonSerializer.Serialize<DialogOptions>(dialogOptions)}");
                    result.Add($"{categoryKey}DataGrid{separator}DeleteOptions{separator}{separator}{JsonSerializer.Serialize<DialogOptions>(dialogOptions)}");
                }
                foreach (var pi in type.GetAllPropertyInfos())
                {
                    var fullKey = $"{categoryKey}{separator}{pi.Name}{separator}";

                    if (result.Any(e => e.StartsWith(fullKey)) == false)
                    {
                        var propertyHelper = new Helpers.ContractPropertyHelper(pi);
                        var displaySetting = new DisplaySetting(ScaffoldItem: true,
                                                                IsModelItem: false,
                                                                Readonly: false,
                                                                FormatValue: string.Empty,
                                                                Visible: GetVisible(propertyHelper),
                                                                DisplayVisible: true,
                                                                EditVisible: true,
                                                                ListVisible: true,
                                                                ListSortable: true,
                                                                ListFilterable: true,
                                                                ListWidth: GetListWitdh(propertyHelper),
                                                                Order: 10_000);

                        result.Add($"{fullKey}{separator}{JsonSerializer.Serialize<DisplaySetting>(displaySetting)}");
                    }
                }
            }
            return result;
        }

        private static bool GetVisible(Helpers.ContractPropertyHelper propertyHelper)
        {
            propertyHelper.CheckArgument(nameof(propertyHelper));

            var result = true;
            var name = propertyHelper.PropertyName;
            var type = propertyHelper.PropertyType;

            if (name.Equals("Id"))
            {
                result = false;
            }
            else if (name.Equals("RowVersion"))
            {
                result = false;
            }
            else if (type == typeof(byte[]))
            {
                result = false;
            }
            return result;
        }
        private static string GetListWitdh(Helpers.ContractPropertyHelper propertyHelper)
        {
            propertyHelper.CheckArgument(nameof(propertyHelper));

            var result = "100%";
            var type = propertyHelper.PropertyType;

            if (type.IsGenericType)
            {
                type = type.GenericTypeArguments[0];
            }

            if (type.IsNumericType())
            {
                result = "100px";
            }
            else if (type == typeof(DateTime))
            {
                result = "150px";
            }
            return result;
        }
    }
}
//MdEnd
