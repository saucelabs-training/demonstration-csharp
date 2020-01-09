using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;

namespace Appium4.NUnit.Framework.RealDevices.Browser
{
    [TestFixture]
    public class MobileBrowserAndroidTests : BaseMobileTest
    {
        [Test]
        public void MobileBrowser_Android8_ShouldPass()
        {
            BrowserCapabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "8.1");
            BrowserCapabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            Driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), BrowserCapabilities);
            Driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(Driver.Url.Contains("saucedemo.com"));
        }
        [Test]
        public void MobileBrowser_Android9_ShouldPass()
        {
            BrowserCapabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "9");
            BrowserCapabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            Driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), BrowserCapabilities);
            Driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(Driver.Url.Contains("saucedemo.com"));
        }
        [Test]
        public void MobileBrowser_Android10_ShouldPass()
        {
            BrowserCapabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "10");
            BrowserCapabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            Driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), BrowserCapabilities);
            Driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(Driver.Url.Contains("saucedemo.com"));
        }
    }
}
