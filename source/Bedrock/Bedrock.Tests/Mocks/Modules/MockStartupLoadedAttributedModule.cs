using Bedrock.Modularity;

namespace Bedrock.Tests.Mocks.Modules
{
    public class MockStartupLoadedAttributedModule
    {
        [Module(ModuleName = "TestModule", StartupLoaded = false)]
        public class MockAttributedModule : IModule
        {
            public void Initialize()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
