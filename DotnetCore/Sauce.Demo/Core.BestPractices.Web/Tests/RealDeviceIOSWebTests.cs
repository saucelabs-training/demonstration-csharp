using System;
using Core.BestPractices.Web.Pages;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;

namespace Core.BestPractices.Web.Tests
{
    [TestFixtureSource(typeof(TestConfigData), nameof(TestConfigData.MostPopularIOSDevices))]
    [Parallelizable]
    public class RealDeviceIOSWebTests
    {
        private readonly string _deviceName;
        private readonly string _platform;
        private readonly string _browser;

        public RealDeviceIOSWebTests(string deviceName, string platform, string browser)
        {
            _deviceName = deviceName;
            _platform = platform;
            _browser = browser;
        }
        private static string HubUrl => "ondemand.us-west-1.saucelabs.com/wd/hub";
        //Must use a unique driver for iOS/Android
        private IOSDriver<IOSElement> _driver;

        [Test]

        public void ShouldOpenHomePage()
        {
            //It's a best practice to store credentials in environment variables
            var sauceUser = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            var sauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
            var uri = $"https://{sauceUser}:{sauceAccessKey}@{HubUrl}";

            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, _deviceName);
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, _platform);
            capabilities.AddAdditionalCapability(MobileCapabilityType.BrowserName, _browser);
            capabilities.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);
            capabilities.AddAdditionalCapability("newCommandTimeout", 90);

            //60 seconds default for the connection timeout
            _driver = new IOSDriver<IOSElement>(new Uri(uri), capabilities);
            _driver.Navigate().GoToUrl("http://www.saucedemo.com");
            var size = _driver.Manage().Window.Size;
            Assert.AreNotEqual(0, size.Height);
        }

        //Never forget to pass the test status to Sauce Labs
        [TearDown]
        public void Teardown()
        {
            if (_driver == null) return;

            var isTestPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
            ((IJavaScriptExecutor)_driver).ExecuteScript("sauce:job-result=" + (isTestPassed ? "passed" : "failed"));
            _driver.Quit();
        }
    }
}