using Core.BestPractices.Web.Pages;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;

namespace Core.BestPractices.Web.Tests
{
    [TestFixtureSource(typeof(TestConfigData), nameof(TestConfigData.PopularRealDevices))]
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class IOSVisual
    {
        private readonly DriverOptions _browserOptions;
        private readonly string _viewportSize;
        private readonly string _deviceName;
        private Dictionary<string, object> _visualOptions;

        public string SauceUserName { get; private set; }
        public string SauceAccessKey { get; private set; }
        public string ScreenerApiKey { get; private set; }
        public Dictionary<string, object> SauceOptions { get; private set; }
        public IWebDriver Driver { get; private set; }

        public IOSVisual(DriverOptions browserOptions, string viewportSize, string deviceName)
        {
            _browserOptions = browserOptions;
            _viewportSize = viewportSize;
            _deviceName = deviceName;
        }

        [SetUp]
        public void VisualSetup()
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

            _visualOptions = new Dictionary<string, object>
            {
                { "apiKey", ScreenerApiKey},
                { "projectName", "Sauce Demo C#" },
                { "viewportSize", _viewportSize}
            };

            var browserOptions = _browserOptions;
            browserOptions.AddAdditionalCapability("sauce:options", SauceOptions);
            browserOptions.AddAdditionalCapability("sauce:visual", _visualOptions);

            //TimeSpan.FromSeconds(120) = needed so that there isn't a 'The HTTP request to the remote WebDriver server for URL' error
            Driver = new RemoteWebDriver(new Uri("https://hub.screener.io:443/wd/hub"), browserOptions.ToCapabilities(),
                TimeSpan.FromSeconds(120));
            //Needed so that Screener 'end' command doesn't timeout
            Driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(60);
        }

        [TearDown]
        public void CleanupVisual()
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


        [Test]
        public void VisualE2EFlow()
        {
            CaptureApplicationSnapshots();
        }
        private void CaptureApplicationSnapshots()
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("/*@visual.init*/", _deviceName);
            var loginPage = new LoginPage(Driver);
            loginPage.Visit();
            loginPage.TakeSnapshot();

            loginPage.Login("standard_user");
            new ProductsPage(Driver).TakeSnapshot();
            var result = (Dictionary<string, object>)((IJavaScriptExecutor)Driver).ExecuteScript("/*@visual.end*/");
            result["message"].Should().BeNull();
        }
    }
}