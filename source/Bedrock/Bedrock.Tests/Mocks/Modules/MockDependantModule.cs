using Bedrock.Modularity;

namespace Bedrock.Tests.Mocks.Modules
{
    [Module(ModuleName = "DependantModule")]
    [ModuleDependency("DependencyModule")]
    public class DependantModule : IModule
    {
        public void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
