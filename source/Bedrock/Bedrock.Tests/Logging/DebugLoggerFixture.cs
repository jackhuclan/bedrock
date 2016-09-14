using Bedrock.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bedrock.Tests.Logging
{
    [TestClass]
    public class DebugLoggerFixture
    {
        [TestMethod]
        public void LogShouldNotFail()
        {
            ILoggerFacade logger = new DebugLogger();
            logger.Log(null, Category.Debug, Priority.Medium);
        }
    }
}