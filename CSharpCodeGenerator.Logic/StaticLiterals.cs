//@QnSCodeCopy
//MdStart

using System.Collections.Generic;
using CommonStaticLiterals = CommonBase.StaticLiterals;

namespace CSharpCodeGenerator.Logic
{
    public static partial class StaticLiterals
    {
        public static string ContractsExtension => ".Contracts";
        public static string SourceFileExtensions => CommonStaticLiterals.QnSSourceFileExtensions;
        public static string CSharpFileExtension => CommonStaticLiterals.QnSCSharpFileExtension;
        public static string GeneratedCodeLabel => CommonStaticLiterals.QnSGeneratedCodeLabel;
        public static string CustomizedAndGeneratedCodeLabel => CommonStaticLiterals.QnSCustomizedAndGeneratedCodeLabel;

        public static IDictionary<string, string> SourceFileHeaders => CommonStaticLiterals.QnSSourceFileHeaders;
        public static string AngularCustomImportBeginLabel => "/** CustomImportBegin **/";
        public static string AngularCustomImportEndLabel => "/** CustomImportEnd **/";
        public static string AngularCustomCodeBeginLabel => "/** CustomCodeBegin **/";
        public static string AngularCustomCodeEndLabel => "/** CustomCodeEnd **/";

        #region Folders
        public static string ContractsFolder => "Contracts";
        public static string EntitiesFolder => "Entities";
        public static string ControllersFolder => "Controllers";
        public static string ModelsFolder => "Models";
        public static string ModulesFolder => "Modules";
        public static string BusinessFolder => "Business";
        public static string PersistenceFolder => "Persistence";
        public static string ShadowFolder => "Shadow";
        #endregion Folders

        public static string RootSubName => ".Contracts.";
        public static string ClientSubName => ".Client.";
        public static string BusinessSubName => ".Business.";
        public static string ModulesSubName => ".Modules.";
        public static string PersistenceSubName => ".Persistence.";
        public static string ShadowSubName => ".Shadow.";

        public static string EntitiesLabel => "Entities";
        public static string ModulesLabel => "Modules";
        public static string BusinessLabel => "Business";
        public static string PersistenceLabel => "Persistence";
        public static string ShadowLabel => "Shadow";

        public static string DelegatePropertyName => "DelegateObject";
        public static string IIdentifiableName => "IIdentifiable";
        public static string IVersionableName => "IVersionable";

        public static string ICopyableName => "ICopyable`1";
        public static string ICompositeName => "IComposite`3";
        public static string IOneToAnotherName => "IOneToAnother`2";
        public static string IOneToManyName => "IOneToMany`2";
        public static string IShadowName => "IShadow`1";

        public static string ConnectorItemName => "ConnectorItem";
        public static string OneItemName => "OneItem";
        public static string OneModelName => "OneModel";
        public static string AnotherItemName => "AnotherItem";
        public static string FirstItemName => "FirstItem";
        public static string SecondItemName => "SecondItem";
        public static string ManyItemName => "ManyItem";
        public static string ManyItemsName => "ManyItems";
    }
}
//MdEnd
