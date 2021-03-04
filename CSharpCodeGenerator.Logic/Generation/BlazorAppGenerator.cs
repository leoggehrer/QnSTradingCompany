//@QnSCodeCopy
//MdStart

using CommonBase.Extensions;
using CSharpCodeGenerator.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CSharpCodeGenerator.Logic.Generation
{
    internal partial class BlazorAppGenerator : ModelGenerator, Contracts.IBlazorAppGenerator
    {
        #region Pages-Definitions
        public string ProjectName => $"{SolutionProperties.SolutionName}{SolutionProperties.BlazorAppPostfix}";
        public static string PageExtension => ".razor";
        public static string CodeExtension => ".cs";
        public static string UsingsLabel => "Usings";
        public static string NamespaceCodeLabel => "NamespaceCode";
        public static string ClassCodeLabel => "ClassCode";

        public static string PagesFolder => "Pages";
        public static string SharedFolder => "Shared";
        public static string ComponentsFolder => "Components";
        public static string TemplatesSubPath => Path.Combine("Templates", "Blazor");

        #endregion Pages-Definitions

        protected BlazorAppGenerator(SolutionProperties solutionProperties)
            : base(solutionProperties)
        {
        }
        public new static BlazorAppGenerator Create(SolutionProperties solutionProperties)
        {
            return new BlazorAppGenerator(solutionProperties);
        }

        public override Common.UnitType UnitType => Common.UnitType.BlazorApp;
        public override string AppPostfix => SolutionProperties.BlazorAppPostfix;
        public override string AppModelsNameSpace => $"{SolutionProperties.BlazorAppProjectName}.{StaticLiterals.ModelsFolder}";
        public override string ModelsFolder => "Models";

        public string CreatePagesNameSpace(Type type)
        {
            type.CheckArgument(nameof(type));

            return $"{SolutionProperties.BlazorAppProjectName}.{PagesFolder}.{CreateSubNamespaceFromType(type)}";
        }
        public string CreateComponentsNameSpace(Type type)
        {
            type.CheckArgument(nameof(type));

            return $"{SolutionProperties.BlazorAppProjectName}.{SharedFolder}.{ComponentsFolder}.{CreateSubNamespaceFromType(type)}";
        }

        private bool CanCreateIndexPage(Type type)
        {
            bool create = true;

            CanCreateIndexPage(type, ref create);
            return create;
        }
        partial void CanCreateIndexPage(Type type, ref bool create);

        protected override void CreateModelPropertyAttributes(Type type, PropertyInfo propertyInfo, List<string> codeLines)
        {
            base.CreateModelPropertyAttributes(type, propertyInfo, codeLines);

            var handled = false;

            BeforeCreateModelPropertyAttributes(type, propertyInfo, codeLines, ref handled);
            if (handled == false)
            {
                var propertyHelper = new ContractPropertyHelper(propertyInfo);

                if (propertyHelper.IsRequired)
                {
                    codeLines.Add("[System.ComponentModel.DataAnnotations.Required]");
                }
                if (propertyInfo.PropertyType.Equals(typeof(string)) && propertyHelper.MaxLength > 0)
                {
                    codeLines.Add($"[System.ComponentModel.DataAnnotations.StringLength({propertyHelper.MaxLength})]");
                }
                if (propertyHelper.RegularExpression.HasContent())
                {
                    codeLines.Add($"[System.ComponentModel.DataAnnotations.RegularExpression(@\"{propertyHelper.RegularExpression}\")]");
                }
                if (propertyHelper.ContentType != CommonBase.Attributes.ContentType.Undefined)
                {
                    codeLines.Add($"[System.ComponentModel.DataAnnotations.DataType(System.ComponentModel.DataAnnotations.DataType.{propertyHelper.ContentType})]");
                }
            }
            AfterCreateModelPropertyAttributes(type, propertyInfo, codeLines);
        }
        partial void BeforeCreateModelPropertyAttributes(Type type, PropertyInfo propertyInfo, List<string> codeLines, ref bool handled);
        partial void AfterCreateModelPropertyAttributes(Type type, PropertyInfo propertyInfo, List<string> codeLines);

        public IEnumerable<Contracts.IGeneratedItem> CreateBusinessIndexPages()
        {
            var result = new List<Contracts.IGeneratedItem>();
            var contractsProject = ContractsProject.Create(SolutionProperties);

            foreach (var type in contractsProject.BusinessTypes)
            {
                if (CanCreateIndexPage(type))
                {
                    result.AddRange(CreateDataGridPage(type, StaticLiterals.BusinessLabel));
                    result.AddRange(CreateDisplayComponent(type, StaticLiterals.BusinessLabel));
                }
            }
            return result;
        }
        public IEnumerable<Contracts.IGeneratedItem> CreatePersistenceIndexPages()
        {
            var result = new List<Contracts.IGeneratedItem>();
            var contractsProject = ContractsProject.Create(SolutionProperties);

            foreach (var type in contractsProject.PersistenceTypes)
            {
                if (CanCreateIndexPage(type))
                {
                    result.AddRange(CreateDataGridPage(type, StaticLiterals.PersistenceLabel));
                    result.AddRange(CreateDisplayComponent(type, StaticLiterals.PersistenceLabel));
                }
            }
            return result;
        }
        public IEnumerable<Contracts.IGeneratedItem> CreateShadowIndexPages()
        {
            var result = new List<Contracts.IGeneratedItem>();
            var contractsProject = ContractsProject.Create(SolutionProperties);

            foreach (var type in contractsProject.ShadowTypes)
            {
                if (CanCreateIndexPage(type))
                {
                    result.AddRange(CreateDataGridPage(type, StaticLiterals.ShadowLabel));
                    result.AddRange(CreateDisplayComponent(type, StaticLiterals.ShadowLabel));
                }
            }
            return result;
        }
        public IEnumerable<Contracts.IGeneratedItem> CreateDataGridPage(Type type, string typeLabel)
        {
            type.CheckArgument(nameof(type));
            typeLabel.CheckArgument(nameof(typeLabel));

            var result = new List<Contracts.IGeneratedItem>();

            StartCreateDataGridPage(type, typeLabel);

            result.Add(CreateDataGridPageRazor(type));
            result.Add(CreateDataGridPageCode(type));

            FinishCreateIndexPage(type, typeLabel);
            return result;
        }
        partial void StartCreateDataGridPage(Type type, string typeLabel);
        partial void FinishCreateIndexPage(Type type, string typeLabel);

        private Contracts.IGeneratedItem CreateDataGridPageRazor(Type type)
        {
            type.CheckArgument(nameof(type));

            var subPath = CreateSubPathFromType(type);
            var projectPagePath = Path.Combine(ProjectName, PagesFolder, subPath);
            var entityName = CreateEntityNameFromInterface(type);
            var entityFullName = $"{CreateModelsNameSpace(type)}.{entityName}";
            var fileNameRazor = $"{entityName}Page{PageExtension}";
            var result = new Models.GeneratedItem(Common.UnitType.BlazorApp, Common.ItemType.IndexRazorPage)
            {
                FullName = CreateEntityFullNameFromInterface(type),
                FileExtension = PageExtension,
            };
            result.SubFilePath = Path.Combine(projectPagePath, fileNameRazor);
            StartCreateDataGridPageRazor(type, result.Source);

            result.Add($"@page \"/{entityName}\"");
            result.Add("@inherits Pages.DataGridPage");
            result.Add("@using Radzen;");
            result.Add($"@using TModel = {entityFullName};");
            result.Add(string.Empty);

            result.Add("@*EmbeddedBegin:File=_DataGridPage.template:Label=DefaultPage*@");
            result.Add("@*EmbeddedEnd:Label=DefaultPage*@");

            result.AddRange(EmbeddedTagReplacer.ReplaceEmbeddedTags(result.Source.Eject(), TemplatesSubPath, "@*", "*@", (st, et, rt, p) => EmbeddedTagManager.Handle(type, st, et, rt, p))
                                            .Select(l => l.Replace("File=TModel", $"File=_{entityName}.template"))
                                            .Select(l => l.Replace("Type=TModel", $"Type={entityFullName}"))
                                            );
            result.AddRange(EmbeddedTagReplacer.ReplaceEmbeddedTags(result.Source.Eject(), TemplatesSubPath, "@*", "*@", (st, et, rt, p) => EmbeddedTagManager.Handle(type, st, et, rt, p)));

            result.Add("@if (CanRender)");
            result.Add("{");
            result.Add($"\t<{CreateComponentsNameSpace(type)}.{entityName}DataGrid DataGridHandler=\"DataGridHandler\" />");
            result.Add("}");
            result.Add("else");
            result.Add("{");
            result.Add("\t<QnSTradingCompany.BlazorApp.Shared.Components.LoadingIndicator />");
            result.Add("}");

            FinishCreateDataGridPageRazor(type, result.Source);
            return result;
        }
        partial void StartCreateDataGridPageRazor(Type type, List<string> lines);
        partial void FinishCreateDataGridPageRazor(Type type, List<string> lines);

        private Contracts.IGeneratedItem CreateDataGridPageCode(Type type)
        {
            type.CheckArgument(nameof(type));

            var customUsings = new List<string>();
            var customNamespaceCode = new List<string>();
            var customClassCode = new List<string>();
            var subPath = CreateSubPathFromType(type);
            var projectPagePath = Path.Combine(ProjectName, PagesFolder, subPath);
            var entityName = CreateEntityNameFromInterface(type);
            var entityFullName = $"{CreateModelsNameSpace(type)}.{entityName}";
            var fileNameRazor = $"{entityName}Page{PageExtension}";
            var fileNameRazorCode = $"{fileNameRazor}{CodeExtension}";
            var filePathRazorCode = Path.Combine(ProjectName, subPath, fileNameRazorCode);
            var result = new Models.GeneratedItem(Common.UnitType.BlazorApp, Common.ItemType.IndexRazorPageCode)
            {
                FullName = CreateEntityFullNameFromInterface(type),
                FileExtension = CodeExtension,
            };
            result.SubFilePath = Path.Combine(projectPagePath, fileNameRazorCode);

            StartCreateDataGridPageCode(type, result.Source);
            if (File.Exists(filePathRazorCode))
            {
                var fileCode = File.ReadAllText(filePathRazorCode, Encoding.Default);

                foreach (var item in fileCode.GetAllTags(new string[] { $"/*{UsingsLabel}Begin*/", $"/*{UsingsLabel}nd*/" })
                                             .OrderBy(e => e.StartTagIndex))
                {
                    customUsings.Add(item.FullText);
                }
                foreach (var item in fileCode.GetAllTags(new string[] { $"/*{NamespaceCodeLabel}Begin*/", $"/*{NamespaceCodeLabel}End*/" })
                                             .OrderBy(e => e.StartTagIndex))
                {
                    customNamespaceCode.Add(item.FullText);
                }
                foreach (var item in fileCode.GetAllTags(new string[] { $"/*{ClassCodeLabel}Begin*/", $"/*{ClassCodeLabel}End*/" })
                                             .OrderBy(e => e.StartTagIndex))
                {
                    customClassCode.Add(item.FullText);
                }
            }

            result.Add("using System.Threading.Tasks;");
            result.Add("using Microsoft.AspNetCore.Components;");
            result.Add("using Radzen;");
            result.Add($"using {CreateComponentsNameSpace(type)};");
            result.Add($"using TContract = {type.FullName};");
            result.Add($"using TModel = {entityFullName};");

            result.AddRange(customUsings);

            result.Add($"namespace {CreatePagesNameSpace(type)}");
            result.Add("{");

            result.AddRange(customNamespaceCode);

            result.Add($"partial class {entityName}Page");
            result.Add("{");

            result.AddRange(customClassCode);

            result.Add("[Inject]");
            result.Add("protected DialogService DialogService { get; private set; }");

            result.Add(string.Empty);
            result.Add($"protected {entityName}DataGridHandler DataGridHandler" + " { get; private set; }");
            result.Add("protected Contracts.Client.IAdapterAccess<TContract> AdapterAccess { get; private set; }");

            result.Add("protected override Task OnFirstRenderAsync()");
            result.Add("{");
            result.Add($"DataGridHandler = new {entityName}DataGridHandler(this);");
            result.Add("DataGridHandler.PageSize = Settings.GetValueTyped<int>($\"{ComponentName}.{nameof(DataGridHandler.PageSize)}\", DataGridHandler.PageSize);");
            result.Add("return base.OnFirstRenderAsync();");
            result.Add("}");

            result.Add("}");
            result.Add("}");

            FinishCreateDataGridPageCode(type, result.Source);
            result.FormatCSharpCode();
            return result;
        }
        partial void StartCreateDataGridPageCode(Type type, List<string> lines);
        partial void FinishCreateDataGridPageCode(Type type, List<string> lines);

        public IEnumerable<Contracts.IGeneratedItem> CreateDisplayComponent(Type type, string typeLabel)
        {
            type.CheckArgument(nameof(type));
            typeLabel.CheckArgument(nameof(typeLabel));

            var result = new List<Contracts.IGeneratedItem>();

            StartCreateDisplayComponent(type, typeLabel);

            result.Add(CreateDataGridHandlerCode(type));
            result.Add(CreateDataGridComponentRazor(type));
            result.Add(CreateDataGridComponentCode(type));
            //result.Add(CreateDataGridComponentCommonCode(type));

            result.Add(CreateDataGridColumnsComponentRazor(type));
            result.Add(CreateDataGridColumnsComponentCode(type));

            result.Add(CreateDataGridDetailComponentRazor(type));
            result.Add(CreateDataGridDetailComponentCode(type));

            //result.Add(CreateFieldSetHandlerCode(type));
            //result.Add(CreateEditFieldSetComponentRazor(type));
            //result.Add(CreateEditFieldSetComponentCode(type));

            //result.Add(CreateEditFieldSetDetailComponentRazor(type));
            //result.Add(CreateEditFieldSetDetailComponentCode(type));

            result.Add(CreateEditFormComponentRazor(type));
            result.Add(CreateEditFormComponentCode(type));

            FinishCreateDisplayComponent(type, typeLabel);
            return result;
        }
        partial void StartCreateDisplayComponent(Type type, string typeLabel);
        partial void FinishCreateDisplayComponent(Type type, string typeLabel);

        #region Edit form component generation
        private Contracts.IGeneratedItem CreateEditFormComponentRazor(Type type)
        {
            type.CheckArgument(nameof(type));

            var subPath = CreateSubPathFromType(type);
            var projectSharedComponentsPath = Path.Combine(ProjectName, SharedFolder, ComponentsFolder, subPath);
            var entityName = CreateEntityNameFromInterface(type);
            var entityFullName = $"{CreateModelsNameSpace(type)}.{entityName}";
            var fileNameRazor = $"{entityName}EditForm{PageExtension}";
            var filePathRazor = Path.Combine(projectSharedComponentsPath, subPath, fileNameRazor);
            var result = new Models.GeneratedItem(Common.UnitType.BlazorApp, Common.ItemType.DataGridComponentRazor)
            {
                FullName = CreateEntityFullNameFromInterface(type),
                FileExtension = PageExtension,
            };
            result.SubFilePath = Path.Combine(projectSharedComponentsPath, fileNameRazor);

            StartCreateEditFormComponentRazor(type, result.Source);
            result.Add($"@using TContract = {type.FullName};");
            result.Add($"@using TModel = {entityFullName};");
            result.Add("@inherits EditFormComponent<TContract, TModel>");
            result.Add(string.Empty);

            result.Add("@*EmbeddedBegin:File=_EditFormComponent.template:Label=DefaultPage*@");
            result.Add("@*EmbeddedEnd:Label=DefaultPage*@");

            result.AddRange(EmbeddedTagReplacer.ReplaceEmbeddedTags(result.Source.Eject(), TemplatesSubPath, "@*", "*@", (st, et, rt, p) => EmbeddedTagManager.Handle(type, st, et, rt, p)));
            FinishCreateEditFormComponentRazor(type, result.Source);
            return result;
        }
        partial void StartCreateEditFormComponentRazor(Type type, List<string> lines);
        partial void FinishCreateEditFormComponentRazor(Type type, List<string> lines);

        private Contracts.IGeneratedItem CreateEditFormComponentCode(Type type)
        {
            type.CheckArgument(nameof(type));

            var subPath = CreateSubPathFromType(type);
            var projectSharedComponentsPath = Path.Combine(ProjectName, SharedFolder, ComponentsFolder, subPath);
            var entityName = CreateEntityNameFromInterface(type);
            var entityFullName = $"{CreateModelsNameSpace(type)}.{entityName}";
            var fileNameRazor = $"{entityName}EditForm{PageExtension}";
            var fileNameRazorCode = $"{fileNameRazor}{CodeExtension}";
            var result = new Models.GeneratedItem(Common.UnitType.BlazorApp, Common.ItemType.EditFormComponentCode)
            {
                FullName = CreateEntityFullNameFromInterface(type),
                FileExtension = PageExtension,
            };
            result.SubFilePath = Path.Combine(projectSharedComponentsPath, fileNameRazorCode);

            StartCreateEditFormComponentCode(type, result.Source);
            result.Add("using Microsoft.AspNetCore.Components;");
            result.Add("using Radzen;");
            result.Add("using System.Linq;");
            result.Add("using System.Threading.Tasks;");
            result.Add($"using TContract = {type.FullName};");
            result.Add($"using TModel = {entityFullName};");

            result.Add($"namespace {CreateComponentsNameSpace(type)}");
            result.Add("{");
            result.Add($"partial class {entityName}EditForm");
            result.Add("{");

            result.Add("@*EmbeddedBegin:File=_EditFormComponent.code:Label=DefaultPage*@");
            result.Add("@*EmbeddedEnd:Label=DefaultPage*@");

            result.AddRange(EmbeddedTagReplacer.ReplaceEmbeddedTags(result.Source.Eject(), TemplatesSubPath, "@*", "*@", (st, et, rt, p) => EmbeddedTagManager.Handle(type, st, et, rt, p)));

            result.Add("}");
            result.Add("}");

            FinishCreateEditFormComponentCode(type, result.Source);
            result.FormatCSharpCode();
            return result;
        }
        partial void StartCreateEditFormComponentCode(Type type, List<string> lines);
        partial void FinishCreateEditFormComponentCode(Type type, List<string> lines);
        #endregion Edit form component generation

        #region DataGrid component generation
        private Contracts.IGeneratedItem CreateDataGridHandlerCode(Type type)
        {
            type.CheckArgument(nameof(type));

            var subPath = CreateSubPathFromType(type);
            var projectSharedComponentsPath = Path.Combine(ProjectName, SharedFolder, ComponentsFolder, subPath);
            var entityName = CreateEntityNameFromInterface(type);
            var entityFullName = $"{CreateModelsNameSpace(type)}.{entityName}";
            var fileNameCode = $"{entityName}DataGridHandler{CodeExtension}";
            var result = new Models.GeneratedItem(Common.UnitType.BlazorApp, Common.ItemType.DataGridHandlerCode)
            {
                FullName = CreateEntityFullNameFromInterface(type),
                FileExtension = CodeExtension,
            };
            result.SubFilePath = Path.Combine(projectSharedComponentsPath, fileNameCode);

            StartCreateDataGridHandlerCode(type, result.Source);
            result.Add($"using TContract = {type.FullName};");
            result.Add($"using TModel = {entityFullName};");

            result.Add($"namespace {CreateComponentsNameSpace(type)}");
            result.Add("{");

            result.Add($"public partial class {entityName}DataGridHandler : Modules.DataGrid.DataGridHandler<TContract, TModel>");
            result.Add("{");

            result.Add($"public {entityName}DataGridHandler(Pages.ModelPage modelPage)");
            result.Add(" : base(modelPage)");
            result.Add("{");
            result.Add("}");

            result.Add("}");
            result.Add("}");

            FinishCreateDataGridHandlerCode(type, result.Source);
            result.FormatCSharpCode();
            return result;
        }
        partial void StartCreateDataGridHandlerCode(Type type, List<string> lines);
        partial void FinishCreateDataGridHandlerCode(Type type, List<string> lines);

        private Contracts.IGeneratedItem CreateDataGridComponentRazor(Type type)
        {
            type.CheckArgument(nameof(type));

            var subPath = CreateSubPathFromType(type);
            var projectSharedComponentsPath = Path.Combine(ProjectName, SharedFolder, ComponentsFolder, subPath);
            var entityName = CreateEntityNameFromInterface(type);
            var entityFullName = $"{CreateModelsNameSpace(type)}.{entityName}";
            var fileNameRazor = $"{entityName}DataGrid{PageExtension}";
            var filePathRazor = Path.Combine(projectSharedComponentsPath, subPath, fileNameRazor);
            var result = new Models.GeneratedItem(Common.UnitType.BlazorApp, Common.ItemType.DataGridComponentRazor)
            {
                FullName = CreateEntityFullNameFromInterface(type),
                FileExtension = PageExtension,
            };
            result.SubFilePath = Path.Combine(projectSharedComponentsPath, fileNameRazor);

            StartCreateDataGridComponentRazor(type, result.Source);
            result.Add("@inherits DataGridComponent");
            result.Add("@using Radzen;");
            result.Add($"@using TModel = {entityFullName};");
            result.Add(string.Empty);

            result.Add("@*EmbeddedBegin:File=_DataGridComponent.template:Label=DefaultPage*@");
            result.Add("@*EmbeddedEnd:Label=DefaultPage*@");

            result.AddRange(EmbeddedTagReplacer.ReplaceEmbeddedTags(result.Source.Eject(), TemplatesSubPath, "@*", "*@", (st, et, rt, p) => EmbeddedTagManager.Handle(type, st, et, rt, p))
                                            .Select(l => l.Replace("File=TModel", $"File=_{entityName}.template"))
                                            .Select(l => l.Replace("Type=TModel", $"Type={entityFullName}"))
                                            .Select(l => l.Replace("<DataGridDetail ", $"<{entityName}DataGridDetail "))
                                            .Select(l => l.Replace("<DataGridColumns ", $"<{entityName}DataGridColumns "))
                                            .Select(l => l.Replace("<EditFieldSetDetail ", $"<{entityName}EditFieldSetDetail "))
                                            );

            FinishCreateDataGridComponentRazor(type, result.Source);
            return result;
        }
        partial void StartCreateDataGridComponentRazor(Type type, List<string> lines);
        partial void FinishCreateDataGridComponentRazor(Type type, List<string> lines);

        private Contracts.IGeneratedItem CreateDataGridComponentCode(Type type)
        {
            type.CheckArgument(nameof(type));

            var subPath = CreateSubPathFromType(type);
            var projectSharedComponentsPath = Path.Combine(ProjectName, SharedFolder, ComponentsFolder, subPath);
            var entityName = CreateEntityNameFromInterface(type);
            var entityFullName = $"{CreateModelsNameSpace(type)}.{entityName}";
            var fileNameRazor = $"{entityName}DataGrid{PageExtension}";
            var fileNameRazorCode = $"{fileNameRazor}{CodeExtension}";
            var result = new Models.GeneratedItem(Common.UnitType.BlazorApp, Common.ItemType.DataGridComponentCode)
            {
                FullName = CreateEntityFullNameFromInterface(type),
                FileExtension = PageExtension,
            };
            result.SubFilePath = Path.Combine(projectSharedComponentsPath, fileNameRazorCode);

            StartCreateDataGridComponentCode(type, result.Source);
            result.Add("using Microsoft.AspNetCore.Components;");
            result.Add("using Radzen;");
            result.Add("using System.Linq;");
            result.Add("using System.Threading.Tasks;");
            result.Add($"using TContract = {type.FullName};");
            result.Add($"using TModel = {entityFullName};");

            result.Add($"namespace {CreateComponentsNameSpace(type)}");
            result.Add("{");
            result.Add($"partial class {entityName}DataGrid");
            result.Add("{");

            result.Add("[Parameter]");
            result.Add($"public {entityName}DataGridHandler DataGridHandler" + " { get; set; }");

            result.Add($"public override string ForPrefix => \"{entityName}\";");
            result.Add("}");
            result.Add("}");

            FinishCreateDataGridComponentCode(type, result.Source);
            result.FormatCSharpCode();
            return result;
        }
        partial void StartCreateDataGridComponentCode(Type type, List<string> lines);
        partial void FinishCreateDataGridComponentCode(Type type, List<string> lines);

        private Contracts.IGeneratedItem CreateDataGridDetailComponentRazor(Type type)
        {
            type.CheckArgument(nameof(type));

            var subPath = CreateSubPathFromType(type);
            var projectSharedComponentsPath = Path.Combine(ProjectName, SharedFolder, ComponentsFolder, subPath);
            var entityName = CreateEntityNameFromInterface(type);
            var entityFullName = $"{CreateModelsNameSpace(type)}.{entityName}";
            var fileNameRazor = $"{entityName}DataGridDetail{PageExtension}";
            var filePathRazor = Path.Combine(projectSharedComponentsPath, subPath, fileNameRazor);
            var result = new Models.GeneratedItem(Common.UnitType.BlazorApp, Common.ItemType.DataGridDetailComponentRazor)
            {
                FullName = CreateEntityFullNameFromInterface(type),
                FileExtension = PageExtension,
            };
            result.SubFilePath = Path.Combine(projectSharedComponentsPath, fileNameRazor);

            StartCreateDataGridDetailComponentRazor(type, result.Source);
            result.Add("@inherits DataGridDetailComponent");
            result.Add("@using Radzen;");
            result.Add($"@using TModel = {entityFullName};");
            result.Add(string.Empty);

            result.Add("@*EmbeddedBegin:File=_DataGridDetailComponent.template:Label=DefaultPage*@");
            result.Add("@*EmbeddedEnd:Label=DefaultPage*@");

            result.AddRange(EmbeddedTagReplacer.ReplaceEmbeddedTags(result.Source.Eject(), TemplatesSubPath, "@*", "*@", (st, et, rt, p) => EmbeddedTagManager.Handle(type, st, et, rt, p))
                                            .Select(l => l.Replace("File=TModel", $"File=_{entityName}.template"))
                                            .Select(l => l.Replace("Type=TModel", $"Type={entityFullName}")));

            FinishCreateDataGridDetailComponentRazor(type, result.Source);
            return result;
        }
        partial void StartCreateDataGridDetailComponentRazor(Type type, List<string> lines);
        partial void FinishCreateDataGridDetailComponentRazor(Type type, List<string> lines);

        private Contracts.IGeneratedItem CreateDataGridDetailComponentCode(Type type)
        {
            type.CheckArgument(nameof(type));

            var subPath = CreateSubPathFromType(type);
            var projectSharedComponentsPath = Path.Combine(ProjectName, SharedFolder, ComponentsFolder, subPath);
            var entityName = CreateEntityNameFromInterface(type);
            var entityFullName = $"{CreateModelsNameSpace(type)}.{entityName}";
            var fileNameRazor = $"{entityName}DataGridDetail{PageExtension}";
            var fileNameRazorCode = $"{fileNameRazor}{CodeExtension}";
            var result = new Models.GeneratedItem(Common.UnitType.BlazorApp, Common.ItemType.DataGridDetailComponentCode)
            {
                FullName = CreateEntityFullNameFromInterface(type),
                FileExtension = PageExtension,
            };
            result.SubFilePath = Path.Combine(projectSharedComponentsPath, fileNameRazorCode);

            StartCreateDataGridDetailComponentCode(type, result.Source);
            result.Add("using Microsoft.AspNetCore.Components;");
            result.Add($"using TModel = {entityFullName};");

            result.Add($"namespace {CreateComponentsNameSpace(type)}");
            result.Add("{");
            result.Add($"partial class {entityName}DataGridDetail");
            result.Add("{");

            result.Add("[Parameter]");
            result.Add($"public {entityName}DataGridHandler MasterDataGridHandler" + " { get; set; }");

            result.Add($"public override string ForPrefix => \"{entityName}\";");
            result.Add("protected Pages.ModelPage ModelPage => MasterDataGridHandler.ModelPage;");
            result.Add("private TModel parentModel;");
            result.Add("protected TModel ParentModel");
            result.Add("{");
            result.Add("get");
            result.Add("{");
            result.Add("if (parentModel == null)");
            result.Add("{");
            result.Add("parentModel = MasterDataGridHandler.ExpandModel;");
            result.Add("}");
            result.Add("return parentModel;");
            result.Add("}");
            result.Add("}");

            result.Add("}");
            result.Add("}");

            FinishCreateDataGridDetailComponentCode(type, result.Source);
            result.FormatCSharpCode();
            return result;
        }
        partial void StartCreateDataGridDetailComponentCode(Type type, List<string> lines);
        partial void FinishCreateDataGridDetailComponentCode(Type type, List<string> lines);

        private Contracts.IGeneratedItem CreateDataGridColumnsComponentRazor(Type type)
        {
            type.CheckArgument(nameof(type));

            var subPath = CreateSubPathFromType(type);
            var projectSharedComponentsPath = Path.Combine(ProjectName, SharedFolder, ComponentsFolder, subPath);
            var entityName = CreateEntityNameFromInterface(type);
            var entityFullName = $"{CreateModelsNameSpace(type)}.{entityName}";
            var fileNameRazor = $"{entityName}DataGridColumns{PageExtension}";
            var result = new Models.GeneratedItem(Common.UnitType.BlazorApp, Common.ItemType.DataGridColumnsComponentRazor)
            {
                FullName = CreateEntityFullNameFromInterface(type),
                FileExtension = PageExtension,
            };
            result.SubFilePath = Path.Combine(projectSharedComponentsPath, fileNameRazor);

            StartCreateDataGridColumnsComponentRazor(type, result.Source);
            result.Add("@inherits DataGridColumnsComponent");
            result.Add("@using Radzen;");
            result.Add("@using CommonBase.Extensions;");
            result.Add($"@using TContract = {type.FullName};");
            result.Add($"@using TModel = {entityFullName};");
            result.Add(string.Empty);

            result.Add("@*EmbeddedBegin:File=_DataGridColumnsComponent.template:Label=DefaultPage*@");
            result.Add("@*EmbeddedEnd:Label=DefaultPage*@");

            result.AddRange(EmbeddedTagReplacer.ReplaceEmbeddedTags(result.Source.Eject(), TemplatesSubPath, "@*", "*@", (st, et, rt, p) => EmbeddedTagManager.Handle(type, st, et, rt, p)));

            FinishCreateDataGridColumnsComponentRazor(type, result.Source);
            return result;
        }
        partial void StartCreateDataGridColumnsComponentRazor(Type type, List<string> lines);
        partial void FinishCreateDataGridColumnsComponentRazor(Type type, List<string> lines);

        private Contracts.IGeneratedItem CreateDataGridColumnsComponentCode(Type type)
        {
            type.CheckArgument(nameof(type));

            var subPath = CreateSubPathFromType(type);
            var projectSharedComponentsPath = Path.Combine(ProjectName, SharedFolder, ComponentsFolder, subPath);
            var entityName = CreateEntityNameFromInterface(type);
            var entityFullName = $"{CreateModelsNameSpace(type)}.{entityName}";
            var fileNameRazor = $"{entityName}DataGridColumns{PageExtension}";
            var fileNameRazorCode = $"{fileNameRazor}{CodeExtension}";
            var result = new Models.GeneratedItem(Common.UnitType.BlazorApp, Common.ItemType.DataGridColumnsComponentCode)
            {
                FullName = CreateEntityFullNameFromInterface(type),
                FileExtension = PageExtension,
            };
            result.SubFilePath = Path.Combine(projectSharedComponentsPath, fileNameRazorCode);

            StartCreateDataGridColumnsComponentCode(type, result.Source);
            result.Add("using Microsoft.AspNetCore.Components;");
            result.Add("using Radzen;");
            result.Add("using System;");
            result.Add("using System.Linq;");
            result.Add("using System.Threading.Tasks;");
            result.Add($"using TContract = {type.FullName};");
            result.Add($"using TModel = {entityFullName};");

            result.Add($"namespace {CreateComponentsNameSpace(type)}");
            result.Add("{");
            result.Add($"partial class {entityName}DataGridColumns");
            result.Add("{");

            result.Add("[Parameter]");
            result.Add($"public {entityName}DataGridHandler DataGridHandler" + " { get; set; }");

            result.Add($"public override string ForPrefix => \"{entityName}\";");

            result.Add("protected override Task OnFirstRenderAsync()");
            result.Add("{");
            result.Add("DataGridHandler.ModelItems = DataGridHandler.ModelItems.Union(GetAllDisplayProperties().Where(e => e.ScaffoldItem && e.Visible && e.IsModelItem).Select(e => e.PropertyName)).Distinct().ToArray();");
            result.Add("return base.OnFirstRenderAsync();");
            result.Add("}");

            result.Add("protected override Type GetModelType()");
            result.Add("{");
            result.Add("var handled = false;");
            result.Add("var result = default(Type);");
            result.Add("BeforeGetModelType(ref result, ref handled);");
            result.Add("if (handled == false)");
            result.Add("{");
            result.Add("result = typeof(TModel);");
            result.Add("}");
            result.Add("AfterGetModelType(result);");
            result.Add("return result;");
            result.Add("}");
            result.Add("static partial void BeforeGetModelType(ref Type modelType, ref bool handled);");
            result.Add("static partial void AfterGetModelType(Type modelType);");

            result.Add("}");
            result.Add("}");

            FinishCreateDataGridColumnsComponentCode(type, result.Source);
            result.FormatCSharpCode();
            return result;
        }
        partial void StartCreateDataGridColumnsComponentCode(Type type, List<string> lines);
        partial void FinishCreateDataGridColumnsComponentCode(Type type, List<string> lines);
        #endregion DataGrid component generation

        #region FieldSet component generation
        private Contracts.IGeneratedItem CreateFieldSetHandlerCode(Type type)
        {
            type.CheckArgument(nameof(type));

            var subPath = CreateSubPathFromType(type);
            var projectSharedComponentsPath = Path.Combine(ProjectName, SharedFolder, ComponentsFolder, subPath);
            var entityName = CreateEntityNameFromInterface(type);
            var entityFullName = $"{CreateModelsNameSpace(type)}.{entityName}";
            var fileNameRazorCode = $"{entityName}FieldSetHandler{CodeExtension}";
            var result = new Models.GeneratedItem(Common.UnitType.BlazorApp, Common.ItemType.FieldSetHandlerCode)
            {
                FullName = CreateEntityFullNameFromInterface(type),
                FileExtension = PageExtension,
            };
            result.SubFilePath = Path.Combine(projectSharedComponentsPath, fileNameRazorCode);

            StartCreateFieldSetHandlerCode(type, result.Source);
            result.Add($"using TContract = {type.FullName};");
            result.Add($"using TModel = {entityFullName};");

            result.Add($"namespace {CreateComponentsNameSpace(type)}");
            result.Add("{");

            result.Add($"public partial class {entityName}FieldSetHandler : FieldSetHandler<TContract, TModel>");
            result.Add("{");

            result.Add($"public {entityName}FieldSetHandler(Pages.ModelPage modelPage, Contracts.Client.IAdapterAccess<TContract> adapterAccess)");
            result.Add(" : base(modelPage, adapterAccess)");
            result.Add("{");
            result.Add("}");

            result.Add("}");
            result.Add("}");

            FinishCreateFieldSetHandlerCode(type, result.Source);
            result.FormatCSharpCode();
            return result;
        }
        partial void StartCreateFieldSetHandlerCode(Type type, List<string> lines);
        partial void FinishCreateFieldSetHandlerCode(Type type, List<string> lines);

        private Contracts.IGeneratedItem CreateEditFieldSetComponentRazor(Type type)
        {
            type.CheckArgument(nameof(type));

            var subPath = CreateSubPathFromType(type);
            var projectSharedComponentsPath = Path.Combine(ProjectName, SharedFolder, ComponentsFolder, subPath);
            var entityName = CreateEntityNameFromInterface(type);
            var entityFullName = $"{CreateModelsNameSpace(type)}.{entityName}";
            var fileNameRazor = $"{entityName}EditFieldSet{PageExtension}";
            var filePathRazor = Path.Combine(projectSharedComponentsPath, subPath, fileNameRazor);
            var result = new Models.GeneratedItem(Common.UnitType.BlazorApp, Common.ItemType.FieldSetComponentRazor)
            {
                FullName = CreateEntityFullNameFromInterface(type),
                FileExtension = PageExtension,
            };
            result.SubFilePath = Path.Combine(projectSharedComponentsPath, fileNameRazor);

            StartCreateEditFieldSetComponentRazor(type, result.Source);
            result.Add("@inherits FieldSetComponent");
            result.Add("@using Radzen;");
            result.Add($"@using TModel = {entityFullName};");
            result.Add(string.Empty);

            result.Add("@*EmbeddedBegin:File=_EditFieldSetComponent.template:Label=DefaultPage*@");
            result.Add("@*EmbeddedEnd:Label=DefaultPage*@");

            result.AddRange(EmbeddedTagReplacer.ReplaceEmbeddedTags(result.Source.Eject(), TemplatesSubPath, "@*", "*@", (st, et, rt, p) => EmbeddedTagManager.Handle(type, st, et, rt, p))
                                               .Select(l => l.Replace("File=TModel", $"File=_{entityName}.template"))
                                               .Select(l => l.Replace("Type=TModel", $"Type={entityFullName}"))
                                               .Select(l => l.Replace("<EditFieldSetDetail ", $"<{entityName}EditFieldSetDetail "))
                                               );

            FinishCreateEditFieldSetComponentRazor(type, result.Source);
            return result;
        }
        partial void StartCreateEditFieldSetComponentRazor(Type type, List<string> lines);
        partial void FinishCreateEditFieldSetComponentRazor(Type type, List<string> lines);

        private Contracts.IGeneratedItem CreateEditFieldSetComponentCode(Type type)
        {
            type.CheckArgument(nameof(type));

            var subPath = CreateSubPathFromType(type);
            var projectSharedComponentsPath = Path.Combine(ProjectName, SharedFolder, ComponentsFolder, subPath);
            var entityName = CreateEntityNameFromInterface(type);
            var fileNameRazor = $"{entityName}EditFieldSet{PageExtension}";
            var fileNameRazorCode = $"{fileNameRazor}{CodeExtension}";
            var result = new Models.GeneratedItem(Common.UnitType.BlazorApp, Common.ItemType.FieldSetComponentCode)
            {
                FullName = CreateEntityFullNameFromInterface(type),
                FileExtension = PageExtension,
            };
            result.SubFilePath = Path.Combine(projectSharedComponentsPath, fileNameRazorCode);

            StartCreateEditFieldSetComponentCode(type, result.Source);
            result.Add("using Microsoft.AspNetCore.Components;");
            result.Add("using Radzen;");

            result.Add($"namespace {CreateComponentsNameSpace(type)}");
            result.Add("{");
            result.Add($"partial class {entityName}EditFieldSet");
            result.Add("{");

            result.Add("[Parameter]");
            result.Add("public int Id" + " { get; set; }");

            result.Add("[Parameter]");
            result.Add($"public {entityName}FieldSetHandler FieldSetHandler" + " { get; set; }");

            result.Add("[Inject]");
            result.Add($"protected DialogService DialogService" + " { get; private set; }");

            result.Add("}");
            result.Add("}");

            FinishCreateEditFieldSetComponentCode(type, result.Source);
            result.FormatCSharpCode();
            return result;
        }
        partial void StartCreateEditFieldSetComponentCode(Type type, List<string> lines);
        partial void FinishCreateEditFieldSetComponentCode(Type type, List<string> lines);

        private Contracts.IGeneratedItem CreateEditFieldSetDetailComponentRazor(Type type)
        {
            type.CheckArgument(nameof(type));

            var subPath = CreateSubPathFromType(type);
            var projectSharedComponentsPath = Path.Combine(ProjectName, SharedFolder, ComponentsFolder, subPath);
            var entityName = CreateEntityNameFromInterface(type);
            var entityFullName = $"{CreateModelsNameSpace(type)}.{entityName}";
            var fileNameRazor = $"{entityName}EditFieldSetDetail{PageExtension}";
            var filePathRazor = Path.Combine(projectSharedComponentsPath, subPath, fileNameRazor);
            var result = new Models.GeneratedItem(Common.UnitType.BlazorApp, Common.ItemType.FieldSetDetailComponentRazor)
            {
                FullName = CreateEntityFullNameFromInterface(type),
                FileExtension = PageExtension,
            };
            result.SubFilePath = Path.Combine(projectSharedComponentsPath, fileNameRazor);

            StartCreateEditFieldSetDetailComponentRazor(type, result.Source);
            result.Add("@inherits FieldSetComponent");
            result.Add("@using Radzen;");
            result.Add($"@using TModel = {entityFullName};");
            result.Add(string.Empty);
            result.AddRange(BlazorUIGenerator.CreateAddFieldSet(type).Select(rb => rb.ToString()));

            result.Add("@*EmbeddedBegin:File=_EditFieldSetDetailComponent.template:Label=DefaultPage*@");
            result.Add("@*EmbeddedEnd:Label=DefaultPage*@");

            result.AddRange(EmbeddedTagReplacer.ReplaceEmbeddedTags(result.Source.Eject(), TemplatesSubPath, "@*", "*@", (st, et, rt, p) => EmbeddedTagManager.Handle(type, st, et, rt, p))
                                            .Select(l => l.Replace("File=TModel", $"File=_{entityName}.template"))
                                            .Select(l => l.Replace("Type=TModel", $"Type={entityFullName}")));

            FinishCreateEditFieldSetDetailComponentRazor(type, result.Source);
            return result;
        }
        partial void StartCreateEditFieldSetDetailComponentRazor(Type type, List<string> lines);
        partial void FinishCreateEditFieldSetDetailComponentRazor(Type type, List<string> lines);

        private Contracts.IGeneratedItem CreateEditFieldSetDetailComponentCode(Type type)
        {
            type.CheckArgument(nameof(type));

            var subPath = CreateSubPathFromType(type);
            var projectSharedComponentsPath = Path.Combine(ProjectName, SharedFolder, ComponentsFolder, subPath);
            var entityName = CreateEntityNameFromInterface(type);
            var entityFullName = $"{CreateModelsNameSpace(type)}.{entityName}";
            var fileNameRazor = $"{entityName}EditFieldSetDetail{PageExtension}";
            var fileNameRazorCode = $"{fileNameRazor}{CodeExtension}";
            var result = new Models.GeneratedItem(Common.UnitType.BlazorApp, Common.ItemType.FieldSetDetailComponentCode)
            {
                FullName = CreateEntityFullNameFromInterface(type),
                FileExtension = PageExtension,
            };
            result.SubFilePath = Path.Combine(projectSharedComponentsPath, fileNameRazorCode);

            StartCreateEditFieldSetDetailComponentCode(type, result.Source);
            result.Add("using Microsoft.AspNetCore.Components;");
            result.Add("using Radzen;");
            result.Add("using System;");

            result.Add($"namespace {CreateComponentsNameSpace(type)}");
            result.Add("{");
            result.Add($"partial class {entityName}EditFieldSetDetail");
            result.Add("{");

            result.Add("[Parameter]");
            result.Add($"public {entityFullName} EditModel" + " { get; set; }");

            result.Add("[Parameter]");
            result.Add("public Func<string, string> LocalTranslate { get; set; }");

            result.Add("[Parameter]");
            result.Add("public Func<string, string> LocalTranslateFor { get; set; }");

            result.Add("}");
            result.Add("}");

            FinishCreateEditFieldSetDetailComponentCode(type, result.Source);
            result.FormatCSharpCode();
            return result;
        }
        partial void StartCreateEditFieldSetDetailComponentCode(Type type, List<string> lines);
        partial void FinishCreateEditFieldSetDetailComponentCode(Type type, List<string> lines);
		#endregion FieldSet component generation
	}
}
//MdEnd
