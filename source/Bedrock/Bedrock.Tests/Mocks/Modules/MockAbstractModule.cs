using Bedrock.Modularity;

namespace Bedrock.Tests.Mocks.Modules
{
    public abstract class MockAbstractModule : IModule
    {
        public void Initialize()
        {
        }
    }

    public class MockInheritingModule : MockAbstractModule
    {
    }
}
