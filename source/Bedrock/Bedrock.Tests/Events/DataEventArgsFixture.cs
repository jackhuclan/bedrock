using System;
using Bedrock.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bedrock.Tests.Events
{
    [TestClass]
    public class DataEventArgsFixture
    {
        [TestMethod]
        public void CanPassData()
        {
            DataEventArgs<int> e = new DataEventArgs<int>(32);
            Assert.AreEqual(32, e.Value);
        }

        [TestMethod]
        public void IsEventArgs()
        {
            DataEventArgs<string> dea = new DataEventArgs<string>("");
            EventArgs ea = dea as EventArgs;
            Assert.IsNotNull(ea);
        }
    }
}