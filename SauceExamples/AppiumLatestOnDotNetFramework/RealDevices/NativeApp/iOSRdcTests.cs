using System;
using System.Reflection;
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

        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void ShouldFailAndSetTestStatusToFail()
        {
            var capabilities = new DesiredCapabilities();
            capabilities.SetCapability("platformName", "iOS");
            capabilities.SetCapability("platformVersion", "12.2");
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
            capabilities.SetCapability("platformVersion", "12.2");
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
