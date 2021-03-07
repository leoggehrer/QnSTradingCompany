//@QnSCodeCopy
//MdStart
using System;
using System.IO;
using System.Linq;
using CommonStaticLiterals = CommonBase.StaticLiterals;

namespace CSharpCodeGenerator.ConApp
{
	internal class Program
    {
        private static string GetCurrentSolutionPath()
        {
            int endPos = AppContext.BaseDirectory
                                   .IndexOf($"{nameof(CSharpCodeGenerator)}", StringComparison.CurrentCultureIgnoreCase);

            return AppContext.BaseDirectory.Substring(0, endPos);
        }

        private static string GetCurrentSolutionName()
        {
            var solutionPath = GetCurrentSolutionPath();

            return GetSolutionNameByPath(solutionPath);
        }
        private static string GetCurrentContractsFilePath()
        {
            return GetContractsFilePath(GetCurrentSolutionPath());
        }
        private static string GetSolutionNameByPath(string solutionPath)
        {
            return solutionPath.Split(new char[] { '\\', '/' })
                               .Where(e => string.IsNullOrEmpty(e) == false)
                               .Last();
        }
        private static string GetSolutionNameByFile(string solutionPath)
        {
            var fileInfo = new DirectoryInfo(solutionPath).GetFiles()
                                                          .SingleOrDefault(f => f.Extension.Equals(CommonStaticLiterals.SolutionFileExtension, StringComparison.CurrentCultureIgnoreCase));

            return fileInfo != null ? Path.GetFileNameWithoutExtension(fileInfo.Name) : string.Empty;
        }
        private static string GetContractsFilePath(string solutionPath)
        {
            var result = default(string);
            var solutionName = GetSolutionNameByPath(solutionPath);
            var projectName = $"{solutionName}{Logic.StaticLiterals.ContractsExtension}";
            var binPath = Path.Combine(solutionPath, projectName, "bin");

            if (Directory.Exists(binPath))
            {
                var fileName = $"{projectName}.dll";
                var fileInfos = new DirectoryInfo(binPath).GetFiles(fileName, SearchOption.AllDirectories)
                                                          .Where(f => f.FullName.EndsWith(fileName))
                                                          .OrderByDescending(f => f.LastWriteTime);

                var fileInfo = fileInfos.Where(f => f.FullName.ToLower().Contains("\\ref\\") == false)
                                        .FirstOrDefault();

                if (fileInfo != null)
                {
                    result = fileInfo.FullName;
                }
            }
            return result;
        }

        private static void Main(/*string[] args*/)
        {
            var solutionPath = GetCurrentSolutionPath();
            var solutionName = GetSolutionNameByPath(solutionPath);
            var contractsFilePath = GetContractsFilePath(solutionPath);
            var solutionProperties = Logic.Factory.GetSolutionProperties(solutionName, contractsFilePath);
            var appGenerationUnits = Logic.Common.UnitType.AllApps;

            //Logic.Generator.DeleteGeneratedCodeFiles(solutionPath);
            //return;
            var generatedItems = Logic.Generator.Generate(solutionName, contractsFilePath, appGenerationUnits);

            Writer.WriteAll(solutionPath, solutionProperties, generatedItems);

            Console.WriteLine("Excluding Files from Git...");
            Logic.Git.GitIgnoreManager.Run($"{nameof(CSharpCodeGenerator)}.{nameof(ConApp)}");
        }


    }
}
//MdEnd
