using System;
using Common.SauceLabs;
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
        private AppiumOptions _browserCapabilities;
        private readonly string _platformName;
        private readonly string _platformVersion;
        private readonly string _browserName;

        public BaseMobileTest(string platformName, string platformVersion, string browserName)
        {
            _platformName = platformName;
            _platformVersion = platformVersion;
            _browserName = browserName;
        }

        public static string RdcServerUrlUs => "https://us1.appium.testobject.com/wd/hub";


        [SetUp]
        public void Setup()
        {
            _browserCapabilities = new AppiumOptions();
            /* Make sure that you get the API key from your app in RDC
             * and store it in an environment variable on your system.
             * Then read the Env Variable into your code
             */
            _browserCapabilities.AddAdditionalCapability("testobject_api_key", 
                new ApiKeys().Rdc.Apps.SauceDemoOnMobileBrowser);
            _browserCapabilities.AddAdditionalCapability("deviceOrientation", "portrait");
            _browserCapabilities.AddAdditionalCapability("browserName", _browserName);
            _browserCapabilities.AddAdditionalCapability("name", TestContext.CurrentContext.Test.FullName);
            _browserCapabilities.AddAdditionalCapability("newCommandTimeout", 180);
            _browserCapabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, _platformVersion);
            _browserCapabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, _platformName);
            Driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), _browserCapabilities);
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