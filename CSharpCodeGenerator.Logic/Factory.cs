//@QnSCodeCopy
//MdStart
using CSharpCodeGenerator.Logic.Generation;

namespace CSharpCodeGenerator.Logic
{
    public class Factory
    {
        public static Contracts.ISolutionProperties GetSolutionProperties(string solutionName, string contractsFilePath)
        {
            return SolutionProperties.Create(solutionName, contractsFilePath);
        }

        public static Contracts.IConfigurationGenerator GetConfigurationGenerator(string solutionName, string contractsFilePath)
        {
            return ConfigurationGenerator.Create(SolutionProperties.Create(solutionName, contractsFilePath));
        }

        public static Contracts.IEntityGenerator GetEntityGenerator(string solutionName, string contractsFilePath)
        {
            return EntityGenerator.Create(SolutionProperties.Create(solutionName, contractsFilePath));
        }
        public static Contracts.IEntityGenerator GetEntityGenerator(string solutionPath)
        {
            return EntityGenerator.Create(SolutionProperties.Create(solutionPath));
        }

        public static Contracts.IDataContextGenerator GetDataContextGenerator(string solutionName, string contractsFilePath)
        {
            return DataContextGenerator.Create(SolutionProperties.Create(solutionName, contractsFilePath));
        }

        public static Contracts.IControllerGenerator GetControllerGenerator(string solutionName, string contractsFilePath)
        {
            return ControllerGenerator.Create(SolutionProperties.Create(solutionName, contractsFilePath));
        }

        public static Contracts.IModelGenerator GetTransferGenerator(string solutionName, string contractsFilePath)
        {
            return TransferGenerator.Create(SolutionProperties.Create(solutionName, contractsFilePath));
        }

        public static Contracts.IModelGenerator GetAspMvcGenerator(string solutionName, string contractsFilePath)
        {
            return AspMvcAppGenerator.Create(SolutionProperties.Create(solutionName, contractsFilePath));
        }

        public static Contracts.IBlazorAppGenerator GetBlazorAppGenerator(string solutionName, string contractsFilePath)
        {
            return BlazorAppGenerator.Create(SolutionProperties.Create(solutionName, contractsFilePath));
        }

        public static Contracts.IAngularAppGenerator GetAngularAppGenerator(string solutionName, string contractsFilePath)
        {
            return AngularAppGenerator.Create(SolutionProperties.Create(solutionName, contractsFilePath));
        }

        public static Contracts.IFactoryGenerator GetFactoryGenerator(string solutionName, string contractsFilePath)
        {
            return FactoryGenerator.Create(SolutionProperties.Create(solutionName, contractsFilePath));
        }
    }
}
//MdEnd
