using System;
using System.Configuration;
using Common;
using Common.SauceLabs;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using RestSharp;
using RestSharp.Authenticators;

namespace Selenium3.Nunit.Framework.Antipatterns.Parallelization
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
    public class WorkingBaseTest
    {
        private readonly string _browser;
        private readonly string _browserVersion;
        private readonly string _osPlatform;
        public SauceJavaScriptExecutor SauceReporter;
        private SauceLabsCapabilities SauceConfig { get; set; }

        public WorkingBaseTest(string browser, string browserVersion, string osPlatform)
        {
            _browser = browser;
            _browserVersion = browserVersion;
            _osPlatform = osPlatform;
        }

        [SetUp]
        public void ExecuteBeforeEveryTestMethod()
        {
            SauceConfig = new SauceLabsCapabilities
            {
                IsDebuggingEnabled = bool.Parse(ConfigurationManager.AppSettings["isExtendedDebuggingEnabled"]),
                IsHeadless = false
            };
            SauceLabsCapabilities.BuildName = ConfigurationManager.AppSettings["buildName"];

            Driver = new WebDriverFactory(SauceConfig).CreateSauceDriver(_browser, _browserVersion, _osPlatform);
            SauceReporter = new SauceJavaScriptExecutor(Driver);
            SauceReporter.SetTestName(TestContext.CurrentContext.Test.Name);
            SauceReporter.SetBuildName("ParallelizationWithoutStatic");
        }

        [TearDown]
        public void CleanUpAfterEveryTestMethod()
        {
            if (SauceConfig.IsUsingSauceLabs) ExecuteSauceCleanupSteps();
            Driver?.Quit();
        }

        private void ExecuteSauceCleanupSteps()
        {
            var isPassed = TestContext.CurrentContext.Result.Outcome.Status
                == TestStatus.Passed;
            SauceReporter.LogTestStatus(isPassed);
            //SetTestStatusUsingApi(isPassed);
            SauceReporter.LogMessage("Test finished execution");
            SauceReporter.LogMessage(TestContext.CurrentContext.Result.Message);
        }



        public IWebDriver Driver { get; set; }
    }
}