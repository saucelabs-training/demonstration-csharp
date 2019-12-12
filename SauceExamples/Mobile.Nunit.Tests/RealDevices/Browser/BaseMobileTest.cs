using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Remote;
using RestSharp;

namespace Appium4.NUnit.Framework.RealDevices.Browser
{
    public class BaseMobileTest
    {
        public SessionId _sessionId;
        public RemoteWebDriver _driver;
        public DesiredCapabilities browserCapabilities;

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
            browserCapabilities = new DesiredCapabilities();
            //this is the API key that you get from your app in Test Object
            browserCapabilities.SetCapability("testobject_api_key", SauceDemoMobileBrowserAppApiKey);
            browserCapabilities.SetCapability("deviceOrientation", "portrait");
            browserCapabilities.SetCapability("browserName", "chrome");
            browserCapabilities.SetCapability("name", TestContext.CurrentContext.Test.Name);
            browserCapabilities.SetCapability("newCommandTimeout", 90);
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