using System;
using System.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Selenium.Nunit.Scripts.SimpleExamples
{
    [TestFixture]
    [Category("SimpleTest")]
    public class SimpleSauceTest
    {
        private IWebDriver _driver;
        private SauceSession _sauce;

        [Test]
        public void DemoTest()
        {
            var options = new SauceOptions
            {
                IsExtendedDebuggingEnabled = bool.Parse(ConfigurationManager.AppSettings["isExtendedDebuggingEnabled"])
            };
            _sauce = new SauceSession(options)
            {
                DataCenter = DataCenter.USEast, //TODO this will mean that it's headless
                TestName = TestContext.CurrentContext.Test.Name
            };
            _driver = _sauce.Start();

            _driver.Navigate().GoToUrl("https://www.google.com");
            Assert.Pass();
        }

        [TearDown]
        public void CleanUpAfterEveryTestMethod()
        {
            //TODO could also log a comment "Test finished execution"
            //TODO will also log the error message if it failed. In the future can take the
            //whole TestContext and parse out the relevant data
            var isPassed = TestContext.CurrentContext.Result.Outcome.Status
                           == TestStatus.Passed;
            _sauce.Stop(isPassed, TestContext.CurrentContext.Result.Message);
        }
    }
}
