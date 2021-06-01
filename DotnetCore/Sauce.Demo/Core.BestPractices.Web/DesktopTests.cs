using System;
using System.Collections.Generic;
using Core.BestPractices.Web.Pages;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;

namespace Core.BestPractices.Web
{
    //[TestFixtureSource(typeof(CrossBrowserData), nameof(CrossBrowserData.MostPopularConfigurations))]
    [Parallelizable]
    public class DesktopTests : WebTestsBase
    {
        private string _sauceUserName;
        private string _sauceAccessKey;
        private Dictionary<string, object> _sauceOptions;
        private RemoteWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            _sauceUserName = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            _sauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
            _sauceOptions = new Dictionary<string, object>
            {
                ["username"] = _sauceUserName,
                ["accessKey"] = _sauceAccessKey,
                ["name"] = TestContext.CurrentContext.Test.Name
            };

            var browserOptions = new EdgeOptions
            {
                BrowserVersion = "latest",
                PlatformName = "Windows 10"
                //AcceptInsecureCertificates = true //Insecure Certs are Not supported by Edge
            };

            browserOptions.AddAdditionalCapability("sauce:options", _sauceOptions);

            _driver = new RemoteWebDriver(new Uri("https://ondemand.saucelabs.com/wd/hub"), browserOptions.ToCapabilities(),
                TimeSpan.FromSeconds(30));
        }

        [TearDown]
        public void CleanUpAfterEveryTestMethod()
        {
            if(_driver == null)
                return;
            ExecuteSauceCleanupSteps();
            _driver.Quit();
        }

        private void ExecuteSauceCleanupSteps()
        {
            var isPassed = TestContext.CurrentContext.Result.Outcome.Status
                           == TestStatus.Passed;
            var script = "sauce:job-result=" + (isPassed ? "passed" : "failed");
            ((IJavaScriptExecutor)_driver).ExecuteScript(script);
        }

        [Test]
        public void LoginWorks()
        {
            var loginPage = new LoginPage(_driver);
            loginPage.Visit();
            loginPage.Login("standard_user");
            new ProductsPage(_driver).IsVisible().Should().NotThrow();
        }
    }
}