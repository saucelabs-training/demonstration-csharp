using Common;
using Common.SauceLabs;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace Selenium3.Nunit.Scripts.BestPractices
{
    [TestFixture()]
    [Category("AcceptanceTests")]
    public class BaseTest
    {
        [SetUp]
        public void ExecuteBeforeEveryTestMethod()
        {
            Driver = new WebDriverFactory().CreateSauceDriver(TestContext.CurrentContext.Test.Name);
        }
        [TearDown]
        public void CleanUpAfterEveryTestMethod()
        {
            //Make sure to check if your Driver is null before performing operations
            //with it in TearDown. It might not be initialized correctly
            if (Driver != null)
            {
                new SauceJavaScriptExecutor(Driver).LogTestStatus(TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed);
                Driver?.Quit();
            }
        }
        public IWebDriver Driver { get; set; }
    }
}
