using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using System;

namespace Core.BestPractices.Web.Tests.Mobile.IOS
{
    [TestFixtureSource(typeof(TestConfigData), nameof(TestConfigData.MostPopularIOSDevices))]
    [Parallelizable]
    public class RealDeviceIOSWebTests : MobileBaseTest
    {
        public RealDeviceIOSWebTests(string deviceName, string platform, string browser) :
            base(deviceName, platform, browser)
        { }
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
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, DeviceName);
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, Platform);
            capabilities.AddAdditionalCapability(MobileCapabilityType.BrowserName, Browser);
            capabilities.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);
            capabilities.AddAdditionalCapability("newCommandTimeout", 90);

            //60 seconds default for the connection timeout
            _driver = new IOSDriver<IOSElement>(new Uri(uri), capabilities, TimeSpan.FromSeconds(120));
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