using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using System;

namespace Core.BestPractices.Web.Tests
{
    public class MobileBaseTest
    {
        public readonly string DeviceName;
        public readonly string Platform;
        public readonly string Browser;
        public string URI;
        public IWebDriver Driver;
        private static string HubUrl => "ondemand.us-west-1.saucelabs.com/wd/hub";


        public MobileBaseTest(string deviceName, string platform, string browser)
        {
            DeviceName = deviceName;
            Platform = platform;
            Browser = browser;
        }
        [SetUp]
        public void MobileBaseSetup()
        {
            SauceUser = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            SauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
            URI = $"https://{SauceUser}:{SauceAccessKey}@{HubUrl}";

            MobileOptions = new AppiumOptions();
            MobileOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, DeviceName);
            MobileOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, Platform);
            MobileOptions.AddAdditionalCapability(MobileCapabilityType.BrowserName, Browser);
            MobileOptions.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);
            MobileOptions.AddAdditionalCapability("newCommandTimeout", 90);
        }

        public AppiumOptions MobileOptions { get; set; }

        public string? SauceAccessKey { get; set; }

        public string? SauceUser { get; set; }

        //Never forget to pass the test status to Sauce Labs
        [TearDown]
        public void Teardown()
        {
            if (Driver == null) return;

            var isTestPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
            ((IJavaScriptExecutor)Driver).ExecuteScript("sauce:job-result=" + (isTestPassed ? "passed" : "failed"));
            Driver.Quit();
        }
    }
}