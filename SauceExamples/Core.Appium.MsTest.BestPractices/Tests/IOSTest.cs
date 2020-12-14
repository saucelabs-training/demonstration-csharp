using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using TestContext = NUnit.Framework.TestContext;

namespace Core.Appium.Nunit.BestPractices.Tests
{
    public class IOSTest : BaseTest
    {
        public IOSDriver<IOSElement> Driver;
        public AppiumOptions AppiumCaps;
        private string _deviceName;

        public IOSTest(string deviceName)
        {
            _deviceName = deviceName;
        }

        [SetUp]
        public void Setup()
        {
            AppiumCaps = new AppiumOptions();
            AppiumCaps.AddAdditionalCapability(MobileCapabilityType.DeviceName, _deviceName);

            AppiumCaps.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");
            AppiumCaps.AddAdditionalCapability("newCommandTimeout", 90);
            AppiumCaps.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);
            /*
             * You need to upload your own Native Mobile App to Sauce Storage!
             * https://wiki.saucelabs.com/display/DOCS/Uploading+your+Application+to+Sauce+Storage
             * You can use either storage:<app-id> or storage:filename=
             */
            AppiumCaps.AddAdditionalCapability("app",
                "storage:filename=iOS.RealDevice.Sample.ipa");
            Driver = GetIosDriver(AppiumCaps);
        }
        [TearDown]
        public void Teardown()
        {
            if (Driver == null) return;

            var isTestPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
            ((IJavaScriptExecutor)Driver).ExecuteScript("sauce:job-result=" + (isTestPassed ? "passed" : "failed"));
            Driver.Quit();
        }
        public IOSDriver<IOSElement> GetIosDriver(AppiumOptions appiumOptions)
        {
            return new IOSDriver<IOSElement>(new Uri(Url), appiumOptions);
        }
    }
}