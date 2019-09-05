using System;
using Common;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using RestSharp;
using RestSharp.Authenticators;

namespace SeleniumNunit.SimpleExamples
{
    [TestFixture]
    [Category("SimpleTest")]
    public class RestApiExamples
    {
        IWebDriver _driver;

        //TODO please supply your Sauce Labs user name in an environment variable
        private string sauceUserName = Environment.GetEnvironmentVariable(
            "SAUCE_USERNAME", EnvironmentVariableTarget.User);
        //TODO please supply your own Sauce Labs access Key in an environment variable
        private string sauceAccessKey = Environment.GetEnvironmentVariable(
            "SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);

        [TearDown]
        public void CleanUpAfterEveryTestMethod()
        {
            var isPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
            var sessionId = ((RemoteWebDriver)_driver).SessionId;
            var client = new RestClient
            {
                Authenticator = new HttpBasicAuthenticator(sauceUserName, sauceAccessKey),
                BaseUrl = new Uri(new SauceLabsEndpoint().SauceHubUrl)
            };
            var request = new RestRequest($"/{sauceUserName}/jobs/{sessionId}",
                Method.PUT)
            { RequestFormat = DataFormat.Json };
            request.AddJsonBody(new { passed = isPassed });
            client.Execute(request);
            _driver?.Quit();
        }

        [Test]
        public void RestApiTest()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddAdditionalCapability(CapabilityType.Version, "latest", true);
            options.AddAdditionalCapability(CapabilityType.Platform, "Windows 10", true);
            options.AddAdditionalCapability("username", sauceUserName, true);
            options.AddAdditionalCapability("accessKey", sauceAccessKey, true);
            options.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name, true);

            _driver = new RemoteWebDriver(new Uri("https://ondemand.saucelabs.com/wd/hub"), options.ToCapabilities(),
                TimeSpan.FromSeconds(600));
            _driver.Navigate().GoToUrl("https://www.google.com");
            Assert.Pass();
        }
    }
}
