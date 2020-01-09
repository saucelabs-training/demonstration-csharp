using System;
using NUnit.Framework;
using OpenQA.Selenium.Remote;

namespace Appium4.NUnit.Framework.RealDevices.Browser
{
    [TestFixture]
    public class MobileBrowserAndroidTests : BaseMobileTest
    {
        [Test]
        public void MobileBrowser_Android8_ShouldPass()
        {
            BrowserCapabilities.AddAdditionalCapability("platformVersion", "8.1");
            BrowserCapabilities.AddAdditionalCapability("platformName", "Android");
            Driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), BrowserCapabilities);
            Driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(Driver.Url.Contains("saucedemo.com"));
        }
        [Test]
        public void MobileBrowser_Android9_ShouldPass()
        {
            BrowserCapabilities.AddAdditionalCapability("platformVersion", "9");
            BrowserCapabilities.AddAdditionalCapability("platformName", "Android");
            Driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), BrowserCapabilities);
            Driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(Driver.Url.Contains("saucedemo.com"));
        }
        [Test]
        public void MobileBrowser_Android10_ShouldPass()
        {
            BrowserCapabilities.AddAdditionalCapability("platformVersion", "10");
            BrowserCapabilities.AddAdditionalCapability("platformName", "Android");
            Driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), BrowserCapabilities);
            Driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(Driver.Url.Contains("saucedemo.com"));
        }
    }
}
