using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Core.BestPractices.Web.Tests
{
    [TestFixture]
    public class WebTestsBase : AllTestsBase
    {
        [TearDown]
        public void CleanUpAfterEveryTestMethod()
        {
            if (Driver == null)
                return;
            ExecuteSauceCleanupSteps();
            Driver.Quit();
        }
        [SetUp]
        public void Setup()
        {
            SauceUserName = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            SauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
            ScreenerApiKey = Environment.GetEnvironmentVariable("SCREENER_API_KEY", EnvironmentVariableTarget.User);
            SauceOptions = new Dictionary<string, object>
            {
                ["username"] = SauceUserName,
                ["accessKey"] = SauceAccessKey,
                ["name"] = TestContext.CurrentContext.Test.Name
            };
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