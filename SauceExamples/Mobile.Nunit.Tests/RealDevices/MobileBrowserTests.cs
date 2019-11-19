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
        private static string USurl => "https://us1.appium.testobject.com/wd/hub";

        private static readonly string VodQANativeAppApiKey =
            Environment.GetEnvironmentVariable("VODQC_RDC_API_KEY", EnvironmentVariableTarget.User);

        [Test]
        public void ShouldPass()
        {
            DesiredCapabilities caps = new DesiredCapabilities();
            caps.SetCapability("appiumVersion", "1.9.1");
            //this is the API key that you get from your app in Test Object
            caps.SetCapability("testobject_api_key", "8D17FF69B0004D35A9142322142DCA73");
            caps.SetCapability("deviceOrientation", "portrait");
            caps.SetCapability("browserName", "chrome");
            caps.SetCapability("platformVersion", "8.1");
            caps.SetCapability("platformName", "Android");
            IWebDriver driver = new RemoteWebDriver(new Uri(USurl), caps, 
                TimeSpan.FromSeconds(600));
            driver.Navigate().GoToUrl("https://www.saucedemo.com");
            driver.Quit();
        }

    }
}
