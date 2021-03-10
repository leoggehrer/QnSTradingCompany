//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using CSharpCodeGenerator.Logic.Contracts;
using CSharpCodeGenerator.Logic.Models.Configuration;
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

            var key = string.Empty;
            var types = contractsProject.PersistenceTypes
                                        .Union(contractsProject.BusinessTypes)
                                        .Union(contractsProject.ModuleTypes)
                                        .Union(contractsProject.ShadowTypes);
            var properties = types.SelectMany(t => t.GetAllPropertyInfos())
                                  .GroupBy(p => p.Name)
                                  .Select(g => g.FirstOrDefault());

            translations.Add($"{SolutionProperties.SolutionName}{separator}En{separator}Cancel{separator}De{separator}Cancel");
            translations.Add($"{SolutionProperties.SolutionName}{separator}En{separator}Confirm{separator}De{separator}Confirm");
            translations.Add($"{SolutionProperties.SolutionName}{separator}En{separator}Submit{separator}De{separator}Submit");
            translations.Add($"{SolutionProperties.SolutionName}{separator}En{separator}SubmitClose{separator}De{separator}SubmitClose");
            foreach (var item in properties.OrderBy(p => p.Name))
            {
                key = $"{item.Name}";
                translations.Add($"{SolutionProperties.SolutionName}{separator}En{separator}{key}{separator}De{separator}{item.Name}");
            }

            key = "LoginMenu";
            translations.Add($"{SolutionProperties.SolutionName}{separator}En{separator}{key}.Access-Authorization{separator}De{separator}Access-Authorization");
            translations.Add($"{SolutionProperties.SolutionName}{separator}En{separator}{key}.Identity-User{separator}De{separator}Identity-User");
            translations.Add($"{SolutionProperties.SolutionName}{separator}En{separator}{key}.Role-Management{separator}De{separator}Role-Management");
            translations.Add($"{SolutionProperties.SolutionName}{separator}En{separator}{key}.Change password{separator}De{separator}Change password");
            translations.Add($"{SolutionProperties.SolutionName}{separator}En{separator}{key}.Reset password{separator}De{separator}Reset password");
            translations.Add($"{SolutionProperties.SolutionName}{separator}En{separator}{key}.Translation{separator}De{separator}Translation");
            translations.Add($"{SolutionProperties.SolutionName}{separator}En{separator}{key}.Settings{separator}De{separator}Settings");
            translations.Add($"{SolutionProperties.SolutionName}{separator}En{separator}{key}.Logout{separator}De{separator}Logout");

            foreach (var type in types.OrderBy(t => t.Name))
            {
                var entityName = CreateEntityNameFromInterface(type);

                key = $"{entityName}";
                translations.Add($"{SolutionProperties.SolutionName}{separator}En{separator}{key}.TitelDetails{separator}De{separator}TitelDetails");
                translations.Add($"{SolutionProperties.SolutionName}{separator}En{separator}{key}.TitleEditModel{separator}De{separator}TitleEditModel");
                translations.Add($"{SolutionProperties.SolutionName}{separator}En{separator}{key}.TitleConfirmDelete{separator}De{separator}TitleConfirmDelete");
            }

            result.Source.AddRange(translations.Distinct());
            return result;
        }

        private Models.GeneratedItem CreateProperties(string separator)
        {
            var properties = new List<string>();
            var contractsProject = ContractsProject.Create(SolutionProperties);
            var result = new Models.GeneratedItem(Common.UnitType.General, Common.ItemType.Properties)
            {
                FullName = $"Properties",
                FileExtension = ".csv",
            };
            var menuItem = new MenuItem()
            {
                Text = "Home",
                Value = "home",
                Path = "/",
                Icon = "home",
                Order = 1,
            };
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
                    var dialogOptions = new DialogOptions()
                    {
                        ShowTitle = true,
                        ShowClose = true,
                        Left = string.Empty,
                        Top = string.Empty,
                        Bottom = string.Empty,
                        Width = "800px",
                        Height = string.Empty,
                    };
                    var dataGridItem = new DataGridSetting()
                    {
                        HasDataGridProgress = true,
                        HasEditDialogHeader = false,
                        HasEditDialogFooter = true,
                        HasDeleteDialogHeader = false,
                        HasDeleteDialogFooter = true,
                    };

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
                        var displaySetting = new DisplaySetting()
                        {
                            ScaffoldItem = true,
                            IsModelItem = false,
                            Readonly = false,
                            FormatValue = string.Empty,
                            Visible = GetVisible(propertyHelper),
                            DisplayVisible = true,
                            EditVisible = true,
                            ListVisible = true,
                            ListSortable = true,
                            ListFilterable = true,
                            ListWidth = GetListWitdh(propertyHelper),
                            Order = 10_000,
                        };

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
