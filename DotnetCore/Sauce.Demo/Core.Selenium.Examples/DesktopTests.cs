using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;

namespace Core.Selenium.Examples
{
    [TestClass]
    public class DesktopTests
    {
        private IWebDriver _driver;
        private string _sauceUserName;
        private string _sauceAccessKey;
        private Dictionary<string, object> _sauceOptions;
        public TestContext TestContext { get; set; }

        [TestCleanup]
        public void CleanUpAfterEveryTestMethod()
        {
            if (_driver == null) return;

            var passed = TestContext.CurrentTestOutcome == UnitTestOutcome.Passed;
            ((IJavaScriptExecutor)_driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
            _driver.Quit();
        }

        [TestMethod]
        public void EdgeW3C()
        {
            //TODO please supply your Sauce Labs user name in an environment variable
            _sauceUserName = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            //TODO please supply your own Sauce Labs access Key in an environment variable
            _sauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
            _sauceOptions = new Dictionary<string, object>
            {
                ["username"] = _sauceUserName,
                ["accessKey"] = _sauceAccessKey,
                ["name"] = TestContext.TestName
            };

            var browserOptions = new EdgeOptions
            {
                BrowserVersion = "latest",
                PlatformName = "Windows 10"
                //AcceptInsecureCertificates = true //Insecure Certs are Not supported by Edge
            };

            browserOptions.AddAdditionalCapability("sauce:options", _sauceOptions);

            _driver = new RemoteWebDriver(new Uri("https://ondemand.saucelabs.com/wd/hub"), browserOptions.ToCapabilities(),
                TimeSpan.FromSeconds(30));
            _driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(_driver.Url.Contains("saucedemo.com"));
        }
    }
}
