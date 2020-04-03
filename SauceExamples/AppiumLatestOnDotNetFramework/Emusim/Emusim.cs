using System.Reflection;
using Common.SauceLabs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;

namespace Appium3.MsTest.Scripts.Emusim
{
    [TestClass]
    [TestCategory("MsTest")]
    [TestCategory("EmusimAPI")]
    [TestCategory("Android")]

    public class Emusim
    {
        private AndroidDriver<IWebElement> _driver;
        private SessionId _sessionId;
        public TestContext TestContext { get; set; }

        /*
         * Never forget to have a cleanup hook at the end of your automation.
         * Always make sure that you are setting the test status in Sauce Labs
         *
         */
        [TestCleanup]
        public void Cleanup()
        {
            if (_driver == null) return;
            var isPassed = TestContext.CurrentTestOutcome == UnitTestOutcome.Passed;
            //API Docs: https://wiki.saucelabs.com/display/DOCS/Job+Methods#JobMethods-UpdateJob
            new SimpleSauce().EmuSim.UpdateTestStatus(isPassed, _sessionId);
            _driver.Quit();
        }
        [TestMethod]
        public void SimpleAndroidEmusimTest()
        {
            var capabilities = new DesiredCapabilities();
            capabilities.SetCapability("appiumVersion", "1.9.1");
            capabilities.SetCapability("deviceName", "Android Emulator");
            capabilities.SetCapability("deviceOrientation", "portrait");
            capabilities.SetCapability("browserName", "Chrome");
            capabilities.SetCapability("platformVersion", "8.0");
            capabilities.SetCapability("platformName", "Android");
            capabilities.SetCapability("name", MethodBase.GetCurrentMethod().Name);
            capabilities.SetCapability("newCommandTimeout", 90);
            capabilities.SetCapability("username", SauceUser.Name);
            capabilities.SetCapability("accessKey", SauceUser.AccessKey);


            _driver = new AndroidDriver<IWebElement>(new SauceLabsEndpoint().SauceHubUri, capabilities);
            _sessionId = _driver.SessionId;
            _driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.AreEqual("https://www.saucedemo.com/", _driver.Url);
        }

    }
}
