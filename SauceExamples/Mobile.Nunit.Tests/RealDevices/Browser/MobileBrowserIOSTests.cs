using System;
using NUnit.Framework;
using OpenQA.Selenium.Remote;

namespace Appium4.NUnit.Framework.RealDevices.Browser
{
    [TestFixture]
    public class MobileBrowserIOSTests : BaseMobileTest
    {
        [Test]
        public void MobileBrowser_iOS12_ShouldPass()
        {
            BrowserCapabilities.AddAdditionalCapability("platformVersion", "12.4");
            BrowserCapabilities.AddAdditionalCapability("platformName", "iOS");
            Driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), BrowserCapabilities);
            Driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(Driver.Url.Contains("saucedemo.com"));
        }
        [Test]
        public void MobileBrowser_iOS13_2_ShouldPass()
        {
            BrowserCapabilities.AddAdditionalCapability("platformVersion", "13.2");
            BrowserCapabilities.AddAdditionalCapability("platformName", "iOS");
            Driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), BrowserCapabilities);
            Driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(Driver.Url.Contains("saucedemo.com"));
        }
        [Test]
        public void MobileBrowser_iOS11_4_1_ShouldPass()
        {
            BrowserCapabilities.AddAdditionalCapability("platformVersion", "11.4.1");
            BrowserCapabilities.AddAdditionalCapability("platformName", "iOS");
            Driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), BrowserCapabilities);
            Driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(Driver.Url.Contains("saucedemo.com"));
        }
    }
}
