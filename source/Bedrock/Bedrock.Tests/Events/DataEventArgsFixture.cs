using System;
using Bedrock.Events;
using Xunit;

namespace Bedrock.Tests.Events
{
    public class DataEventArgsFixture
    {
        [Fact]
        public void CanPassData()
        {
            DataEventArgs<int> e = new DataEventArgs<int>(32);
            Assert.Equal(32, e.Value);
        }

        [Fact]
        public void IsEventArgs()
        {
            DataEventArgs<string> dea = new DataEventArgs<string>("");
            EventArgs ea = dea as EventArgs;
            Assert.NotNull(ea);
        }
    }
}