using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;

namespace Core.Selenium4.MsTest.Scripts
{
    [TestClass]
    public class UnitTest1
    {
        /*
         * How to execute parallel tests at the method level using MsTest
         *
         * Make sure that your AssemplyInfo.cs for the project has this property:
         * [assembly: Parallelize(Workers = 100, Scope = ExecutionScope.MethodLevel)]
         *
         * There are recommendations on the web to configure the .runsettings file,
         * but you do not need it to run in parallel.
         *
         * In this example, we can run many Selenium test methods in parallel without any issue
         */
        IWebDriver _driver;
        private string sauceUserName;
        private string sauceAccessKey;
        private Dictionary<string, object> sauceOptions;
        public TestContext TestContext { get; set; }
        [TestInitialize]
        public void Setup()
        {
            //TODO please supply your Sauce Labs user name in an environment variable
            sauceUserName = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            //TODO please supply your own Sauce Labs access Key in an environment variable
            sauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
            sauceOptions = new Dictionary<string, object>
            {
                ["username"] = sauceUserName,
                ["accessKey"] = sauceAccessKey
            };
            var chromeOptions = new ChromeOptions
            {
                BrowserVersion = "latest",
                PlatformName = "Windows 10"
            };
            sauceOptions.Add("name", TestContext.TestName);
            chromeOptions.AddAdditionalOption("sauce:options", sauceOptions);

            _driver = new RemoteWebDriver(new Uri("https://ondemand.saucelabs.com/wd/hub"),
                chromeOptions.ToCapabilities(), TimeSpan.FromSeconds(30));
        }
        [TestMethod]
        public void TestMethod1()
        {
            GoToThenAssert();
        }
        private void GoToThenAssert()
        {
            _driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(_driver.Url.Contains("saucedemo.com"));
        }
        [TestMethod]
        public void TestMethod2()
        {
            GoToThenAssert();
        }
        [TestMethod]
        public void TestMethod3()
        {
            GoToThenAssert();
        }
    }
}
