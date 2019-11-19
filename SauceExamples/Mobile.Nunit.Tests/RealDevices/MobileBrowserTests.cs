using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;

namespace Mobile.Nunit.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class MobileBrowserTests
    {
        private SessionId _sessionId;
        private AndroidDriver<IWebElement> _driver;
        private static string RdcServerUrlUs => "https://us1.appium.testobject.com/wd/hub";

        private static string SauceDemoMobileBrowserAppApiKey =>
            Environment.GetEnvironmentVariable(
                "SAUCE_DEMO_MOBILE_WEB_RDC_API_KEY", EnvironmentVariableTarget.User);

        [Test]
        public void ShouldPass()
        {
            DesiredCapabilities caps = new DesiredCapabilities();
            //this is the API key that you get from your app in Test Object
            caps.SetCapability("testobject_api_key", SauceDemoMobileBrowserAppApiKey);
            caps.SetCapability("deviceOrientation", "portrait");
            caps.SetCapability("browserName", "chrome");
            caps.SetCapability("platformVersion", "8.1");
            caps.SetCapability("platformName", "Android");
            IWebDriver driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), caps, 
                TimeSpan.FromSeconds(600));
            driver.Navigate().GoToUrl("https://www.saucedemo.com");
            driver.Quit();
        }

    }
}
