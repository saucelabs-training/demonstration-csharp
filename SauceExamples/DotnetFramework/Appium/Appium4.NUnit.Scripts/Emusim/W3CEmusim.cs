using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using Assert = NUnit.Framework.Assert;

namespace Appium4.NUnit.Scripts.Emusim
{
    [TestFixture]
    public class W3CEmusim
    {
        private RemoteWebDriver _driver;
        private object _sauceUserName;
        private object _sauceAccessKey;
        private Dictionary<string, object> _sauceOptions;

        [SetUp]
        public void Setup()
        {
            _sauceUserName = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            _sauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
            _sauceOptions = new Dictionary<string, object>
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
            _sauceOptions.Add("name", MethodBase.GetCurrentMethod().Name);
            options.AddAdditionalOption("sauce:options", _sauceOptions);

            _driver = new RemoteWebDriver(new Uri("https://ondemand.saucelabs.com/wd/hub"),
                options.ToCapabilities(), TimeSpan.FromSeconds(30));
        }
        [TearDown]
        public void Teardown()
        {
            _driver?.Quit();
        }


        [Test]
        [Ignore("OpenQA.Selenium.WebDriverException : Unexpected error.")]
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
