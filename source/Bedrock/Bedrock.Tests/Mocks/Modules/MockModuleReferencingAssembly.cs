using Bedrock.Modularity;

namespace Bedrock.Tests.Mocks.Modules
{
    public class MockModuleReferencingAssembly : IModule
    {
        public void Initialize()
        {
            MockReferencedModule instance = new MockReferencedModule();
        }
    }
}