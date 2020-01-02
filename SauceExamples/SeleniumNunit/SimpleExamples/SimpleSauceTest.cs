using System;
using System.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Simple.Sauce;

namespace Selenium3.Nunit.Scripts.SimpleExamples
{
    [TestFixture]
    [Category("SimpleTest")]
    public class SimpleSauceTest
    {
        private IWebDriver _driver;
        private SauceSession _sauce;

        [Test]
        public void DemoSimpleSauce()
        {
            _sauce = new SauceSession
            {
                DataCenter = DataCenter.UsWest, //TODO this will mean that it's headless
                //TestName = TestContext.CurrentContext.Test.Name
            };
            _driver = _sauce.Start();

            _driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.That(_driver.Title, Is.EqualTo("Swag Labs"));
        }

        [TearDown]
        public void CleanUpAfterEveryTestMethod()
        {
            _driver.Quit();
            //TODO could also log a comment "Test finished execution"
            //TODO will also log the error message if it failed. In the future can take the
            //whole TestContext and parse out the relevant data
            var isPassed = TestContext.CurrentContext.Result.Outcome.Status
                           == TestStatus.Passed;
            //_sauce.Stop(isPassed, TestContext.CurrentContext.Result.Message);
        }
    }
}
