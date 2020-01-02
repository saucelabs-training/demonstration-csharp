using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using Simple.Sauce;

namespace Selenium.MsTest.Scripts
{
    [TestClass]
    [TestCategory("WebDriver 4 tests on Sauce")]
    public class Selenium3WithW3C
    {
        IWebDriver _driver;
        private string _sauceUserName;
        private string _sauceAccessKey;
        private Dictionary<string, object> sauceOptions;
        public TestContext TestContext { get; set; }
        private Uri SeleniumHub => new Uri("https://ondemand.saucelabs.com/wd/hub");


        [TestMethod]
        public void EdgeW3C()
        {
            var options = new EdgeOptions
            {
                BrowserVersion = "latest",
                PlatformName = "Windows 10"
                //AcceptInsecureCertificates = true //Insecure Certs are Not supported by Edge
            };

            sauceOptions.Add("name", MethodBase.GetCurrentMethod().Name);
            options.AddAdditionalCapability("sauce:options", sauceOptions);

            _driver = new RemoteWebDriver(SeleniumHub, options.ToCapabilities(),
                TimeSpan.FromSeconds(600));
            GoToThenAssert();
        }
        [TestMethod]
        public void IEW3C()
        {
            var options = new InternetExplorerOptions
            {
                BrowserVersion = "latest",
                PlatformName = "Windows 10"
                //AcceptInsecureCertificates = true //Insecure Certs are Not supported by Edge
            };

            sauceOptions.Add("name", MethodBase.GetCurrentMethod().Name);
            options.AddAdditionalCapability("sauce:options", sauceOptions);

            _driver = new RemoteWebDriver(SeleniumHub, options.ToCapabilities(),
                TimeSpan.FromSeconds(600));
            GoToThenAssert();
        }

        private void GoToThenAssert()
        {
            _driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(_driver.Url.Contains("saucedemo.com"));
        }

        [TestMethod]
        public void ChromeW3C()
        {
            var chromeOptions = new ChromeOptions
            {
                BrowserVersion = "latest",
                PlatformName = "Windows 10"
            };
            sauceOptions.Add("name", MethodBase.GetCurrentMethod().Name);
            chromeOptions.AddAdditionalCapability("sauce:options", sauceOptions);

            _driver = new RemoteWebDriver(SeleniumHub,
                chromeOptions.ToCapabilities(), TimeSpan.FromSeconds(600));
            GoToThenAssert();
        }
        [TestMethod]
        public void ChromeW3CSimpleSauce()
        {
            var sauce = new SauceSession();
            _driver = sauce.Start();
            GoToThenAssert();
        }
        [TestMethod]
        public void SafariW3C()
        {
            SafariOptions safariOptions = new SafariOptions
            {
                BrowserVersion = "12.0",
                PlatformName = "macOS 10.13"
                //AcceptInsecureCertificates = true Don't use this as Safari doesn't support Insecure certs
            };
            sauceOptions.Add("name", MethodBase.GetCurrentMethod().Name);
            safariOptions.AddAdditionalCapability("sauce:options", sauceOptions);

            _driver = new RemoteWebDriver(SeleniumHub,
                safariOptions.ToCapabilities(), TimeSpan.FromSeconds(600));
            GoToThenAssert();
        }
        [TestMethod]
        public void FirefoxW3C()
        {
            var browserOptions = new FirefoxOptions
            {
                BrowserVersion = "latest",
                PlatformName = "Windows 10"
            };
            sauceOptions.Add("name", MethodBase.GetCurrentMethod().Name);
            browserOptions.AddAdditionalCapability("sauce:options", sauceOptions);

            _driver = new RemoteWebDriver(SeleniumHub,
                browserOptions.ToCapabilities(), TimeSpan.FromSeconds(600));
            _driver.Navigate().GoToUrl("https://www.saucedemo.com");
            GoToThenAssert();
        }
        //[TestInitialize]
        //public void SetupTests()
        //{
        //    _sauceUserName = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
        //    _sauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
        //    sauceOptions = new Dictionary<string, object>
        //    {
        //        ["username"] = _sauceUserName,
        //        ["accessKey"] = _sauceAccessKey
        //    };
        //}
        //[TestCleanup]
        //public void CleanUpAfterEveryTestMethod()
        //{
        //    var passed = TestContext.CurrentTestOutcome == UnitTestOutcome.Passed;
        //    if (_driver == null) return;
            
        //    ((IJavaScriptExecutor) _driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
        //    _driver.Quit();
        //}
    }
}
