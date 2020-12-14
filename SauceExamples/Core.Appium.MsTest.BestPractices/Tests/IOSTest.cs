using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;

namespace Core.Appium.MsTest.BestPractices.Tests
{
    public class IOSTest : BaseTest
    {
        public IOSDriver<IOSElement> Driver;
        [TestInitialize]
        //[DynamicData(nameof(MsTestCrossBrowserData.LatestConfigurations), typeof(MsTestCrossBrowserData))]
        public void Setup()
        {
            var capabilities = new AppiumOptions();
            //We can run on any version of the platform as long as it's the correct device
            //Make sure to pick an Android or iOS device based on your app
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "iPhone 11 Pro");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");
            capabilities.AddAdditionalCapability("newCommandTimeout", 90);
            capabilities.AddAdditionalCapability("name", TestContext.TestName);
            /*
             * You need to upload your own Native Mobile App to Sauce Storage!
             * https://wiki.saucelabs.com/display/DOCS/Uploading+your+Application+to+Sauce+Storage
             * You can use either storage:<app-id> or storage:filename=
             */
            capabilities.AddAdditionalCapability("app",
                "storage:filename=iOS.RealDevice.Sample.ipa");
            /*
             Getting Error: OpenQA.Selenium.WebDriverException : The HTTP request to the remote WebDriver server for URL 
            https://us1.appium.testobject.com/wd/hub/session timed out after 60 seconds.
                ----> System.Net.WebException : The operation has timed out
            Solution: Try changing to a more popular device
             */
            Driver = new IOSDriver<IOSElement>(new Uri(Url), capabilities);
        }
        [TestCleanup]
        public void Teardown()
        {
            if (Driver == null) return;

            var isTestPassed = TestContext.CurrentTestOutcome == UnitTestOutcome.Passed;
            ((IJavaScriptExecutor)Driver).ExecuteScript("sauce:job-result=" + (isTestPassed ? "passed" : "failed"));
            Driver.Quit();
        }
    }
}