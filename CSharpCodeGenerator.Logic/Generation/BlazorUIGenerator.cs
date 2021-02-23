//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using CSharpCodeGenerator.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSharpCodeGenerator.Logic.Generation
{
    public partial class BlazorUIGenerator
    {
        public static readonly string[] IgnoreFields = new string[] { "Id", "RowVersion" };
        public static bool IsPropertyCreatable(PropertyInfo propertyInfo)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var propertyHelper = new ContractPropertyHelper(propertyInfo);

            return propertyHelper.HasImplementation == false;
        }
        public static bool IsNullableField(PropertyInfo propertyInfo)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            return propertyInfo.PropertyType.IsNullableType();
        }
        public static bool IsDisplayField(PropertyInfo propertyInfo)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var result = false;

            if (propertyInfo.CanRead)
            {
                if (propertyInfo.PropertyType == typeof(string))
                {
                    result = true;
                }
                else if ((propertyInfo.Name.EndsWith("Id") == false
                          || propertyInfo.PropertyType != typeof(int))
                          && propertyInfo.PropertyType.IsNumericType())
                {
                    result = true;
                }
            }
            return result;
        }

        public static IEnumerable<RazorBuilder> CreateGridColumns(Type type)
        {
            var result = new List<RazorBuilder>();
            var handled = false;

            BeginCreateGridColumns(type, result, ref handled);
            if (handled == false)
            {
                var createHelper = default(ContractHelper);

                if (ContractHelper.HasOneToMany(type))
                {
                    var (one, _) = ContractHelper.GetOneToManyTypes(type);

                    createHelper = new ContractHelper(one);
                }
                else
                {
                    createHelper = new ContractHelper(type);
                }

                foreach (var propertyInfo in createHelper.GetAllProperties()
                                                         .Where(e => IsPropertyCreatable(e))
                                                         .Select(e => new ContractPropertyHelper(e))
                                                         .OrderBy(e => e.Order)
                                                         .Select(e => e.Property))
                {
                    var item = CreateGridColumn(propertyInfo);

                    if (item != null)
                    {
                        result.Add(item);
                    }
                }
            }
            EndCreateGridColumns(type, result);
            return result;
        }
        static partial void BeginCreateGridColumns(Type type, List<RazorBuilder> gridColumns, ref bool handled);
        static partial void EndCreateGridColumns(Type type, List<RazorBuilder> gridColumns);

        public static RazorBuilder CreateGridColumn(PropertyInfo propertyInfo)
        {
            var result = default(RazorBuilder);
            var handled = false;

            BeginCreateGridColumn(propertyInfo, ref result, ref handled);
            if (handled == false)
            {
                var propertyHelper = new ContractPropertyHelper(propertyInfo);

                if (propertyHelper.Property.Name.Equals("Id"))
                {
                    result = CreateIdGridColumn(propertyInfo);
                }
                else if (propertyHelper.Property.Name.EndsWith("Id")
                         && (propertyHelper.Property.PropertyType.Equals(typeof(int))
                             || propertyHelper.Property.PropertyType.Equals(typeof(int?))))
                {
                    result = CreateIdGridColumn(propertyInfo);
                }
                else if (propertyInfo.PropertyType.IsEnum)
                {
                    result = CreateEnumGridColumn(propertyInfo);
                }
                else if (propertyInfo.PropertyType.IsNumericType())
                {
                    result = CreateNumericGridColumn(propertyInfo);
                }
                else if (propertyInfo.PropertyType.Equals(typeof(DateTime))
                         || propertyInfo.PropertyType.Equals(typeof(DateTime?)))
                {
                    result = CreateDateTimeGridColumn(propertyInfo);
                }
                else if (propertyInfo.PropertyType.Equals(typeof(string)))
                {
                    result = CreateTextGridColumn(propertyInfo);
                }
            }
            EndCreateGridColumn(propertyInfo, result);
            return result;
        }
        static partial void BeginCreateGridColumn(PropertyInfo propertyInfo, ref RazorBuilder gridColumn, ref bool handled);
        static partial void EndCreateGridColumn(PropertyInfo propertyInfo, RazorBuilder gridColumn);

        public static IEnumerable<RazorBuilder> CreateAddFieldSet(Type type)
        {
            var result = new List<RazorBuilder>();
            var handled = false;

            BeginCreateAddFieldSet(type, result, ref handled);
            if (handled == false)
            {
                var typeHelper = new Helpers.ContractHelper(type);

                foreach (var property in typeHelper.GetAllProperties()
                                                   .Where(e => IsPropertyCreatable(e)
                                                            && IgnoreFields.Contains(e.Name) == false))
                {
                    var item = CreateEditField(property);

                    if (item != null)
                    {
                        result.Add(item);
                    }
                }
            }
            EndCreateAddFieldSet(type, result);
            return result;
        }
        static partial void BeginCreateAddFieldSet(Type type, List<RazorBuilder> editFields, ref bool handled);
        static partial void EndCreateAddFieldSet(Type type, List<RazorBuilder> editFields);

        public static IEnumerable<RazorBuilder> CreateDeleteFieldSet(Type type)
        {
            var result = new List<RazorBuilder>();
            var handled = false;

            BeginCreateDeleteFieldSet(type, result, ref handled);
            if (handled == false)
            {
                var typeHelper = new Helpers.ContractHelper(type);

                foreach (var property in typeHelper.GetAllProperties()
                                                   .Where(e => IsPropertyCreatable(e)
                                                            && IgnoreFields.Contains(e.Name) == false
                                                            && IsDisplayField(e)))
                {
                    var item = CreateDeleteField(property);

                    if (item != null)
                    {
                        result.Add(item);
                    }
                }
            }
            EndCreateDeleteFieldSet(type, result);
            return result;
        }
        static partial void BeginCreateDeleteFieldSet(Type type, List<RazorBuilder> deleteFields, ref bool handled);
        static partial void EndCreateDeleteFieldSet(Type type, List<RazorBuilder> deleteFields);

        public static RazorBuilder CreateEditField(PropertyInfo propertyInfo)
        {
            var result = default(RazorBuilder);
            var handled = false;

            BeginCreateEditField(propertyInfo, ref result, ref handled);
            if (handled == false)
            {
                if (propertyInfo.PropertyType.Equals(typeof(string)))
                {
                    result = CreateTextEditField(propertyInfo);
                }
                else if (propertyInfo.PropertyType.IsEnum)
                {
                    result = CreateEnumEditField(propertyInfo);
                }
                else if (propertyInfo.PropertyType.IsNumericType())
                {
                    result = CreateNumericEditField(propertyInfo);
                }
                else if (propertyInfo.PropertyType.Equals(typeof(DateTime))
                         || propertyInfo.PropertyType.Equals(typeof(DateTime?)))
                {
                    result = CreateDateTimeEditField(propertyInfo);
                }
            }
            EndCreateEditField(propertyInfo, result);
            return result;
        }
        static partial void BeginCreateEditField(PropertyInfo propertyInfo, ref RazorBuilder editFild, ref bool handled);
        static partial void EndCreateEditField(PropertyInfo propertyInfo, RazorBuilder editField);

        public static RazorBuilder CreateDisplayField(PropertyInfo propertyInfo)
        {
            var result = default(RazorBuilder);
            var handled = false;

            BeginCreateDisplayField(propertyInfo, ref result, ref handled);
            if (handled == false)
            {
                if (propertyInfo.PropertyType.IsCollectible == false)
                {
                    result = CreateDefaultDisplayField(propertyInfo);
                }
            }
            EndCreateDisplayField(propertyInfo, result);
            return result;
        }
        static partial void BeginCreateDisplayField(PropertyInfo propertyInfo, ref RazorBuilder displayField, ref bool handled);
        static partial void EndCreateDisplayField(PropertyInfo propertyInfo, RazorBuilder displayField);

        public static RazorBuilder CreateDeleteField(PropertyInfo propertyInfo)
        {
            var result = default(RazorBuilder);
            var handled = false;

            BeginCreateDeleteField(propertyInfo, ref result, ref handled);
            if (handled == false)
            {
                if (propertyInfo.PropertyType.IsCollectible == false)
                {
                    result = CreateDefaultDeleteField(propertyInfo);
                }
            }
            EndCreateDeleteField(propertyInfo, result);
            return result;
        }
        static partial void BeginCreateDeleteField(PropertyInfo propertyInfo, ref RazorBuilder deleteField, ref bool handled);
        static partial void EndCreateDeleteField(PropertyInfo propertyInfo, RazorBuilder deleteField);

        #region CreateGridColumns
        private static string GetDefaultColumnWidth(PropertyInfo propertyInfo)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var result = "100%";
            var propertyHelper = new ContractPropertyHelper(propertyInfo);

            if (propertyHelper.Property.Name.Equals("Id"))
            {
                result = "60px";
            }
            else if (propertyHelper.Property.Name.EndsWith("Id")
                     && (propertyHelper.Property.PropertyType.Equals(typeof(int))
                         || propertyHelper.Property.PropertyType.Equals(typeof(int?))))
            {
                result = "60px";
            }
            else if (propertyInfo.PropertyType.IsEnum)
            {
                result = "100%";
            }
            else if (propertyInfo.PropertyType.IsNumericType())
            {
                result = "100px";
            }

            else if (propertyInfo.PropertyType.Equals(typeof(string)))
            {
                result = "100%";
            }
            return result;
        }
        private static RazorBuilder CreateDefaultGridColumn(PropertyInfo propertyInfo)
        {
            return CreateDefaultGridColumn(propertyInfo, null);
        }
        private static RazorBuilder CreateDefaultGridColumn(PropertyInfo propertyInfo, Action<RazorBuilder> createAction)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var result = new RazorBuilder();
            var defaultWidth = GetDefaultColumnWidth(propertyInfo);
            var displayProperty = $"displayProperty{propertyInfo.Name}";

            result.OpenCodeBlock("@{")
                    .AddCode($"var {displayProperty} = GetOrCreateDisplayProperty(nameof(TModel.{propertyInfo.Name}));")
                    .OpenTag($"RadzenGridColumn", $"TItem =\"TModel\" Property=\"@{displayProperty}.PropertyName\" Visible=\"@{displayProperty}.IsListVisible\" Width=\"@{displayProperty}.ListWidth\"  Sortable=\"@{displayProperty}.IsListSortable\" Filterable=\"@{displayProperty}.IsListFilterable\"")
                      .OpenTag("HeaderTemplate")
                        .AddTag($"RadzenLabel", $"Text=\"@TranslateFor({displayProperty}.PropertyName)\"")
                      .CloseTag()
                      .OpenTag("Template", "Context=\"data\"")
                        .AddCode($"@{displayProperty}.ToDisplay(data, data.{propertyInfo.Name})")
                      .CloseTag()
                      .Execute(createAction)
                      .OpenTag("FooterTemplate")
                        .AddCode($"@{displayProperty}.GetFooterText({displayProperty}.PropertyName)")
                      .CloseTag()
                    .CloseTag()
                  .CloseCodeBlock();

            return result;
        }

        public static RazorBuilder CreateIdGridColumn(PropertyInfo propertyInfo)
        {
            return CreateDefaultGridColumn(propertyInfo);
        }
        public static RazorBuilder CreateEnumGridColumn(PropertyInfo propertyInfo)
        {
            var data = string.Empty;

            foreach (var item in Enum.GetValues(propertyInfo.PropertyType).OfType<Enum>())
            {
                if (data.Length > 0)
                    data += ",";

                data += "new { Value=" + $"{propertyInfo.PropertyType.FullName}.{item.Description()}" + ", Text=\"" + item.Description() + "\" }";
            }
            data = "new object[] {" + data + "}";

            return CreateDefaultGridColumn(propertyInfo, rb =>
            {
                if (propertyInfo.CanWrite)
                {
                    rb.OpenTag("EditTemplate", "Context=\"item\"");
                    rb.AddTag("RadzenDropDown", $"TValue=\"{propertyInfo.PropertyType.FullName}\" @bind-Value=\"@item.{propertyInfo.Name}\" Data=\"@({data})\" ValueProperty=\"Value\" TextProperty=\"Text\" Name=\"@TranslateFor(nameof(TModel.{propertyInfo.Name}))\" Style=\"width: 100%;\"");
                    rb.CloseTag();
                }
            });
        }
        public static RazorBuilder CreateNumericGridColumn(PropertyInfo propertyInfo)
        {
            return CreateDefaultGridColumn(propertyInfo, rb =>
            {
                if (propertyInfo.CanWrite)
                {
                    rb.OpenTag("EditTemplate", "Context=\"item\"");
                    rb.AddTag("RadzenNumeric", $"@bind-Value=\"@item.{propertyInfo.Name}\" Name=\"@TranslateFor(nameof(TModel.{propertyInfo.Name}))\" Style=\"width: 100%; display: block\"");
                    rb.CloseTag();
                }
            });
        }
        public static RazorBuilder CreateDateTimeGridColumn(PropertyInfo propertyInfo)
        {
            var propertyHelper = new Helpers.ContractPropertyHelper(propertyInfo);

            return CreateDefaultGridColumn(propertyInfo, rb =>
            {
                if (propertyInfo.CanWrite)
                {
                    rb.OpenTag("EditTemplate", "Context=\"item\"");
                    rb.AddTag("RadzenDatePicker", $"@bind-Value=\"@item.{propertyInfo.Name}\" Name=\"@TranslateFor(nameof(TModel.{propertyInfo.Name}))\" Style=\"width: 100%; display: block\"");
                    if (propertyHelper.IsRequired)
                    {
                        rb.AddTag("RadzenRequiredValidator", $"Text=\"@TranslateFor(nameof(TModel.{propertyInfo.Name}) + \" is required\")\" Component=\"@TranslateFor(nameof(TModel.{propertyInfo.Name}))\" Popup=\"true\"");
                    }
                    rb.CloseTag();
                }
            });
        }
        public static RazorBuilder CreateTextGridColumn(PropertyInfo propertyInfo)
        {
            var propertyHelper = new Helpers.ContractPropertyHelper(propertyInfo);

            return CreateDefaultGridColumn(propertyInfo, rb =>
            {
                if (propertyInfo.CanWrite)
                {
                    rb.OpenTag("EditTemplate", "Context=\"item\"");
                    rb.AddTag("RadzenTextBox", $"@bind-Value=\"@item.{propertyInfo.Name}\" Name=\"@TranslateFor(nameof(TModel.{propertyInfo.Name}))\" Style=\"width: 100%; display: block\"");
                    if (propertyHelper.IsRequired)
                    {
                        rb.AddTag("RadzenRequiredValidator", $"Text=\"@TranslateFor(nameof(TModel.{propertyInfo.Name}) + \" is required\")\" Component=\"@TranslateFor(nameof(TModel.{propertyInfo.Name}))\" Popup=\"true\"");
                    }
                    rb.CloseTag();
                }
            });
        }
        #endregion CreateGridColumns

        #region CreateEditFields
        private static RazorBuilder CreateDefaultEditField(PropertyInfo propertyInfo, Action<Helpers.RazorBuilder> createAction)
        {
            var result = new RazorBuilder();

            result.AddCode($"@if (CanFieldCreate(nameof(EditModel.{propertyInfo.Name})))")
                   .OpenCodeBlock()
                    .OpenTag("div", "row", string.Empty)
                     .OpenTag("div", "col-md-4 align-items-center d-flex", string.Empty)
                      .AddTag("RadzenLabel", $"Text=\"@TranslateFor(nameof(EditModel.{propertyInfo.Name}))\"")
                     .CloseTag()
                    .OpenTag("div", "col-md-8", "style=\"margin-top: 16px;\"")
                    .Execute(createAction)
                    .CloseTag()
                   .CloseTag()
                  .CloseCodeBlock();
            return result;
        }

        public static RazorBuilder CreateTextEditField(PropertyInfo propertyInfo)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var propertyHelper = new Helpers.ContractPropertyHelper(propertyInfo);

            return CreateDefaultEditField(propertyInfo, rb =>
            {
                if (propertyHelper.MaxLength > 50)
                {
                    rb.AddTag("RadzenTextArea", $"@bind-Value=\"@EditModel.{propertyInfo.Name}\" MaxLength=\"{propertyHelper.MaxLength}\" Name=\"@TranslateFor(nameof(EditModel.{propertyInfo.Name}))\" Style=\"width: 100%;\"");
                }
                else
                {
                    rb.AddTag("RadzenTextBox", $"@bind-Value=\"@EditModel.{propertyInfo.Name}\" MaxLength=\"{propertyHelper.MaxLength}\" Name=\"@TranslateFor(nameof(EditModel.{propertyInfo.Name}))\" Style=\"width: 100%;\"");
                }
                if (propertyHelper.IsRequired)
                {
                    rb.AddTag("RadzenRequiredValidator", $"Text=\"@TranslateFor(nameof(EditModel.{propertyInfo.Name}) + \" is required\")\" Component=\"@TranslateFor(nameof(EditModel.{propertyInfo.Name}))\" Popup=\"false\"");
                }
            });
        }

        public static RazorBuilder CreateEnumEditField(PropertyInfo propertyInfo)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var data = string.Empty;

            foreach (var item in Enum.GetValues(propertyInfo.PropertyType).OfType<Enum>())
            {
                if (data.Length > 0)
                    data += ",";

                data += "new { Value=" + $"{propertyInfo.PropertyType.FullName}.{item.Description()}" + ", Text=\"" + item.Description() + "\" }";
            }
            data = "new object[] {" + data + "}";

            return CreateDefaultEditField(propertyInfo, rb =>
            {
                rb.AddTag("RadzenDropDown", $"TValue=\"{propertyInfo.PropertyType.FullName}\" @bind-Value=\"@EditModel.{propertyInfo.Name}\" Data=\"@({data})\" ValueProperty=\"Value\" TextProperty=\"Text\" Name=\"@TranslateFor(nameof(EditModel.{propertyInfo.Name}))\" Style=\"width: 100%;\"");
            });
        }
        public static RazorBuilder CreateEnumEditField(PropertyInfo propertyInfo, Type enumType)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var idx = 0;
            var data = string.Empty;

            foreach (var item in Enum.GetValues(enumType).OfType<Enum>())
            {
                if (data.Length > 0)
                    data += ",";

                data += "new { Value=" + $"{(int)Enum.GetValues(enumType).GetValue(idx++)}" + ", Text=\"" + item.Description() + "\" }";
            }
            data = "new object[] {" + data + "}";

            return CreateDefaultEditField(propertyInfo, rb =>
            {
                rb.AddTag("RadzenDropDown", $"TValue=\"{propertyInfo.PropertyType.FullName}\" @bind-Value=\"@EditModel.{propertyInfo.Name}\" Data=\"@({data})\" ValueProperty=\"Value\" TextProperty=\"Text\" Name=\"@TranslateFor(nameof(EditModel.{propertyInfo.Name}))\" Style=\"width: 100%;\"");
            });
        }
        public static RazorBuilder CreateNumericEditField(PropertyInfo propertyInfo)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var typeName = propertyInfo.PropertyType.Name;

            if (propertyInfo.PropertyType.IsNullableType())
            {
                typeName = $"Nullable<{Nullable.GetUnderlyingType(propertyInfo.PropertyType)}>";
            }
            return CreateDefaultEditField(propertyInfo, rb =>
            {
                rb.AddTag("RadzenNumeric", $"TValue=\"{typeName}\" @bind-Value=\"@EditModel.{propertyInfo.Name}\" Name=\"@TranslateFor(nameof(EditModel.{propertyInfo.Name}))\" Style=\"width: 100%;\"");
            });
        }
        public static RazorBuilder CreateDateTimeEditField(PropertyInfo propertyInfo)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var typeName = propertyInfo.PropertyType.Name;

            if (propertyInfo.PropertyType.IsNullableType())
            {
                typeName = $"Nullable<{Nullable.GetUnderlyingType(propertyInfo.PropertyType)}>";
            }
            return CreateDefaultEditField(propertyInfo, rb =>
            {
                rb.AddTag("RadzenDatePicker", $"TValue=\"{typeName}\" @bind-Value=\"@EditModel.{propertyInfo.Name}\" DateFomat=\"d\" Name=\"@TranslateFor(nameof(EditModel.{propertyInfo.Name}))\" Style=\"width: 100%;\"");
            });
        }
        #endregion CreateEditFields

        #region CreateDisplayField
        public static RazorBuilder CreateDefaultDisplayField(PropertyInfo propertyInfo)
        {
            var result = new RazorBuilder();

            result.AddTag("QnSTradingCompany.BlazorApp.Shared.Components.DisplayProperty", $"Model=\"@DisplayModel\" PropertyName=\"{propertyInfo.Name}\"");
            return result;
        }
        public static RazorBuilder CreateDefaultDeleteField(PropertyInfo propertyInfo)
        {
            var result = new RazorBuilder();

            result.AddTag("QnSTradingCompany.BlazorApp.Shared.Components.DisplayProperty", $"Model=\"@DeleteModel\" PropertyName=\"@nameof(DeleteModel.{propertyInfo.Name})\"");
            return result;
        }
        #endregion CreateDisplayField
    }
}
//MdEnd
