using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.Target;
using OpenQA.Selenium.Remote;
using Simple.Sauce;

namespace Selenium3.Nunit.Scripts.OnboardingTests
{
    /*
     * These scripts are simply for demonstration purposes.
     * They should not be used as an example of good practices for how to do test automation.
     */
    [TestFixture]
    [Category("InstantSauceTest"), Category("NUnit"), Category("Instant")]
    [Parallelizable]
    public class InstantSauceTest4
    {
        private IWebDriver _driver;
        private readonly string _sauceUserName =
            Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
        private readonly string _sauceAccessKey =
            Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
        private IJavaScriptExecutor _javascriptExecutor;

        [Test]
        public void BestPractices()
        {
            /*
             * Commenting is one of the most powerful ways to debug your failed tests.
             * Using the sauce:context command below will allow you to place
             * comments inside of Sauce Labs logs that you can read and analyze.
             * Comment your important methods and your automation will drastically improve
             */
            _javascriptExecutor.ExecuteScript("sauce:context=Open SauceDemo.com");
            _driver.Navigate().GoToUrl("https://www.saucedemo.com");

            _javascriptExecutor.ExecuteScript("sauce:context=Sleep for 10000ms");
            Thread.Sleep(10000);
            Assert.IsTrue(true);
        }

        [SetUp]
        public void ExecuteBeforeEveryTest()
        {
            var sauceOptions = new SauceOptions();
            //sauceOptions.WithSafari(Browsers.Safari._11_1).WithPlatform(Platforms.MacOsMojave);
        }


        [TearDown]
        public void CleanUpAfterEveryTestMethod()
        {
            var passed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
            _javascriptExecutor.ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
            _driver?.Quit();
        }
    }
}
