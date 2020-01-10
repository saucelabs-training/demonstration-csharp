using System;
using Common;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;

namespace Appium4.NUnit.Framework.RealDevices.Browser
{
    public class BaseMobileTest
    {
        private SessionId _sessionId;
        public RemoteWebDriver Driver;
        public AppiumOptions BrowserCapabilities;
        private string _platformName;
        private string _platformVersion;

        public BaseMobileTest(string platformName, string platformVersion)
        {
            _platformName = platformName;
            _platformVersion = platformVersion;
        }

        public static string RdcServerUrlUs => "https://us1.appium.testobject.com/wd/hub";

        /* Make sure that you get the API key from your app in RDC
         * and store it in an environment variable on your system.
         * Then read the Env Variable as you see below
         */
        public static string SauceDemoMobileBrowserAppApiKey =>
            Environment.GetEnvironmentVariable(
                "SAUCE_DEMO_MOBILE_WEB_RDC_API_KEY", EnvironmentVariableTarget.User);
        [SetUp]
        public void Setup()
        {
            BrowserCapabilities = new AppiumOptions();
            //this is the API key that you get from your app in Test Object
            BrowserCapabilities.AddAdditionalCapability("testobject_api_key", SauceDemoMobileBrowserAppApiKey);
            BrowserCapabilities.AddAdditionalCapability("deviceOrientation", "portrait");
            BrowserCapabilities.AddAdditionalCapability("browserName", "chrome");
            BrowserCapabilities.AddAdditionalCapability("name", TestContext.CurrentContext.Test.FullName);
            BrowserCapabilities.AddAdditionalCapability("newCommandTimeout", 90);
            BrowserCapabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, _platformVersion);
            BrowserCapabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, _platformName);
            Driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), BrowserCapabilities);
        }
        [TearDown]
        public void Teardown()
        {
            if (Driver == null) return;

            _sessionId = Driver.SessionId;
            Driver.Quit();
            var isTestPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
            new SimpleSauce().Rdc.UpdateTestStatus(isTestPassed, _sessionId);
        }

    }
}