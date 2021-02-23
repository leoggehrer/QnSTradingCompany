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
    internal partial class AngularAppGenerator : ClassGenerator, Contracts.IAngularAppGenerator
    {
        #region AngularApp-Definitions
        public static string CodeExtension => "ts";
        public string ProjectName => $"{SolutionProperties.SolutionName}{SolutionProperties.AngularAppPostfix}";
        public string ProjectContractsPath => Path.Combine(ProjectName, "ClientApp", "src", "app", "app-contracts");
        #endregion AngularApp-Definitions

        public static string SourceNameSpace => "src";
        public static string ContractsNameSpace => $"{SourceNameSpace}.contracts";
        public static string CreateContractsNameSpace(Type type)
        {
            type.CheckArgument(nameof(type));

            return $"{ContractsNameSpace}.{CreateSubNamespaceFromType(type)}".ToLower();
        }
        public static string CreateTypeScriptFullName(Type type)
        {
            type.CheckArgument(nameof(type));

            return $"{CreateContractsNameSpace(type)}.{(type.IsInterface ? CreateEntityNameFromInterface(type) : type.Name)}";
        }

        internal AngularAppGenerator(SolutionProperties solutionProperties)
            : base(solutionProperties)
        {
        }
        public new static AngularAppGenerator Create(SolutionProperties solutionProperties)
        {
            return new AngularAppGenerator(solutionProperties);
        }
        private bool CanCreate(Type type)
        {
            bool create = true;

            CanCreateContract(type, ref create);
            return create;
        }
        partial void CanCreateContract(Type type, ref bool create);

        public IEnumerable<Contracts.IGeneratedItem> CreateEnums()
        {
            var result = new List<Contracts.IGeneratedItem>();
            var contractsProject = ContractsProject.Create(SolutionProperties);

            foreach (var type in contractsProject.EnumTypes)
            {
                if (CanCreate(type))
                {
                    result.Add(CreateEnum(type));
                }
            }
            return result;
        }
        public IEnumerable<Contracts.IGeneratedItem> CreateBusinessContracts()
        {
            var result = new List<Contracts.IGeneratedItem>();
            var contractsProject = ContractsProject.Create(SolutionProperties);
            var types = contractsProject.BusinessTypes;

            foreach (var type in types)
            {
                if (CanCreate(type))
                {
                    result.Add(CreateContract(type, types));
                }
            }
            return result;
        }
        public IEnumerable<Contracts.IGeneratedItem> CreateModulesContracts()
        {
            var result = new List<Contracts.IGeneratedItem>();
            var contractsProject = ContractsProject.Create(SolutionProperties);
            var types = contractsProject.ModuleTypes;

            foreach (var type in types)
            {
                if (CanCreate(type))
                {
                    result.Add(CreateContract(type, types));
                }
            }
            return result;
        }
        public IEnumerable<Contracts.IGeneratedItem> CreatePersistenceContracts()
        {
            var result = new List<Contracts.IGeneratedItem>();
            var contractsProject = ContractsProject.Create(SolutionProperties);
            var types = contractsProject.PersistenceTypes;

            foreach (var type in types)
            {
                if (CanCreate(type))
                {
                    result.Add(CreateContract(type, types));
                }
            }
            return result;
        }
        public IEnumerable<Contracts.IGeneratedItem> CreateShadowContracts()
        {
            var result = new List<Contracts.IGeneratedItem>();
            var contractsProject = ContractsProject.Create(SolutionProperties);
            var types = contractsProject.ShadowTypes;

            foreach (var type in types)
            {
                if (CanCreate(type))
                {
                    result.Add(CreateContract(type, types));
                }
            }
            return result;
        }

        public Contracts.IGeneratedItem CreateEnum(Type type)
        {
            type.CheckArgument(nameof(type));

            var subPath = CreateSubPathFromType(type);
            var fileName = $"{ConvertFileName(type.Name)}.{CodeExtension}";
            var result = new Models.GeneratedItem(Common.UnitType.AngularApp, Common.ItemType.TypeScriptEnum)
            {
                FullName = CreateTypeScriptFullName(type),
                FileExtension = CodeExtension,
            };
            result.SubFilePath = Path.Combine(ProjectContractsPath, subPath, fileName);

            result.Add($"export enum {type.Name}" + " {");

            foreach (var item in Enum.GetNames(type))
            {
                var value = Enum.Parse(type, item);

                result.Add($"{item} = {(int)value},");
            }

            result.Add("}");
            result.AddRange(result.Source.Eject().Distinct());
            FinishCreateContract(type, result.Source);
            result.FormatCSharpCode();
            return result;
        }
        public Contracts.IGeneratedItem CreateContract(Type type, IEnumerable<Type> types)
        {
            type.CheckArgument(nameof(type));

            var subPath = CreateSubPathFromType(type);
            var entityName = CreateEntityNameFromInterface(type);
            var fileName = $"{ConvertFileName(entityName)}.{CodeExtension}";
            var properties = ContractHelper.GetAllProperties(type);
            var declarationTypeName = string.Empty;
            var result = new Models.GeneratedItem(Common.UnitType.AngularApp, Common.ItemType.TypeScriptContract)
            {
                FullName = CreateTypeScriptFullName(type),
                FileExtension = CodeExtension,
            };
            result.SubFilePath = Path.Combine(ProjectContractsPath, subPath, fileName);

            StartCreateContract(type, result.Source);
            result.Add($"export interface {entityName}" + " {");

            foreach (var item in properties)
            {
                if (declarationTypeName.Equals(item.DeclaringType.Name) == false)
                {
                    declarationTypeName = item.DeclaringType.Name;
                    result.Add($"/** {declarationTypeName} **/");
                }

                result.AddRange(CreateTypeScriptProperty(item));
            }
            result.AddRange(CreateContactToContractFromContracts(type, types));
            result.Add("}");
            result.FormatCSharpCode();
            result.Source.Insert(result.Source.Count - 1, StaticLiterals.AngularCustomCodeBeginLabel);
            result.Source.Insert(result.Source.Count - 1, StaticLiterals.AngularCustomCodeEndLabel);

            var imports = new List<string>();

            imports.AddRange(CreateTypeImports(type));
            imports.AddRange(CreateContactToContractImports(type, types));
            imports.Add(StaticLiterals.AngularCustomImportBeginLabel);
            imports.Add(StaticLiterals.AngularCustomImportEndLabel);

            InsertTypeImports(imports, result.Source);

            FinishCreateContract(type, result.Source);
            return result;
        }
        partial void StartCreateContract(Type type, List<string> lines);
        partial void FinishCreateContract(Type type, List<string> lines);

        #region Helpers
        public static string ConvertFileName(string fileName)
        {
            fileName.CheckArgument(nameof(fileName));

            var result = new StringBuilder();

            foreach (var item in fileName)
            {
                if (result.Length == 0)
                {
                    result.Append(Char.ToLower(item));
                }
                else if (Char.IsUpper(item))
                {
                    result.Append('-');
                    result.Append(Char.ToLower(item));
                }
                else
                {
                    result.Append(Char.ToLower(item));
                }
            }
            return result.ToString();
        }
        public static string CreateImport(string typeName, string subPath)
        {
            return "import { " + typeName + " } from " + $"'@app-contracts/{subPath}/{ConvertFileName(typeName)}';";
        }
        public static void InsertTypeImports(IEnumerable<string> imports, List<string> lines)
        {
            lines.CheckArgument(nameof(lines));
            imports.CheckArgument(nameof(imports));

            foreach (var item in imports.Reverse())
            {
                lines.Insert(0, item);
            }
        }

        public static IEnumerable<string> CreateTypeImports(Type type)
        {
            type.CheckArgument(nameof(type));

            var result = new List<string>();
            var properties = ContractHelper.GetAllProperties(type);
            var entityName = CreateEntityNameFromInterface(type);

            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.PropertyType.IsEnum)
                {
                    var typeName = $"{propertyInfo.PropertyType.Name}";

                    if (typeName.Equals(entityName) == false)
                    {
                        var subPath = GeneratorObject.CreateSubPathFromType(propertyInfo.PropertyType).ToLower();

                        result.Add(CreateImport(typeName, subPath));
                    }
                }
                else if (propertyInfo.PropertyType.IsGenericType)
                {
                    Type subType = propertyInfo.PropertyType.GetGenericArguments().First();

                    if (subType.IsInterface)
                    {
                        var typeName = subType.Name[1..];

                        if (typeName.Equals(entityName) == false)
                        {
                            var subPath = GeneratorObject.CreateSubPathFromType(subType).ToLower();

                            result.Add(CreateImport(typeName, subPath));
                        }
                    }
                    else
                    {
                        //result.Add($"{Char.ToLower(propertyInfo.Name[0])}{propertyInfo.Name.Substring(1)}: {subType.Name}[];");
                    }
                }
                else if (propertyInfo.PropertyType.IsInterface)
                {
                    var typeName = CreateEntityNameFromInterface(propertyInfo.PropertyType);
                    if (typeName.Equals(entityName) == false)
                    {
                        var subPath = GeneratorObject.CreateSubPathFromType(propertyInfo.PropertyType).ToLower();

                        result.Add(CreateImport(typeName, subPath));
                    }
                }
            }
            return result.Distinct();
        }
        public static IEnumerable<string> CreateTypeScriptProperty(PropertyInfo propertyInfo)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var result = new List<string>();

            if (propertyInfo.PropertyType.IsEnum)
            {
                var enumName = $"{propertyInfo.PropertyType.Name}";

                result.Add($"{Char.ToLower(propertyInfo.Name[0])}{propertyInfo.Name[1..]}: {enumName};");
            }
            else if (propertyInfo.PropertyType == typeof(DateTime)
                     || propertyInfo.PropertyType == typeof(DateTime?))
            {
                result.Add($"{Char.ToLower(propertyInfo.Name[0])}{propertyInfo.Name[1..]}: Date;");
            }
            else if (propertyInfo.PropertyType == typeof(string))
            {
                result.Add($"{Char.ToLower(propertyInfo.Name[0])}{propertyInfo.Name[1..]}: string;");
            }
            else if (propertyInfo.PropertyType == typeof(bool))
            {
                result.Add($"{Char.ToLower(propertyInfo.Name[0])}{propertyInfo.Name[1..]}: boolean;");
            }
            else if (propertyInfo.PropertyType.IsNumericType())
            {
                result.Add($"{Char.ToLower(propertyInfo.Name[0])}{propertyInfo.Name[1..]}: number;");
            }
            else if (propertyInfo.PropertyType.IsGenericType)
            {
                Type subType = propertyInfo.PropertyType.GetGenericArguments().First();

                if (subType.IsInterface)
                {
                    result.Add($"{Char.ToLower(propertyInfo.Name[0])}{propertyInfo.Name[1..]}: {subType.Name[1..]}[];");
                }
                else if (subType == typeof(Guid))
                {
                    result.Add($"{Char.ToLower(propertyInfo.Name[0])}{propertyInfo.Name[1..]}: string[];");
                }
                else
                {
                    result.Add($"{Char.ToLower(propertyInfo.Name[0])}{propertyInfo.Name[1..]}: {subType.Name}[];");
                }
            }
            else if (propertyInfo.PropertyType.IsInterface)
            {
                result.Add($"{Char.ToLower(propertyInfo.Name[0])}{propertyInfo.Name[1..]}: {propertyInfo.PropertyType.Name[1..]};");
            }
            return result;
        }
        private static IEnumerable<string> CreateContactToContractImports(Type type, IEnumerable<Type> types)
        {
            type.CheckArgument(nameof(type));
            types.CheckArgument(nameof(types));

            var result = new List<string>();
            var typeName = GeneratorObject.CreateEntityNameFromInterface(type);

            foreach (var other in types)
            {
                var otherName = GeneratorObject.CreateEntityNameFromInterface(other);

                foreach (var pi in other.GetProperties())
                {
                    if (pi.Name.Equals($"{typeName}Id"))
                    {
                        var refTypeName = CreateEntityNameFromInterface(other);
                        var subPath = GeneratorObject.CreateSubPathFromType(other).ToLower();

                        result.Add(CreateImport(refTypeName, subPath));
                    }
                }
            }
            foreach (var pi in type.GetProperties())
            {
                foreach (var other in types)
                {
                    var otherName = GeneratorObject.CreateEntityNameFromInterface(other);

                    if (pi.Name.Equals($"{otherName}Id"))
                    {
                        var refTypeName = CreateEntityNameFromInterface(other);
                        var subPath = GeneratorObject.CreateSubPathFromType(other).ToLower();

                        result.Add(CreateImport(refTypeName, subPath));
                    }
                    else if (pi.Name.StartsWith($"{otherName}Id_"))
                    {
                        var data = pi.Name.Split("_");

                        if (data.Length == 2)
                        {
                            var refTypeName = CreateEntityNameFromInterface(other);
                            var subPath = GeneratorObject.CreateSubPathFromType(other).ToLower();

                            result.Add(CreateImport(refTypeName, subPath));
                        }
                    }
                }
            }
            return result.Distinct();
        }
        private static IEnumerable<string> CreateContactToContractFromContracts(Type type, IEnumerable<Type> types)
        {
            type.CheckArgument(nameof(type));
            types.CheckArgument(nameof(types));

            var result = new List<string>();
            var typeName = GeneratorObject.CreateEntityNameFromInterface(type);

            foreach (var other in types)
            {
                var otherName = GeneratorObject.CreateEntityNameFromInterface(other);

                foreach (var pi in other.GetProperties())
                {
                    if (pi.Name.Equals($"{typeName}Id"))
                    {
                        var name = $"{other.Name[1..]}s";

                        result.Add($"{char.ToLower(name[0])}{name[1..]}: {other.Name[1..]}[];");
                    }
                }
            }
            foreach (var pi in type.GetProperties())
            {
                foreach (var other in types)
                {
                    var otherName = GeneratorObject.CreateEntityNameFromInterface(other);

                    if (pi.Name.Equals($"{otherName}Id"))
                    {
                        var name = $"{other.Name[1..]}";

                        result.Add($"{char.ToLower(name[0])}{name[1..]}: {other.Name[1..]};");
                    }
                    else if (pi.Name.StartsWith($"{otherName}Id_"))
                    {
                        var data = pi.Name.Split("_");

                        if (data.Length == 2)
                        {
                            var name = $"{other.Name[1..]}_{data[1]}";

                            result.Add($"{char.ToLower(name[0])}{name[1..]}: {other.Name[1..]};");
                        }
                    }
                }
            }
            return result;
        }
        #endregion Helpers
    }
}
//MdEnd
