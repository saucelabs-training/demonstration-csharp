using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using Common.SauceLabs.SauceLabs;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using Newtonsoft.Json;
using OpenQA.Selenium.Support.UI;

namespace SeleniumNunit.SaucePerformance
{
    [TestFixture]
    [Category("performance")]
    public class CustomCapabilitiesTests
    {
        public RemoteWebDriver Driver { get; set; }

        [Test]
        public void NetworkThrottleOffline()
        {
            Driver.Navigate().GoToUrl("http://www.saucedemo.com");
            var throttleCondition = new Dictionary<string, object>
            {
                ["condition"] = "offline"
            };
            ((IJavaScriptExecutor)Driver).ExecuteScript("sauce:throttleNetwork", throttleCondition);
            Driver.Navigate().GoToUrl("http://www.saucedemo.com");
            Assert.Throws<WebDriverTimeoutException>(() => isDisplayed());
        }

        private bool isDisplayed()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            return wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("btn_action"))).Displayed;
        }

        [Test]
        public void NetworkThrottle3G()
        {
            var throttleCondition = new Dictionary<string, object>
            {
                ["condition"] = "Regular 3G"
            };
            ((IJavaScriptExecutor)Driver).ExecuteScript("sauce:throttleNetwork", throttleCondition);
            Driver.Navigate().GoToUrl("http://www.saucedemo.com");
        }
        [Test]
        public void GetSauceMetrics()
        {
            //https://wiki.saucelabs.com/display/DOCS/Custom+Sauce+Labs+WebDriver+Extensions+for+Network+and+Log+Commands
            //this works
            var metrics = new Dictionary<string, object>();
            metrics["type"] = "sauce:metrics";
            var output = ((IJavaScriptExecutor)Driver).ExecuteScript("sauce:log", metrics);
            ((IJavaScriptExecutor)Driver).ExecuteScript("sauce:network");
        }
        [Test]
        public void ReplaceImages()
        {
            Driver.Navigate().GoToUrl("http://www.wswebcreation.nl/");
            var sauceInterceptConditions = new Dictionary<string, object>
            {
                ["url"] = "**/*.jpg",
                ["redirect"] = "https://www.tvvantoen.nl/wp-content/themes/website/data/php/timthumb.php?src=https://www.tvvantoen.nl/wp-content/uploads/the-a-team-poster.jpg&w=700&q=100"
            };
            ((IJavaScriptExecutor)Driver).ExecuteScript("sauce:intercept", sauceInterceptConditions);
            Driver.Navigate().GoToUrl("http://www.wswebcreation.nl/");
        }
        [SetUp]
        public void RunBeforeEveryTest()
        {
            var sauceUserName = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            var sauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);

            var chromeOptions = new ChromeOptions()
            {
                BrowserVersion = "latest",
                PlatformName = "Windows 10",
                UseSpecCompliantProtocol = true
            };
            var sauceOptions = new Dictionary<string, object>
            {
                ["username"] = sauceUserName,
                ["accessKey"] = sauceAccessKey,
                ["name"] = TestContext.CurrentContext.Test.Name,
                ["extendedDebugging"] = true,
                ["capturePerformance"] = true,
                ["crmuxdriverVersion"] = "beta"
            };
            chromeOptions.AddAdditionalCapability("sauce:options", sauceOptions, true);

            Driver = new RemoteWebDriver(new Uri("https://ondemand.saucelabs.com/wd/hub"),
                chromeOptions.ToCapabilities(), TimeSpan.FromSeconds(600));
        }

        [TearDown]
        public void CleanUp()
        {
            var isPassed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed;
            new SauceJavaScriptExecutor(Driver).LogTestStatus(isPassed);
            Driver.Quit();
        }


    }
}
