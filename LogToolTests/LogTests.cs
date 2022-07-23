using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using Bygdrift.Tools.LogTool;
using Bygdrift.Tools.LogTool.Models;

namespace LogToolTests
{
    [TestClass]
    public class LogTests
    {
        [TestMethod]
        public void SimpleLogMessage()
        {
            Mock<ILogger> loggerMock = new();

            var log = new Log(loggerMock.Object);
            log.LogInformation("Started");
            log.LogError("Error thrown: {A}", "An error");  //Messages can be parsed with curly brackets like in ILogger: loggerMock.Object.LogError("Error thrown: {A}", "An error");

            var logs = log.GetLogs();  //This is not possible with ILogger
            Assert.IsTrue(logs.Count() == 2);

            var errors = log.GetErrorsAndCriticals();  //This is not possible with ILogger
            Assert.IsTrue(errors.Count() == 1);
        }

        [TestMethod]
        public void AddLogMessage()
        {
            var log = new Log();
            log.LogError("-{A}- {B}-", "a", 1);
            log.LogError("-{A}- {B}-", "a");
            log.LogError("-{A}- {B}-");
            log.LogError("test");

            var errors = log.GetErrorsAndCriticals();
            Assert.AreEqual(errors.Count(), 4);

            Assert.AreEqual(log.GetLogs().ElementAt(0), "-a- 1-");
            Assert.AreEqual(log.GetLogs().ElementAt(1), "-a- {B}-");
            Assert.AreEqual(log.GetLogs().ElementAt(2), "-{A}- {B}-");
            Assert.AreEqual(log.GetLogs().ElementAt(3) ,"test");
        }

        [TestMethod]
        public void AddUniqueMessages()
        {
            var log = new Log();
            log.LogInformation("test");
            AddError(log, "test2");  //Adds an error from another method

            var aa = log.Logs.Last().Caller;
            

            Assert.AreEqual(log.GetLogs(CallerPath.NamespaceName).Count(), 2);
            Assert.AreEqual(log.GetLogs(CallerPath.NamespaceClassName).Count(), 2);
            Assert.AreEqual(log.GetLogs(CallerPath.NamespaceClassMethodName).Count(), 1);
            Assert.IsFalse(log.HasErrorsOrCriticals(CallerPath.NamespaceClassMethodName));
        }

        private void AddError(Log log, string message)
        {
            log.LogError(message);
        }
    }
}