using System;
using System.Collections.Generic;
using Common.SauceLabs;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Selenium3.Nunit.Scripts.SaucePerformance
{
    [TestFixture]
    [Category("performance")]
    public class PerformanceDemo
    {
        public RemoteWebDriver Driver { get; set; }

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
                ["capturePerformance"] = true
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

        public void W3CPerformanceTestForSauceDemo()
        {
            //Login steps here
            Driver.Navigate().GoToUrl("https://www.saucedemo.com");
        }

        [Test]
        public void W3CPeformanceTestForUltimateQA()
        {
            Driver.Navigate().GoToUrl("https://www.ultimateqa.com");
        }

        [Test]
        public void SauceDemoLoadShouldBeWithin20Percent()
        {
            Driver.Navigate().GoToUrl("https://www.saucedemo.com");

            var metrics = new Dictionary<string, object>
            {
                ["type"] = "sauce:performance"
            };
            var performanceMetrics = (Dictionary<string, object>)((IJavaScriptExecutor)Driver).ExecuteScript("sauce:log", metrics);
            Assert.That(performanceMetrics["load"], Is.EqualTo(450).Within(20).Percent);
        }
        [Test]
        public void SauceDemoSpeedIndexShouldBeWithin20Percent()
        {
            Driver.Navigate().GoToUrl("https://www.saucedemo.com");

            var metrics = new Dictionary<string, object>
            {
                ["type"] = "sauce:performance"
            };
            var performanceMetrics = (Dictionary<string, object>)((IJavaScriptExecutor)Driver).ExecuteScript("sauce:log", metrics);
            Assert.That(performanceMetrics["speedIndex"], Is.EqualTo(415).Within(20).Percent);
        }
    }
}
