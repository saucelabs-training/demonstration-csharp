using System;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using RestSharp;

namespace Mobile.Nunit.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class MobileBrowserTests
    {
        private SessionId _sessionId;
        private RemoteWebDriver _driver;
        private static string RdcServerUrlUs => "https://us1.appium.testobject.com/wd/hub";

        
        /* Make sure that you get the API key from your app in RDC
         * and store it in an environment variable on your system.
         * Then read the Env Variable as you see below
         */
        private static string SauceDemoMobileBrowserAppApiKey =>
            Environment.GetEnvironmentVariable(
                "SAUCE_DEMO_MOBILE_WEB_RDC_API_KEY", EnvironmentVariableTarget.User);

        [Test]
        public void MobileBrowser_Android_ShouldPass()
        {
            DesiredCapabilities caps = new DesiredCapabilities();
            //this is the API key that you get from your app in Test Object
            caps.SetCapability("testobject_api_key", SauceDemoMobileBrowserAppApiKey);
            caps.SetCapability("deviceOrientation", "portrait");
            caps.SetCapability("browserName", "chrome");
            caps.SetCapability("platformVersion", "8.1");
            caps.SetCapability("platformName", "Android");
            _driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), caps, 
                TimeSpan.FromSeconds(600));
            _driver.Navigate().GoToUrl("https://www.saucedemo.com");
            _driver.Quit();
        }
        [TearDown]
        public void Teardown()
        {
            if (_driver != null)
            {
                _sessionId = _driver.SessionId;
                _driver.Quit();
            }

            var isTestPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;

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
