using System;
using System.Reflection;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using RestSharp;

namespace Appium3.MsTest.Scripts.RealDevices.NativeApp
{
    [TestClass]
    [TestCategory("MsTest")]
    [TestCategory("Rdc")]
    [TestCategory("iOS")]

    public class iOSRdcTests
    {
        private SessionId _sessionId;
        private IOSDriver<IWebElement> _driver;
        private static string USurl => "https://us1.appium.testobject.com/wd/hub";

        //Always store API keys in environment variables
        private static readonly string SauceDemoIosRdcApiKey =
            Environment.GetEnvironmentVariable("SAUCE_DEMO_IOS_RDC_API_KEY", EnvironmentVariableTarget.User);
        public TestContext TestContext { get; set; }

        /**
         With dynamic allocation, you provide basic parameters for the platform and operating system, 
            or the type of device you want to use in your tests, 
            and a device with those specifications is selected from the device pool. 
            While static allocation allows you more fine-grained control over the device used in your tests, 
            it can also cause delays in your test execution if that device isn't available when you run your tests. 
            If you only need to test on a particular platform and OS version, such as an Android 4.1,  
            or on a particular type of device, you should use dynamic allocation, 
            and we recommend that you use dynamic allocation for all automated mobile application testing 
            in CI/CD environments.        
        */
        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void ShouldFailAndSetTestStatusToFail()
        {
            var capabilities = new DesiredCapabilities();
            capabilities.SetCapability("platformName", "iOS");
            capabilities.SetCapability("platformVersion", "13.3");
            //TODO first you must upload an app to RDC so that you get your app key
            capabilities.SetCapability("testobject_api_key", SauceDemoIosRdcApiKey);
            capabilities.SetCapability("name", MethodBase.GetCurrentMethod().Name);
            capabilities.SetCapability("newCommandTimeout", 90);

            _driver = new IOSDriver<IWebElement>(new Uri(USurl), capabilities,
                TimeSpan.FromSeconds(300));
            Assert.Fail("Sample test that should fail to make sure the correct status is logged in RDC");
        }
        [TestMethod]
        public void ShouldPassAndSetTestStatusToPass()
        {
            var capabilities = new DesiredCapabilities();
            capabilities.SetCapability("platformName", "iOS");
            capabilities.SetCapability("platformVersion", "13.3");
            //TODO first you must upload an app to RDC so that you get your app key
            capabilities.SetCapability("testobject_api_key", SauceDemoIosRdcApiKey);
            capabilities.SetCapability("name", MethodBase.GetCurrentMethod().Name);
            capabilities.SetCapability("newCommandTimeout", 90);

            _driver = new IOSDriver<IWebElement>(new Uri(USurl), capabilities,
                TimeSpan.FromSeconds(300));
            Assert.IsTrue(true);
        }

        [TestCleanup]
        public void Teardown()
        {
            if (_driver == null) return;

            var isTestPassed = TestContext.CurrentTestOutcome == UnitTestOutcome.Passed;
            new SimpleSauce().Rdc.UpdateTestStatus(isTestPassed, _driver.SessionId);
            _driver.Quit();
        }
    }
}
