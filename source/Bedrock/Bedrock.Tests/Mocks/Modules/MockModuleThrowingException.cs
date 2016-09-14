using Bedrock.Modularity;

namespace Bedrock.Tests.Mocks.Modules
{
    public class MockModuleThrowingException : IModule
    {
        public void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
