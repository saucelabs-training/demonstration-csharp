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
    [TestFixture]
    public class MobileBrowserIOSTests : BaseMobileTest
    {
        [Test]
        public void MobileBrowser_iOS_ShouldPass()
        {
            DesiredCapabilities caps = new DesiredCapabilities();
            //this is the API key that you get from your app in Test Object
            caps.SetCapability("testobject_api_key", SauceDemoMobileBrowserAppApiKey);
            caps.SetCapability("deviceOrientation", "portrait");
            caps.SetCapability("platformVersion", "12.4");
            caps.SetCapability("platformName", "iOS");
            caps.SetCapability("name", TestContext.CurrentContext.Test.Name);
            caps.SetCapability("newCommandTimeout", 90);
            _driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), caps,
                TimeSpan.FromSeconds(600));
            _driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(_driver.Url.Contains("saucedemo.com"));
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
