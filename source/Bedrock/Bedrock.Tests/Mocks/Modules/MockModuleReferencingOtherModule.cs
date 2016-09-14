using Bedrock.Modularity;

namespace Bedrock.Tests.Mocks.Modules
{
    public class MockModuleReferencingOtherModule : IModule
    {
        public void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }

    public class MyDummyClass : DummyClass {}
}
