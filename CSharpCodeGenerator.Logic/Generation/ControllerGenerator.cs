//@QnSCodeCopy
//MdStart
using CommonBase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpCodeGenerator.Logic.Generation
{
    internal partial class ControllerGenerator : ClassGenerator, Contracts.IControllerGenerator
    {
        protected ControllerGenerator(SolutionProperties solutionProperties)
            : base(solutionProperties)
        {
        }
        public new static ControllerGenerator Create(SolutionProperties solutionProperties)
        {
            return new ControllerGenerator(solutionProperties);
        }

        #region General
        private static bool CanCreate(string generationName, Type type)
        {
            bool create = true;

            CanCreateController(generationName, type, ref create);
            if (generationName.Equals(nameof(CreateBusinessController)))
            {
                CanCreateLogicController(type, ref create);
            }
            else if (generationName.Equals(nameof(CreatePersistenceController)))
            {
                CanCreateLogicController(type, ref create);
            }
            else if (generationName.Equals(nameof(CreateShadowController)))
            {
                CanCreateLogicController(type, ref create);
            }
            else if (generationName.Equals(nameof(CreateWebApiControllers)))
            {
                CanCreateWebApiController(type, ref create);
            }
            return create;
        }
        static partial void CanCreateController(string generationName, Type type, ref bool create);
        static partial void CanCreateLogicController(Type type, ref bool create);
        static partial void CanCreateWebApiController(Type type, ref bool create);
        #endregion General

        #region LogicController
        public string LogicControllerNameSpace => $"{SolutionProperties.LogicProjectName}.{StaticLiterals.ControllersFolder}";
        public string CreateLogicControllerNameSpace(Type type)
        {
            type.CheckArgument(nameof(type));

            return $"{LogicControllerNameSpace}.{GeneratorObject.CreateSubNamespaceFromType(type)}";
        }
        private static List<string> InitLogicControllerAttributes(Type type)
        {
            var result = new List<string>();

            if (type.FullName.EndsWith(".Business.Account.IAppAccess")
                || type.FullName.EndsWith(".Persistence.Account.IIdentity")
                || type.FullName.EndsWith(".Persistence.Account.IRole")
                || type.FullName.EndsWith(".Persistence.Account.IIdentityXRole")
                || type.FullName.EndsWith(".Persistence.Account.ILoginSession")
                )
            {
                result.Add("[Logic.Modules.Security.Authorize(\"SysAdmin\", \"AppAdmin\")]");
            }
            return result;
        }

        static partial void CreateLogicControllerAttributes(Type type, List<string> attributes);

        public IEnumerable<Contracts.IGeneratedItem> CreateBusinessControllers()
        {
            var result = new List<Contracts.IGeneratedItem>();
            var contractsProject = ContractsProject.Create(SolutionProperties);

            foreach (var type in contractsProject.BusinessTypes)
            {
                if (CanCreate(nameof(CreateBusinessControllers), type))
                {
                    result.Add(CreateBusinessController(type));
                }
            }
            return result;
        }
        private Contracts.IGeneratedItem CreateBusinessController(Type type)
        {
            type.CheckArgument(nameof(type));

            Contracts.IGeneratedItem result;
            var itfcs = type.GetInterfaces();

            if (itfcs.Length > 0 && itfcs[0].Name.Equals(StaticLiterals.ICompositeName) && itfcs[0].GetGenericArguments().Length == 3)
            {
                result = CreateCompositeBusinessController(type);
            }
            else if (itfcs.Length > 0 && itfcs[0].Name.Equals(StaticLiterals.IOneToAnotherName) && itfcs[0].GetGenericArguments().Length == 2)
            {
                result = CreateOneToAnotherBusinessController(type);
            }
            else if (itfcs.Length > 0 && itfcs[0].Name.Equals(StaticLiterals.IOneToManyName) && itfcs[0].GetGenericArguments().Length == 2)
            {
                result = CreateOneToManyBusinessController(type);
            }
            else
            {
                result = CreateDefaultBusinessController(type);
            }
            return result;
        }
        private Contracts.IGeneratedItem CreateDefaultBusinessController(Type type)
        {
            type.CheckArgument(nameof(type));

            var entityName = CreateEntityNameFromInterface(type);
            var subNameSpace = CreateSubNamespaceFromType(type);
            var entityType = $"{StaticLiterals.EntitiesFolder}.{subNameSpace}.{entityName}";
            var controllerName = $"{entityName}Controller";
            var baseControllerName = "BusinessControllerAdapter";
            var result = new Models.GeneratedItem(Common.UnitType.Logic, Common.ItemType.BusinessController)
            {
                FullName = CreateLogicControllerFullNameFromInterface(type),
                FileExtension = StaticLiterals.CSharpFileExtension,
            };
            result.SubFilePath = $"{result.FullName}{result.FileExtension}";
            ConvertGenericPersistenceControllerName(type, ref baseControllerName);
            CreateLogicControllerAttributes(type, result.Source);
            result.Add($"sealed partial class {controllerName} : {baseControllerName}<{type.FullName}, {entityType}>");
            result.Add("{");
            result.AddRange(CreatePartialStaticConstrutor(controllerName));
            result.AddRange(CreatePartialConstrutor("public", controllerName, $"{SolutionProperties.DataContextFolder}.IContext context", "base(context)"));
            result.AddRange(CreatePartialConstrutor("public", controllerName, "ControllerObject controller", "base(controller)", null, false));
            result.Add("}");
            result.EnvelopeWithANamespace(CreateLogicControllerNameSpace(type));
            result.FormatCSharpCode();
            return result;
        }
        private Contracts.IGeneratedItem CreateCompositeBusinessController(Type type)
        {
            type.CheckArgument(nameof(type));

            var entityName = CreateEntityNameFromInterface(type);
            var subNameSpace = CreateSubNamespaceFromType(type);
            var entityType = $"{StaticLiterals.EntitiesFolder}.{subNameSpace}.{entityName}";
            var controllerName = $"{entityName}Controller";
            var baseControllerName = "GenericCompositeController";
            var interfaceTypes = type.GetInterfaces();
            var connectorGenericType = interfaceTypes[0].GetGenericArguments()[0];
            var oneGenericType = interfaceTypes[0].GetGenericArguments()[1];
            var anotherGenericType = interfaceTypes[0].GetGenericArguments()[2];
            var connectorEntityType = $"{CreateEntityFullNameFromInterface(connectorGenericType)}";
            var oneEntityType = $"{CreateEntityFullNameFromInterface(oneGenericType)}";
            var anotherEntityType = $"{CreateEntityFullNameFromInterface(anotherGenericType)}";
            var connectorCtrlType = $"{CreateLogicControllerFullNameFromInterface(connectorGenericType)}";
            var oneCtrlType = $"{CreateLogicControllerFullNameFromInterface(oneGenericType)}";
            var anotherCtrlType = $"{CreateLogicControllerFullNameFromInterface(anotherGenericType)}";
            var result = new Models.GeneratedItem(Common.UnitType.Logic, Common.ItemType.BusinessController)
            {
                FullName = CreateLogicControllerFullNameFromInterface(type),
                FileExtension = StaticLiterals.CSharpFileExtension,
            };
            result.SubFilePath = $"{result.FullName}{result.FileExtension}";
            ConvertGenericPersistenceControllerName(type, ref baseControllerName);
            CreateLogicControllerAttributes(type, result.Source);
            result.Add($"sealed partial class {controllerName} : {baseControllerName}<{type.FullName}, {entityType}, {connectorGenericType.FullName}, {connectorEntityType}, {oneGenericType.FullName}, {oneEntityType}, {anotherGenericType.FullName}, {anotherEntityType}>");
            result.Add("{");

            var initStatements = new List<string>()
            {
                $"connectorEntityController = new {connectorCtrlType}(this);",
                $"oneEntityController = new {oneCtrlType}(this);",
                $"anotherEntityController = new {anotherCtrlType}(this);",
            };

            result.AddRange(CreatePartialStaticConstrutor(controllerName));
            result.AddRange(CreatePartialConstrutor("public", controllerName, $"{SolutionProperties.DataContextFolder}.IContext context", "base(context)", initStatements));
            result.AddRange(CreatePartialConstrutor("public", controllerName, "ControllerObject controller", "base(controller)", initStatements, false));

            result.Add($"private {connectorCtrlType} connectorEntityController = null;");
            result.Add($"protected override GenericController<{connectorGenericType.FullName}, {connectorEntityType}> ConnectorEntityController");
            result.Add("{");
            result.Add($"get => connectorEntityController; // ?? (connectorEntityController =  new {connectorCtrlType}(this));");
            result.Add($"set => connectorEntityController = value as {connectorCtrlType};");
            result.Add("}");

            result.Add($"private {oneCtrlType} oneEntityController = null;");
            result.Add($"protected override GenericController<{oneGenericType.FullName}, {oneEntityType}> OneEntityController");
            result.Add("{");
            result.Add($"get => oneEntityController; // ?? (oneEntityController =  new {oneCtrlType}(this));");
            result.Add($"set => oneEntityController = value as {oneCtrlType};");
            result.Add("}");

            result.Add($"private {anotherCtrlType} anotherEntityController = null;");
            result.Add($"protected override GenericController<{anotherGenericType.FullName}, {anotherEntityType}> AnotherEntityController");
            result.Add("{");
            result.Add($"get => anotherEntityController; // ?? (anotherEntityController =  new {anotherCtrlType}(this));");
            result.Add($"set => anotherEntityController = value as {anotherCtrlType};");
            result.Add("}");

            result.Add("}");
            result.EnvelopeWithANamespace(CreateLogicControllerNameSpace(type));
            result.FormatCSharpCode();
            return result;
        }
        private Contracts.IGeneratedItem CreateOneToAnotherBusinessController(Type type)
        {
            type.CheckArgument(nameof(type));

            var entityName = CreateEntityNameFromInterface(type);
            var subNameSpace = CreateSubNamespaceFromType(type);
            var entityType = $"{StaticLiterals.EntitiesFolder}.{subNameSpace}.{entityName}";
            var controllerName = $"{entityName}Controller";
            var baseControllerName = "GenericOneToAnotherController";
            var interfaceTypes = type.GetInterfaces();
            var oneGenericType = interfaceTypes[0].GetGenericArguments()[0];
            var anotherGenericType = interfaceTypes[0].GetGenericArguments()[1];
            var oneEntityType = $"{CreateEntityFullNameFromInterface(oneGenericType)}";
            var anotherEntityType = $"{CreateEntityFullNameFromInterface(anotherGenericType)}";
            var oneCtrlType = $"{CreateLogicControllerFullNameFromInterface(oneGenericType)}";
            var anotherCtrlType = $"{CreateLogicControllerFullNameFromInterface(anotherGenericType)}";
            var result = new Models.GeneratedItem(Common.UnitType.Logic, Common.ItemType.BusinessController)
            {
                FullName = CreateLogicControllerFullNameFromInterface(type),
                FileExtension = StaticLiterals.CSharpFileExtension,
            };
            result.SubFilePath = $"{result.FullName}{result.FileExtension}";
            ConvertGenericPersistenceControllerName(type, ref baseControllerName);
            CreateLogicControllerAttributes(type, result.Source);
            result.Add($"sealed partial class {controllerName} : {baseControllerName}<{type.FullName}, {entityType}, {oneGenericType.FullName}, {oneEntityType}, {anotherGenericType.FullName}, {anotherEntityType}>");
            result.Add("{");

            var initStatements = new List<string>()
            {
                $"oneEntityController = new {oneCtrlType}(this);",
                $"anotherEntityController = new {anotherCtrlType}(this);",
            };

            result.AddRange(CreatePartialStaticConstrutor(controllerName));
            result.AddRange(CreatePartialConstrutor("public", controllerName, $"{SolutionProperties.DataContextFolder}.IContext context", "base(context)", initStatements));
            result.AddRange(CreatePartialConstrutor("public", controllerName, "ControllerObject controller", "base(controller)", initStatements, false));

            result.Add($"private {oneCtrlType} oneEntityController = null;");
            result.Add($"protected override GenericController<{oneGenericType.FullName}, {oneEntityType}> OneEntityController");
            result.Add("{");
            result.Add($"get => oneEntityController; // ?? (oneEntityController =  new {oneCtrlType}(this));");
            result.Add($"set => oneEntityController = value as {oneCtrlType};");
            result.Add("}");

            result.Add($"private {anotherCtrlType} anotherEntityController = null;");
            result.Add($"protected override GenericController<{anotherGenericType.FullName}, {anotherEntityType}> AnotherEntityController");
            result.Add("{");
            result.Add($"get => anotherEntityController; // ?? (anotherEntityController =  new {anotherCtrlType}(this));");
            result.Add($"set => anotherEntityController = value as {anotherCtrlType};");
            result.Add("}");

            result.Add("}");
            result.EnvelopeWithANamespace(CreateLogicControllerNameSpace(type));
            result.FormatCSharpCode();
            return result;
        }
        private Contracts.IGeneratedItem CreateOneToManyBusinessController(Type type)
        {
            type.CheckArgument(nameof(type));

            var entityName = CreateEntityNameFromInterface(type);
            var subNameSpace = CreateSubNamespaceFromType(type);
            var entityType = $"{StaticLiterals.EntitiesFolder}.{subNameSpace}.{entityName}";
            var controllerName = $"{entityName}Controller";
            var baseControllerName = "GenericOneToManyController";
            var interfaceTypes = type.GetInterfaces();
            var oneGenericType = interfaceTypes[0].GetGenericArguments()[0];
            var manyGenericType = interfaceTypes[0].GetGenericArguments()[1];
            var oneEntityType = $"{CreateEntityFullNameFromInterface(oneGenericType)}";
            var manyEntityType = $"{CreateEntityFullNameFromInterface(manyGenericType)}";
            var oneCtrlType = $"{CreateLogicControllerFullNameFromInterface(oneGenericType)}";
            var manyCtrlType = $"{CreateLogicControllerFullNameFromInterface(manyGenericType)}";
            var result = new Models.GeneratedItem(Common.UnitType.Logic, Common.ItemType.BusinessController)
            {
                FullName = CreateLogicControllerFullNameFromInterface(type),
                FileExtension = StaticLiterals.CSharpFileExtension,
            };
            result.SubFilePath = $"{result.FullName}{result.FileExtension}";
            ConvertGenericPersistenceControllerName(type, ref baseControllerName);
            CreateLogicControllerAttributes(type, result.Source);
            result.Add($"sealed partial class {controllerName} : {baseControllerName}<{type.FullName}, {entityType}, {oneGenericType.FullName}, {oneEntityType}, {manyGenericType.FullName}, {manyEntityType}>");
            result.Add("{");

            var initStatements = new List<string>()
            {
                $"oneEntityController = new {oneCtrlType}(this);",
                $"manyEntityController = new {manyCtrlType}(this);",
            };

            result.AddRange(CreatePartialStaticConstrutor(controllerName));
            result.AddRange(CreatePartialConstrutor("public", controllerName, $"{SolutionProperties.DataContextFolder}.IContext context", "base(context)", initStatements));
            result.AddRange(CreatePartialConstrutor("public", controllerName, "ControllerObject controller", "base(controller)", initStatements, false));

            result.Add($"private {oneCtrlType} oneEntityController = null;");
            result.Add($"protected override GenericController<{oneGenericType.FullName}, {oneEntityType}> OneEntityController");
            result.Add("{");
            result.Add($"get => oneEntityController; // ?? (oneEntityController = new {oneCtrlType}(this));");
            result.Add($"set => oneEntityController = value as {oneCtrlType};");
            result.Add("}");

            result.Add($"private {manyCtrlType} manyEntityController = null;");
            result.Add($"protected override GenericController<{manyGenericType.FullName}, {manyEntityType}> ManyEntityController");
            result.Add("{");
            result.Add($"get => manyEntityController; // ?? (manyEntityController = new {manyCtrlType}(this));");
            result.Add($"set => manyEntityController = value as {manyCtrlType};");
            result.Add("}");

            result.Add("}");
            result.EnvelopeWithANamespace(CreateLogicControllerNameSpace(type));
            result.FormatCSharpCode();
            return result;
        }

        public IEnumerable<Contracts.IGeneratedItem> CreatePersistenceControllers()
        {
            var result = new List<Contracts.IGeneratedItem>();
            var contractsProject = ContractsProject.Create(SolutionProperties);

            foreach (var type in contractsProject.PersistenceTypes)
            {
                if (CanCreate(nameof(CreatePersistenceControllers), type))
                {
                    result.Add(CreatePersistenceController(type));
                }
            }
            return result;
        }
        private Contracts.IGeneratedItem CreatePersistenceController(Type type)
        {
            type.CheckArgument(nameof(type));

            var entityName = CreateEntityNameFromInterface(type);
            var subNameSpace = CreateSubNamespaceFromType(type);
            var entityType = $"{StaticLiterals.EntitiesFolder}.{subNameSpace}.{entityName}";
            var controllerName = $"{entityName}Controller";
            var baseControllerName = "GenericPersistenceController";
            var controllerAttributes = InitLogicControllerAttributes(type);
            var result = new Models.GeneratedItem(Common.UnitType.Logic, Common.ItemType.PersistenceController)
            {
                FullName = CreateLogicControllerFullNameFromInterface(type),
                FileExtension = StaticLiterals.CSharpFileExtension,
            };
            result.SubFilePath = $"{result.FullName}{result.FileExtension}";
            ConvertGenericPersistenceControllerName(type, ref baseControllerName);
            CreateLogicControllerAttributes(type, controllerAttributes);
            result.AddRange(controllerAttributes);
            result.Add($"sealed partial class {controllerName} : {baseControllerName}<{type.FullName}, {entityType}>");
            result.Add("{");

            result.AddRange(CreatePartialStaticConstrutor(controllerName));
            result.AddRange(CreatePartialConstrutor("internal", controllerName, $"{SolutionProperties.DataContextFolder}.IContext context", "base(context)"));
            result.AddRange(CreatePartialConstrutor("internal", controllerName, "ControllerObject controller", "base(controller)", null, false));
            result.Add("}");
            result.EnvelopeWithANamespace(CreateLogicControllerNameSpace(type));
            result.FormatCSharpCode();
            return result;
        }

        public IEnumerable<Contracts.IGeneratedItem> CreateShadowControllers()
        {
            var result = new List<Contracts.IGeneratedItem>();
            var contractsProject = ContractsProject.Create(SolutionProperties);

            foreach (var type in contractsProject.ShadowTypes)
            {
                if (CanCreate(nameof(CreateShadowControllers), type))
                {
                    result.Add(CreateShadowController(type));
                }
            }
            return result;
        }
        private Contracts.IGeneratedItem CreateShadowController(Type type)
        {
            type.CheckArgument(nameof(type));

            var entityName = CreateEntityNameFromInterface(type);
            var subNameSpace = CreateSubNamespaceFromType(type);
            var entityType = $"{StaticLiterals.EntitiesFolder}.{subNameSpace}.{entityName}";
            var controllerName = $"{entityName}Controller";
            var baseControllerName = "GenericShadowController";
            var interfaceTypes = type.GetInterfaces();
            var sourceGenericType = interfaceTypes.Single(e => e.IsGenericType && e.Name.Equals(StaticLiterals.IShadowName)).GetGenericArguments()[0];
            var sourceEntityType = $"{CreateEntityFullNameFromInterface(sourceGenericType)}";
            var sourceCtrlType = $"{CreateLogicControllerFullNameFromInterface(sourceGenericType)}";
            var result = new Models.GeneratedItem(Common.UnitType.Logic, Common.ItemType.ShadowController)
            {
                FullName = CreateLogicControllerFullNameFromInterface(type),
                FileExtension = StaticLiterals.CSharpFileExtension,
            };
            result.SubFilePath = $"{result.FullName}{result.FileExtension}";
            ConvertGenericPersistenceControllerName(type, ref baseControllerName);
            CreateLogicControllerAttributes(type, result.Source);
            result.Add($"sealed partial class {controllerName} : {baseControllerName}<{type.FullName}, {entityType}, {sourceGenericType.FullName}, {sourceEntityType}>");
            result.Add("{");

            var initStatements = new List<string>()
            {
                $"sourceEntityController = new {sourceCtrlType}(this);",
            };

            result.AddRange(CreatePartialStaticConstrutor(controllerName));
            result.AddRange(CreatePartialConstrutor("public", controllerName, $"{SolutionProperties.DataContextFolder}.IContext context", "base(context)", initStatements));
            result.AddRange(CreatePartialConstrutor("public", controllerName, "ControllerObject controller", "base(controller)", initStatements, false));

            result.Add($"private {sourceCtrlType} sourceEntityController = null;");
            result.Add($"protected override GenericController<{sourceGenericType.FullName}, {sourceEntityType}> SourceEntityController");
            result.Add("{");
            result.Add($"get => sourceEntityController; // ?? (sourceEntityController =  new {sourceCtrlType}(this));");
            result.Add($"set => sourceEntityController = value as {sourceCtrlType};");
            result.Add("}");

            result.Add("}");
            result.EnvelopeWithANamespace(CreateLogicControllerNameSpace(type));
            result.FormatCSharpCode();
            return result;
        }
        #endregion LogicController

        #region WebApiController
        public string WebApiNameSpace => $"{SolutionProperties.WebApiProjectName}.{StaticLiterals.ControllersFolder}";
        public string CreateWebApiControllerNameSpace(Type type)
        {
            type.CheckArgument(nameof(type));

            return $"{WebApiNameSpace}.{CreateSubNamespaceFromType(type)}";
        }
        static partial void CreateWebApiControllerAttributes(Type type, List<string> codeLines);
        static partial void CreateWebApiActionAttributes(Type type, string action, List<string> codeLines);

        public IEnumerable<Contracts.IGeneratedItem> CreateWebApiControllers()
        {
            var result = new List<Contracts.IGeneratedItem>();
            var contractsProject = ContractsProject.Create(SolutionProperties);
            var types = contractsProject.PersistenceTypes
                                        .Union(contractsProject.ShadowTypes)
                                        .Union(contractsProject.BusinessTypes);

            foreach (var type in types)
            {
                if (CanCreate(nameof(CreateWebApiControllers), type))
                {
                    result.Add(CreateWebApiController(type));
                }
            }
            return result;
        }
        private Contracts.IGeneratedItem CreateWebApiController(Type type)
        {
            //var routeBase = $"/api/[controller]";
            var entityName = CreateEntityNameFromInterface(type);
            var subNameSpace = CreateSubNamespaceFromType(type);
            var contractType = $"Contracts.{subNameSpace}.{type.Name}";
            var modelType = $"Transfer.{subNameSpace}.{entityName}";
            var controllerName = entityName.EndsWith("s") ? entityName : $"{entityName}s";
            var result = new Models.GeneratedItem(Common.UnitType.WebApi, Common.ItemType.WebApiController)
            {
                FullName = CreateWebApiControllerFullNameFromInterface(type),
                FileExtension = StaticLiterals.CSharpFileExtension,
            };
            result.SubFilePath = $"{result.FullName}{result.FileExtension}";
            ConvertWebApiControllerName(type, ref controllerName);
            result.Add("using Microsoft.AspNetCore.Mvc;");
            result.Add($"using TContract = {contractType};");
            result.Add($"using TModel = {modelType};");

            result.Add("[ApiController]");
            result.Add("[Route(\"Controller\")]");
            CreateWebApiControllerAttributes(type, result.Source);
            result.Add($"public partial class {controllerName}Controller : GenericController<TContract, TModel>");
            result.Add("{");

            result.Add("}");
            result.EnvelopeWithANamespace(CreateWebApiControllerNameSpace(type));
            result.FormatCSharpCode();
            return result;
        }
        static partial void ConvertWebApiControllerName(Type type, ref string name);
        static partial void ConvertGenericPersistenceControllerName(Type type, ref string name);
        #endregion WebApiController
    }
}
//MdEnd
