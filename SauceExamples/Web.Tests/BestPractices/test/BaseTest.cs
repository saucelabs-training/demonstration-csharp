using Common;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Configuration;
using Common.SauceLabs;

namespace Web.Tests.BestPractices.test
{
    //TODO this whole class is a duplication of BaseCrossBrowserTEst.cs
    //The reason why it was duplicated was because I needed to be able to configure
    //the [Setup] methods. Some needed to be able to set the Build Name and do some actions,
    //other tests didn't need to set the build name, and others, only needed to set
    //the build name. It seems as though maybe a Strategy pattern might solve these problems
    //It might make sense to create a ISetupStrategy that is defined in the constructor
    //of every single feature file. That feature file will define the setup Strategy.
    //Then, those operations will be performed int the [Setup] of the BaseTest
    [TestFixture]
    [Category("AcceptanceTests")]
    [Category("CrossBrowser")]
    [Category("NUnit")]
    [Category("BestPractices")]
    public class BaseTest
    {
        private readonly string _browser;
        private readonly string _browserVersion;
        private readonly string _osPlatform;
        private SauceSession _sauce;

        [SetUp]
        public void ExecuteBeforeEveryTestMethod()
        {
            var options = new SauceOptions
            {
                IsExtendedDebuggingEnabled = bool.Parse(ConfigurationManager.AppSettings["isExtendedDebuggingEnabled"]),
                Browser = _browser,
                BrowserVersion = _browserVersion,
                OperatingSystem = _osPlatform
            };
            _sauce = new SauceSession(options);
            _sauce.DataCenter = DataCenter.USEast;  //TODO this will mean that it's headless
            Driver = _sauce.Start();
            _sauce.TestName = TestContext.CurrentContext.Test.Name;
            _sauce.BuildName = ConfigurationManager.AppSettings["buildName"];   //set by default, no need to explicitly state
        }

        [TearDown]
        public void CleanUpAfterEveryTestMethod()
        {
            if (_sauce != null) ExecuteSauceCleanupSteps();
            Driver?.Quit();
        }

        private void ExecuteSauceCleanupSteps()
        {
            var isPassed = TestContext.CurrentContext.Result.Outcome.Status
                == TestStatus.Passed;
            //TODO could also log a comment "Test finished execution"
            //TODO will also log the error message if it failed. In the future can take the
            //whole TestContext and parse out the relevant data
            _sauce.Stop(isPassed, TestContext.CurrentContext.Result.Message);
        }

        private void SetTestStatusUsingApi(bool isPassed)
        {
            var userName = SauceUser.Name;
            var accessKey = SauceUser.AccessKey;

            var sessionId = ((RemoteWebDriver)Driver).SessionId;
            var client = new RestClient
            {
                Authenticator = new HttpBasicAuthenticator(userName, accessKey),
                BaseUrl = new Uri(new SauceLabsEndpoint().HeadlessRestApiUrl)
            };
            var request = new RestRequest($"/{userName}/jobs/{sessionId}",
                Method.PUT)
            { RequestFormat = DataFormat.Json };
            request.AddJsonBody(new { passed = isPassed });
            client.Execute(request);
        }



        public BaseTest(string browser, string browserVersion, string osPlatform)
        {
            _browser = browser;
            _browserVersion = browserVersion;
            _osPlatform = osPlatform;
        }

        public IWebDriver Driver { get; set; }
    }
}