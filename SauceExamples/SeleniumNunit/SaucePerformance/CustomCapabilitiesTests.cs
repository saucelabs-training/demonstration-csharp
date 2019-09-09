using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using Common.SauceLabs.SauceLabs;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using Newtonsoft.Json;

namespace SeleniumNunit.SaucePerformance
{
    [TestFixture]
    [Category("performance")]
    public class CustomCapabilitiesTests
    {
        public RemoteWebDriver Driver { get; set; }

        [Test]
        public void NetworkThrottle()
        {
            //TODO doesn't work
            //dynamic redirectObject = new JObject();
            //redirectObject.url = "www.saucedemo.com";
            //redirectObject.redirect = "www.google.com";
            //var json = JsonConvert.SerializeObject(redirectObject);

            //TODO doesn't work
           // var redirect = new Dictionary<string, string>
           // {
           //     { "url", "www.saucedemo.com" },
           //     { "redirect", "www.google.com" }
           // };
           // string json = JsonConvert.SerializeObject(redirect, Formatting.Indented);
           //((IJavaScriptExecutor)Driver).ExecuteScript("sauce:intercept", json.ToString());


            var throttleCondition = new Dictionary<string, object>
            {
                ["condition"] = "Regular 3G"
            };
            ((IJavaScriptExecutor)Driver).ExecuteScript("sauce:throttleNetwork", throttleCondition);
            Driver.Navigate().GoToUrl("http://www.saucedemo.com");
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
        [Test]
        public void ReplaceImageUrls()
        {
            Driver.Navigate().GoToUrl("http://www.saucedemo.com");
            dynamic interceptObject = new JObject();
            interceptObject.url = "**/*.jpg";
            interceptObject.redirect = "www.google.com";

            ((IJavaScriptExecutor) Driver).ExecuteScript("sauce:intercept", interceptObject.ToString());

            //var throttleOptions = new Dictionary<string, object>();
            //throttleOptions["condition"] = "offline";
            //((IJavaScriptExecutor)_driver).ExecuteScript("sauce:throttle", throttleOptions);
            //_driver.Navigate().Refresh();
            //((IJavaScriptExecutor)_driver).ExecuteScript("sauce:performance");

            //timing didn't work
            //var timing = ((IJavaScriptExecutor)_driver).ExecuteScript("sauce:timing");


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
    }
}
