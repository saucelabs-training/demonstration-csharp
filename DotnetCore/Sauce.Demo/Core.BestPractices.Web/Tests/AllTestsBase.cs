using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Core.BestPractices.Web.Tests
{
    [TestFixture]
    public class AllTestsBase
    {
        public RemoteWebDriver Driver;

        [TearDown]
        public void CleanUpAfterEveryTestMethod()
        {
            if (Driver == null)
                return;
            ExecuteSauceCleanupSteps();
            Driver.Quit();
        }
        private void ExecuteSauceCleanupSteps()
        {
            var isPassed = TestContext.CurrentContext.Result.Outcome.Status
                           == TestStatus.Passed;
            var script = "sauce:job-result=" + (isPassed ? "passed" : "failed");
            ((IJavaScriptExecutor)Driver).ExecuteScript(script);
        }
    }
}