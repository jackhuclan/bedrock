using Bedrock.Modularity;

namespace Bedrock.Tests.Mocks.Modules
{
    [Module(ModuleName = "DependencyModule")]
    public class DependencyModule : IModule
    {
        public void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
