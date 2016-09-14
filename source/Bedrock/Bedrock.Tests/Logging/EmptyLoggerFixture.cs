using Bedrock.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bedrock.Tests.Logging
{
    [TestClass]
    public class EmptyLoggerFixture
    {
        [TestMethod]
        public void LogShouldNotFail()
        {
            ILoggerFacade logger = new EmptyLogger();

            logger.Log(null, Category.Debug, Priority.Medium);
        }
    }
}