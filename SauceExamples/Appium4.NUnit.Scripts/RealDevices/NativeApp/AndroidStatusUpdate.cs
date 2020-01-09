using System;
using System.Reflection;
using Common;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;

namespace Appium4.NUnit.Scripts.RealDevices.NativeApp
{
    [TestFixture]
    public class AndroidStatusUpdate
    {
        /*
         * This is the very basic setup that you should have for your test automation.
         * This include a setup and teardown, Quit(), and results update.
         * Ultimately, you should always use the POM that can be found here:
         * Selenium.Nunit.Framework.BestPractices.test
         */
        private static string RdcUsHubUrl => "https://us1.appium.testobject.com/wd/hub";
        private AndroidDriver<AndroidElement> _driver;
        private SessionId _sessionId;

        [SetUp]
        public void Setup()
        {
            var capabilities = new AppiumOptions();
            //We can run on any version of the platform as long as it's the correct device
            //Make sure to pick an Android or iOS device based on your app
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Google Pixel");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");

            /*
             * !!!!!!
             * TODO first you must upload an app to RDC so that you get your app key
             * Then, make sure you can hardcode it here just to get started
             */
            capabilities.AddAdditionalCapability("testobject_api_key", new ApiKeys().Rdc.Apps.SampleAppAndroid);
            capabilities.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);
            capabilities.AddAdditionalCapability("newCommandTimeout", 90);

            _driver = new AndroidDriver<AndroidElement>(new Uri(RdcUsHubUrl), capabilities);
        }
        [TearDown]
        public void Teardown()
        {
            if (_driver == null) return;

            _sessionId = _driver.SessionId;
            _driver.Quit();
            var isTestPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
            new SimpleSauce().Rdc.UpdateTestStatus(isTestPassed, _sessionId);
        }

        [Test]
        [Category("Android")]
        [Category("SimpleTest")]
        [Category("Rdc")]
        [Category("NativeApp")]
        [Category("Appium4NUnitScripts")]

        public void ShouldOpenApp()
        {
            var size = _driver.Manage().Window.Size;
            Assert.AreNotEqual(0, size.Height);
        }
    }
}
