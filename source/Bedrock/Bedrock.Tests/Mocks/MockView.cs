using System;
using Bedrock.Views;

namespace Bedrock.Tests.Mocks
{
    public class MockView : IView
    {
        public string Name { get; set; }
        public object DataContext { get; set; }
        public void RegisterRegion(string regionName, object control)
        {
            throw new NotImplementedException();
        }
    }
}
