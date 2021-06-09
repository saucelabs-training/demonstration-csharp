using NUnit.Framework;
using System.Collections.Generic;

namespace Core.BestPractices.Web.Tests.Desktop
{
    [TestFixture]
    public class WebTestsBase : AllTestsBase
    {
        [TearDown]
        public void CleanUpAfterEveryTestMethod()
        {
            if (Driver == null)
                return;
            ExecuteSauceCleanupSteps(Driver);
            Driver.Quit();
        }
        [SetUp]
        public void Setup()
        {
            SauceOptions = new Dictionary<string, object>
            {
                ["username"] = SauceUserName,
                ["accessKey"] = SauceAccessKey,
                ["name"] = TestContext.CurrentContext.Test.Name,
                ["build"] = Constants.BuildId
            };
        }
    }
}