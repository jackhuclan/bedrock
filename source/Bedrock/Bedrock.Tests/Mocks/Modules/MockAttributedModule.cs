using Bedrock.Modularity;

namespace Bedrock.Tests.Mocks.Modules
{
    [Module(ModuleName = "TestModule", OnDemand = true)]
    public class MockAttributedModule : IModule
    {
        public void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
