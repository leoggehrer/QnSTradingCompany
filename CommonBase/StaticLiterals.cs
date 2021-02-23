//@QnSCodeCopy
//MdStart

using System.Collections.Generic;

namespace CommonBase
{
    public static partial class StaticLiterals
    {
        static StaticLiterals()
        {
            QnSSourceFileHeaders = new Dictionary<string, string>()
            {
                {".css", $"/*{QnSGeneratedCodeLabel}*/" },
                {".cs", $"//{QnSGeneratedCodeLabel}" },
                {".ts", $"//{QnSGeneratedCodeLabel}" },
                {".cshtml", $"@*{QnSGeneratedCodeLabel}*@" },
                {".razor", $"@*{QnSGeneratedCodeLabel}*@" },
                {".razor.cs", $"//{QnSGeneratedCodeLabel}" },
            };
            QnSCommonProjects = new[]
            {
                "CommonBase",
                "ConfigurationImporter.ConApp",
            };
            QnSGeneratorProjects = new[]
            {
                "CSharpCodeGenerator.Logic",
                "CSharpCodeGenerator.ConApp"
            };
            QnSToolProjects = new[]
            {
                "SolutionCodeComparsion.ConApp",
                "SolutionCopier.ConApp",
                "SolutionDeveloperHelper.ConApp",
                "SolutionGeneratedCodeDeleter.ConApp",
            };
            QnSProjectExtensions = new[]
            {
                ".Contracts",
                ".Logic",
                ".Transfer",
                ".WebApi",
                ".Adapters",
                ".AspMvc",
                ".AngularApp",
                ".BlazorApp",
                ".ConApp"
            };
        }
        #region QnSTradingCompanyLiterals
        public static string QnSGeneratedCodeLabel => "@QnSGeneratedCode";
        public static string QnSCustomizedAndGeneratedCodeLabel => "@QnSCustomAndGeneratedCode";
        public static string QnSSourceFileExtensions => "*.css|*.cs|*.ts|*.cshtml|*.razor|*.razor.cs|*.template";
        public static string QnSCSharpFileExtension => ".cs";
        public static IDictionary<string, string> QnSSourceFileHeaders { get; }
        public static string QnSIgnoreLabel => "@QnSIgnore";
        public static string QnSBaseCodeLabel => "@QnSBaseCode";
        public static string QnSCodeCopyLabel => "@QnSCodeCopy";

        public static string[] QnSCommonProjects { get; }
        public static string[] QnSGeneratorProjects { get; }
        public static string[] QnSToolProjects { get; }
        public static string[] QnSProjectExtensions { get; }

        public static string SolutionFileExtension => ".sln";
        public static string ProjectFileExtension => ".csproj";
        #endregion QnSTradingCompanyLiterals

        public static string ConnectionString => "ConnectionStrings:DefaultConnection";
    }
}
//MdEnd
