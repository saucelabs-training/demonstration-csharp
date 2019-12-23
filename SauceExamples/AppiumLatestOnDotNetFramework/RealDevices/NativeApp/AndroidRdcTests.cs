using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using RestSharp;

namespace Appium3.MsTest.Scripts.RealDevices.NativeApp
{
    [TestClass]
    [TestCategory("MsTest")]
    [TestCategory("Rdc")]
    [TestCategory("Android")]

    public class AndroidRdcTests
    {
        private SessionId _sessionId;
        private AndroidDriver<IWebElement> _driver;
        private static string USurl => "https://us1.appium.testobject.com/wd/hub";

        private static readonly string VodQANativeAppApiKey =
            Environment.GetEnvironmentVariable("VODQC_RDC_API_KEY", EnvironmentVariableTarget.User);
        public TestContext TestContext { get; set; }

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void ShouldFailAndSetTestStatusToFail()
        {
            var capabilities = new DesiredCapabilities();
            //Setting only the 2 capabilities below will run this test on 
            //any Android 7 device and this test runs in about 50s
            capabilities.SetCapability("platformName", "Android");
            capabilities.SetCapability("platformVersion", "7");
            //TODO first you must upload an app to Test Object so that you get your app key
            capabilities.SetCapability("testobject_api_key", VodQANativeAppApiKey);
            capabilities.SetCapability("name", MethodBase.GetCurrentMethod().Name);
            capabilities.SetCapability("newCommandTimeout", 90);

            _driver = new AndroidDriver<IWebElement>(new Uri(USurl), capabilities,
                TimeSpan.FromSeconds(300));
            Assert.Fail("Sample test that should fail to make sure the correct status is logged in RDC");
        }
        [TestMethod]
        public void ShouldPassAndSetTestStatusToPass()
        {
            var capabilities = new DesiredCapabilities();
            //Setting only the 2 capabilities below will run this test on 
            //any Android 8.1 device and this test runs in about 50s
            capabilities.SetCapability("platformName", "Android");
            capabilities.SetCapability("platformVersion", "8.1");
            //TODO first you must upload an app to Test Object so that you get your app key
            capabilities.SetCapability("testobject_api_key", VodQANativeAppApiKey);
            capabilities.SetCapability("name", MethodBase.GetCurrentMethod().Name);
            capabilities.SetCapability("newCommandTimeout", 90);

            _driver = new AndroidDriver<IWebElement>(new Uri(USurl), capabilities,
                TimeSpan.FromSeconds(300));
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void DynamicAllocationUsingDeviceName()
        {
            var capabilities = new DesiredCapabilities();
            //We can run on any version of the platform as long as it's the correct device
            //Make sure to pick an Android or iOS device based on your app
            capabilities.SetCapability("deviceName", "Google Pixel");
            //TODO first you must upload an app to Test Object so that you get your app key
            capabilities.SetCapability("testobject_api_key", VodQANativeAppApiKey);
            capabilities.SetCapability("name", MethodBase.GetCurrentMethod().Name);
            capabilities.SetCapability("newCommandTimeout", 90);

            _driver = new AndroidDriver<IWebElement>(new Uri(USurl), capabilities,
                TimeSpan.FromSeconds(300));
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]

        public void DynamicAllocationForAnyPlatformAndVersion()
        {
            var capabilities = new DesiredCapabilities();
            /*
             * This will execute a test on any available device
             */
            capabilities.SetCapability("platformName", ".*");
            capabilities.SetCapability("platformVersion", ".*");
            capabilities.SetCapability("testobject_api_key", VodQANativeAppApiKey);
            capabilities.SetCapability("name", MethodBase.GetCurrentMethod().Name);
            capabilities.SetCapability("newCommandTimeout", 90);

            _driver = new AndroidDriver<IWebElement>(new Uri(USurl), capabilities,
                TimeSpan.FromSeconds(300));
            _sessionId = _driver.SessionId;
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DynamicAllocationForAnyGalaxyDevice()
        {
            var capabilities = new DesiredCapabilities();
            /*
             * This will execute a test on any Galaxy device
             */
            capabilities.SetCapability("platformName", ".*");
            capabilities.SetCapability("platformVersion", ".*Galaxy.*");

            capabilities.SetCapability("testobject_api_key", VodQANativeAppApiKey);
            capabilities.SetCapability("name", MethodBase.GetCurrentMethod().Name);
            capabilities.SetCapability("newCommandTimeout", 90);

            _driver = new AndroidDriver<IWebElement>(new Uri(USurl), capabilities,
                TimeSpan.FromSeconds(300));
        }
        [TestMethod]
        [TestCategory("VodQANativeApp")]
        public void StaticAllocation()
        {
            /*
             * By default, every time you complete a test session,
             * the real device cloud uninstalls your application,
             * performs device cleaning, and de-allocates the device.
             * This means that if you have multiple tests that you want to run on the same device,
             * you will, by default,
             * wait for this cleaning process to complete between every test.
             */
            //capabilities.SetCapability("platformName", "*");
            //capabilities.SetCapability("platformVersion", "*");
            var capabilities = new DesiredCapabilities();
            capabilities.SetCapability("deviceName", "Asus_Google_Nexus_7_2013_real");

            capabilities.SetCapability("testobject_api_key", VodQANativeAppApiKey);
            capabilities.SetCapability("name", MethodBase.GetCurrentMethod().Name);
            capabilities.SetCapability("newCommandTimeout", 90);

            _driver = new AndroidDriver<IWebElement>(new Uri(USurl), capabilities,
                TimeSpan.FromSeconds(300));

            Assert.IsTrue(_driver.FindElementByAccessibilityId("username").Displayed);
        }


        [TestCleanup]
        public void Teardown()
        {
            if (_driver != null)
            {
                _sessionId = _driver.SessionId;
                _driver.Quit();
            }

            var isTestPassed = TestContext.CurrentTestOutcome == UnitTestOutcome.Passed;

            var client = new RestClient
            {
                BaseUrl = new Uri("https://app.testobject.com/api/rest")
            };
            var request = new RestRequest($"/v2/appium/session/{_sessionId}/test", Method.PUT)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(new { passed = isTestPassed });
            client.Execute(request);
        }
    }
}
