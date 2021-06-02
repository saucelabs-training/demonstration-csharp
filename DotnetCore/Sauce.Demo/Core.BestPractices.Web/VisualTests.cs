using System;
using System.Collections.Generic;
using Core.BestPractices.Web.Pages;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;

namespace Core.BestPractices.Web
{
    //[TestFixtureSource(typeof(CrossBrowserData), nameof(CrossBrowserData.MostPopularConfigurations))]
    [Parallelizable]
    public class VisualTests : WebTestsBase
    {
        private Dictionary<string, object> _visualOptions;

        [SetUp]
        public void VisualSetup()
        {
            _visualOptions = new Dictionary<string, object>
            {
                { "apiKey", ScreenerApiKey},
                { "projectName", "Sauce Demo C#" }
            };
        }

        [TearDown]
        public void CleanUpAfterEveryTestMethod()
        {
            if(Driver == null)
                return;
            var result = (Dictionary<string, object>)new Browser(Driver).JS.ExecuteScript("/*@visual.end*/");
            result["message"].Should().BeNull();
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

        [Test]
        public void LooksCorrectOnIPhoneX()
        {
            _visualOptions.Add("viewportSize", "375x812");

            var safariOptions = new SafariOptions
            {
                BrowserVersion = "latest",
                PlatformName = "macOS 10.15"
            };
            safariOptions.AddAdditionalCapability("sauce:options", SauceOptions);
            safariOptions.AddAdditionalCapability("sauce:visual", _visualOptions);

            Driver = new RemoteWebDriver(new Uri("https://hub.screener.io:443/wd/hub"), safariOptions.ToCapabilities(),
                TimeSpan.FromSeconds(30));
            new Browser(Driver).JS.ExecuteScript("/*@visual.init*/", "iPhone X");

            var loginPage = new LoginPage(Driver);
            loginPage.Visit();
            loginPage.TakeSnapshot();

        }

        [Test]
        public void LooksCorrectOnPixelXL()
        {
            _visualOptions.Add("viewportSize", "412x732");

            var safariOptions = new ChromeOptions()
            {
                BrowserVersion = "latest",
                PlatformName = "Windows 10"
            };
            safariOptions.AddAdditionalCapability("sauce:options", SauceOptions, true);
            safariOptions.AddAdditionalCapability("sauce:visual", _visualOptions, true);

            Driver = new RemoteWebDriver(new Uri("https://hub.screener.io:443/wd/hub"), safariOptions.ToCapabilities(),
                TimeSpan.FromSeconds(30));
            new Browser(Driver).JS.ExecuteScript("/*@visual.init*/", "Pixel XL");

            var loginPage = new LoginPage(Driver);
            loginPage.Visit();
            loginPage.TakeSnapshot();
        }
    }
}