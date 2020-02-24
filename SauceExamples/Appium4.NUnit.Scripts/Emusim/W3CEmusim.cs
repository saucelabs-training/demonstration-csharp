using System;
using System.Collections.Generic;
using System.Reflection;
using Common;
using Common.SauceLabs;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using Assert = NUnit.Framework.Assert;

namespace Appium3.MsTest.Scripts.Emusim
{
    [TestFixture]
    public class Emusim
    {
        private AppiumOptions _browserCapabilities;
        private SessionId _sessionId;


        private RemoteWebDriver _driver;
        private object _sauceUserName;
        private object _sauceAccessKey;
        private Dictionary<string, object> sauceOptions;

        [SetUp]
        public void Setup()
        {
            _sauceUserName = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            _sauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
            sauceOptions = new Dictionary<string, object>
            {
                ["username"] = _sauceUserName,
                ["accessKey"] = _sauceAccessKey,
                ["deviceName"] = "iPhone XS Max Simulator",
                ["platformVersion"] = "13.0",
                ["appiumVersion"] = "1.15.0"
            };
            var options = new SafariOptions
            {
                BrowserVersion = "latest",
                PlatformName = "iOS"
            };
            sauceOptions.Add("name", MethodBase.GetCurrentMethod().Name);
            options.AddAdditionalCapability("sauce:options", sauceOptions);

            _driver = new RemoteWebDriver(new Uri("https://ondemand.saucelabs.com/wd/hub"),
                options.ToCapabilities(), TimeSpan.FromSeconds(30));
        }
        [TearDown]
        public void Teardown()
        {
            if (_driver == null) return;

            _sessionId = _driver.SessionId;
            _driver.Quit();
            var isTestPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
            new SimpleSauce().Rdc.UpdateTestStatus(isTestPassed, _sessionId);
        }


        [Test]
        [Ignore("not currently working")]
        public void AndroidOnEmusimUsingW3C()
        {
            GoToThenAssert();
        }
        private void GoToThenAssert()
        {
            _driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(_driver.Url.Contains("saucedemo.com"));
        }
    }
}
