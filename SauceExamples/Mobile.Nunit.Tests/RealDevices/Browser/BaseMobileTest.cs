using System;
using Common.SauceLabs;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;

namespace Appium4.NUnit.Framework.RealDevices.Browser
{
    public class BaseMobileTest
    {
        private SessionId _sessionId;
        public RemoteWebDriver Driver;
        private AppiumOptions _browserCapabilities;
        private readonly string _platformName;
        private readonly string _deviceName;
        private readonly string _browserName;

        public BaseMobileTest(string platformName, string deviceName, string browserName)
        {
            _platformName = platformName;
            _deviceName = deviceName;
            _browserName = browserName;
        }

        public string HubUrl => $"https://" +
                                        $"{Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User)}" +
                                        $":{Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User)}" +
                                        $"@ondemand.us-west-1.saucelabs.com/wd/hub";


        [SetUp]
        public void Setup()
        {
            _browserCapabilities = new AppiumOptions();
            _browserCapabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, _deviceName);
            _browserCapabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, _platformName);
            _browserCapabilities.AddAdditionalCapability("browserName", _browserName);
            _browserCapabilities.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);
            _browserCapabilities.AddAdditionalCapability("newCommandTimeout", 180);

            Driver = new RemoteWebDriver(new Uri(HubUrl), _browserCapabilities);
        }
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