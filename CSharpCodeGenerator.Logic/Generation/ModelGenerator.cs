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
    internal abstract partial class ModelGenerator : ClassGenerator, Contracts.IModelGenerator
    {
        #region Models
        internal static string ModelObject => nameof(ModelObject);
        internal static string ModuleModel => nameof(ModuleModel);
        internal static string IdentityModel => nameof(IdentityModel);
        internal static string VersionModel => nameof(VersionModel);

        internal static string CompositeModel => nameof(CompositeModel);
        internal static string OneToAnotherModel => nameof(OneToAnotherModel);
        internal static string OneToManyModel => nameof(OneToManyModel);
        internal static string ShadowModel => nameof(ShadowModel);
        #endregion Models

        protected ModelGenerator(SolutionProperties solutionProperties)
            : base(solutionProperties)
        {
        }

        public abstract Common.UnitType UnitType { get; }
        public abstract string AppPostfix { get; }
        public abstract string AppModelsNameSpace { get; }
        public abstract string ModelsFolder { get; }

        public string CreateModelsNameSpace(Type type)
        {
            type.CheckArgument(nameof(type));

            return $"{AppModelsNameSpace}.{GeneratorObject.CreateSubNamespaceFromType(type)}";
        }
        protected virtual bool CanCreate(Type type)
        {
            bool create = true;

            CanCreateModel(type, ref create);
            return create;
        }
        partial void CanCreateModel(Type type, ref bool create);
        partial void CreateModelAttributes(Type type, List<string> codeLines);
        protected virtual void CreateModelPropertyAttributes(Type type, PropertyInfo propertyInfo, List<string> codeLines)
        {
            var handled = false;

            BeforeCreateModelPropertyAttributes(type, propertyInfo, codeLines, ref handled);
            if (handled == false)
            {
            }
            AfterCreateModelPropertyAttributes(type, propertyInfo, codeLines);
        }
        partial void BeforeCreateModelPropertyAttributes(Type type, PropertyInfo propertyInfo, List<string> codeLines, ref bool handled);
        partial void AfterCreateModelPropertyAttributes(Type type, PropertyInfo propertyInfo, List<string> codeLines);

        public IEnumerable<Contracts.IGeneratedItem> CreateBusinessModels()
        {
            var result = new List<Contracts.IGeneratedItem>();
            var contractsProject = ContractsProject.Create(SolutionProperties);

            foreach (var type in contractsProject.BusinessTypes)
            {
                if (CanCreate(type))
                {
                    result.Add(CreateModelFromContract(type, UnitType, Common.ItemType.BusinessModel));
                    if (ContractHelper.HasOneToMany(type))
                    {
                        var (one, _) = ContractHelper.GetOneToManyTypes(type);

                        result.Add(CreateDelegateProperties(type, one, StaticLiterals.OneModelName, UnitType, Common.ItemType.BusinessModel));
                    }
                    result.Add(CreateBusinessModel(type, UnitType));
                }
            }
            return result;
        }
        private Contracts.IGeneratedItem CreateBusinessModel(Type type, Common.UnitType unitType)
        {
            type.CheckArgument(nameof(type));

            var result = new Models.GeneratedItem(unitType, Common.ItemType.BusinessModel)
            {
                FullName = CreateModelFullNameFromInterface(type),
                FileExtension = StaticLiterals.CSharpFileExtension,
            };
            result.SubFilePath = $"{result.FullName}PartA{result.FileExtension}";
            result.Source.Add($"partial class {CreateModelNameFromInterface(type)} : {GetBaseClassByContract(type)}");
            result.Source.Add("{");
            result.Source.Add("}");
            result.EnvelopeWithANamespace(CreateModelsNameSpace(type));
            result.FormatCSharpCode();
            return result;
        }

        public IEnumerable<Contracts.IGeneratedItem> CreateModulesModels()
        {
            var result = new List<Contracts.IGeneratedItem>();
            var contractsProject = ContractsProject.Create(SolutionProperties);

            foreach (var type in contractsProject.ModuleTypes)
            {
                if (CanCreate(type))
                {
                    result.Add(CreateModelFromContract(type, UnitType, Common.ItemType.ModuleModel));
                    result.Add(CreateModuleModel(type, UnitType));
                }
            }
            return result;
        }
        private Contracts.IGeneratedItem CreateModuleModel(Type type, Common.UnitType unitType)
        {
            type.CheckArgument(nameof(type));

            var result = new Models.GeneratedItem(unitType, Common.ItemType.ModuleModel)
            {
                FullName = CreateModelFullNameFromInterface(type),
                FileExtension = StaticLiterals.CSharpFileExtension,
            };
            result.SubFilePath = $"{result.FullName}PartA{result.FileExtension}";
            result.Source.Add($"partial class {CreateModelNameFromInterface(type)} : {GetBaseClassByContract(type)}");
            result.Source.Add("{");
            result.Source.Add("}");
            result.EnvelopeWithANamespace(CreateModelsNameSpace(type));
            result.FormatCSharpCode();
            return result;
        }

        public IEnumerable<Contracts.IGeneratedItem> CreatePersistenceModels()
        {
            var result = new List<Contracts.IGeneratedItem>();
            var contractsProject = ContractsProject.Create(SolutionProperties);

            foreach (var type in contractsProject.PersistenceTypes)
            {
                if (CanCreate(type))
                {
                    result.Add(CreateModelFromContract(type, UnitType, Common.ItemType.PersistenceModel));
                    result.Add(CreatePersistenceModel(type, UnitType));
                }
            }
            return result;
        }
        private Contracts.IGeneratedItem CreatePersistenceModel(Type type, Common.UnitType unitType)
        {
            type.CheckArgument(nameof(type));

            var result = new Models.GeneratedItem(unitType, Common.ItemType.PersistenceModel)
            {
                FullName = CreateModelFullNameFromInterface(type),
                FileExtension = StaticLiterals.CSharpFileExtension,
            };
            result.SubFilePath = $"{result.FullName}PartA{result.FileExtension}";
            result.Source.Add($"partial class {CreateModelNameFromInterface(type)} : {GetBaseClassByContract(type)}");
            result.Source.Add("{");
            result.Source.Add("}");
            result.EnvelopeWithANamespace(CreateModelsNameSpace(type));
            result.FormatCSharpCode();
            return result;
        }

        public IEnumerable<Contracts.IGeneratedItem> CreateShadowModels()
        {
            var result = new List<Contracts.IGeneratedItem>();
            var contractsProject = ContractsProject.Create(SolutionProperties);

            foreach (var type in contractsProject.ShadowTypes)
            {
                if (CanCreate(type))
                {
                    result.Add(CreateModelFromContract(type, UnitType, Common.ItemType.ShadowModel));
                    result.Add(CreateShadowModel(type, UnitType));
                }
            }
            return result;
        }
        private Contracts.IGeneratedItem CreateShadowModel(Type type, Common.UnitType unitType)
        {
            type.CheckArgument(nameof(type));

            var result = new Models.GeneratedItem(unitType, Common.ItemType.ShadowModel)
            {
                FullName = CreateModelFullNameFromInterface(type),
                FileExtension = StaticLiterals.CSharpFileExtension,
            };
            result.SubFilePath = $"{result.FullName}PartA{result.FileExtension}";
            result.Source.Add($"partial class {CreateModelNameFromInterface(type)} : {GetBaseClassByContract(type)}");
            result.Source.Add("{");
            result.Source.Add("}");
            result.EnvelopeWithANamespace(CreateModelsNameSpace(type));
            result.FormatCSharpCode();
            return result;
        }

        private Contracts.IGeneratedItem CreateModelFromContract(Type type, Common.UnitType unitType, Common.ItemType itemType)
        {
            type.CheckArgument(nameof(type));

            var modelName = CreateModelNameFromInterface(type);
            var typeProperties = ContractHelper.GetAllProperties(type);
            var interfaces = GetInterfaces(type);
            var result = new Models.GeneratedItem(unitType, itemType)
            {
                FullName = CreateModelFullNameFromInterface(type),
                FileExtension = StaticLiterals.CSharpFileExtension,
            };
            result.SubFilePath = $"{result.FullName}{result.FileExtension}";
            CreateModelAttributes(type, result.Source);
            result.Add($"public partial class {modelName} : {type.FullName}");
            result.Add("{");
            result.AddRange(CreatePartialStaticConstrutor(modelName));
            result.AddRange(CreatePartialConstrutor("public", modelName));
            foreach (var item in ContractHelper.FilterPropertiesForGeneration(typeProperties))
            {
                CreateModelPropertyAttributes(type, item, result.Source);
                result.AddRange(CreateProperty(item));
            }
            result.AddRange(CreateCopyProperties(type));
            foreach (var item in interfaces.Where(e => ContractHelper.HasCopyable(e)))
            {
                result.AddRange(CreateCopyProperties(item));
            }
            result.AddRange(CreateFactoryMethods(type, false));
            result.Add("}");
            result.EnvelopeWithANamespace(CreateModelsNameSpace(type), "using System;");
            result.FormatCSharpCode();
            return result;
        }

        private Contracts.IGeneratedItem CreateDelegateProperties(Type type, Type delegateType, string delegateObjectName, Common.UnitType unitType, Common.ItemType itemType)
        {
            type.CheckArgument(nameof(type));
            delegateType.CheckArgument(nameof(delegateType));

            var modelName = CreateModelNameFromInterface(type);
            var typeProperties = ContractHelper.GetAllProperties(delegateType);
            var result = new Models.GeneratedItem(unitType, itemType)
            {
                FullName = CreateModelFullNameFromInterface(type),
                FileExtension = StaticLiterals.CSharpFileExtension,
            };
            result.SubFilePath = $"{result.FullName}{result.FileExtension}";
            CreateModelAttributes(type, result.Source);
            result.Add($"public partial class {modelName}");
            result.Add("{");
            foreach (var item in ContractHelper.FilterPropertiesForGeneration(typeProperties))
            {
                var propertyType = GetPropertyType(item);

                if (item.CanRead || item.CanWrite)
                {
                    CreateModelPropertyAttributes(delegateType, item, result.Source);
                    result.Add($"public {propertyType} {item.Name}");
                    result.Add("{");
                    if (item.CanRead)
                    {
                        result.Add($"get => {delegateObjectName}.{item.Name};");
                    }
                    if (item.CanWrite)
                    {
                        result.Add($"set => {delegateObjectName}.{item.Name} = value;");
                    }
                    result.Add("}");
                }
            }
            result.Add("}");
            result.EnvelopeWithANamespace(CreateModelsNameSpace(type), "using System;");
            result.FormatCSharpCode();
            return result;
        }

        private string GetBaseClassByContract(Type type)
        {
            type.CheckArgument(nameof(type));

            var result = string.Empty;
            var typeHelper = new ContractHelper(type);

            if (type.FullName.Contains(StaticLiterals.BusinessSubName))
            {
                result = IdentityModel;
                var itfcs = type.GetInterfaces();

                if (itfcs.Length > 0 && itfcs[0].Name.Equals(StaticLiterals.ICompositeName))
                {
                    var genericArgs = itfcs[0].GetGenericArguments();

                    if (genericArgs.Length == 3)
                    {
                        var connectorModel = $"{CreateModelFullNameFromInterface(genericArgs[0])}";
                        var oneModel = $"{CreateModelFullNameFromInterface(genericArgs[1])}";
                        var anotherModel = $"{CreateModelFullNameFromInterface(genericArgs[2])}";

                        result = $"{CompositeModel}<{genericArgs[0].FullName}, {connectorModel}, {genericArgs[1].FullName}, {oneModel}, {genericArgs[2].FullName}, {anotherModel}>";
                    }
                }
                else if (itfcs.Length > 0 && itfcs[0].Name.Equals(StaticLiterals.IOneToAnotherName))
                {
                    var genericArgs = itfcs[0].GetGenericArguments();

                    if (genericArgs.Length == 2)
                    {
                        var oneModel = $"{CreateModelFullNameFromInterface(genericArgs[0])}";
                        var anotherModel = $"{CreateModelFullNameFromInterface(genericArgs[1])}";

                        result = $"{OneToAnotherModel}<{genericArgs[0].FullName}, {oneModel}, {genericArgs[1].FullName}, {anotherModel}>";
                    }
                }
                else if (itfcs.Length > 0 && itfcs[0].Name.Equals(StaticLiterals.IOneToManyName))
                {
                    var genericArgs = itfcs[0].GetGenericArguments();

                    if (genericArgs.Length == 2)
                    {
                        var firstModel = $"{CreateModelFullNameFromInterface(genericArgs[0])}";
                        var secondModel = $"{CreateModelFullNameFromInterface(genericArgs[1])}";

                        result = $"{OneToManyModel}<{genericArgs[0].FullName}, {firstModel}, {genericArgs[1].FullName}, {secondModel}>";
                    }
                }
            }
            else if (type.FullName.Contains(StaticLiterals.PersistenceSubName))
            {
                if (typeHelper.IsVersionable)
                {
                    result = VersionModel;
                }
                else if (typeHelper.IsIdentifiable)
                {
                    result = IdentityModel;
                }
                else
                {
                    result = ModelObject;
                }
            }
            else if (type.FullName.Contains(StaticLiterals.ShadowSubName))
            {
                result = ShadowModel;
            }
            else if (typeHelper.IsVersionable)
            {
                result = VersionModel;
            }
            else if (typeHelper.IsIdentifiable)
            {
                result = IdentityModel;
            }
            else if (type.FullName.Contains(StaticLiterals.ModulesSubName))
            {
                result = ModuleModel;
            }

            return result;
        }
        private string CreateModelFullNameFromInterface(Type type)
        {
            CheckInterfaceType(type);

            var modelName = CreateModelNameFromInterface(type);
            var subNamespace = $"{AppPostfix}.{(ModelsFolder.HasContent() ? $"{ModelsFolder}." : string.Empty)}";
            var result = type.FullName.Replace(type.Name, modelName);

            return result.Replace(".Contracts.", subNamespace);
        }
    }
}
//MdEnd
