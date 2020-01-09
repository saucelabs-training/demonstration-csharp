using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;

namespace Appium4.NUnit.Framework.RealDevices.Browser
{
    [TestFixture]
    public class MobileBrowserIOSTests : BaseMobileTest
    {
        [Test]
        public void MobileBrowser_iOS12_ShouldPass()
        {
            BrowserCapabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "12.4");
            BrowserCapabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");
            Driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), BrowserCapabilities);
            Driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(Driver.Url.Contains("saucedemo.com"));
        }
        [Test]
        public void MobileBrowser_iOS13_2_ShouldPass()
        {
            BrowserCapabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "13.2");
            BrowserCapabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");
            Driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), BrowserCapabilities);
            Driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(Driver.Url.Contains("saucedemo.com"));
        }
        [Test]
        public void MobileBrowser_iOS11_4_1_ShouldPass()
        {
            BrowserCapabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "11.4.1");
            BrowserCapabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");
            Driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), BrowserCapabilities);
            Driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(Driver.Url.Contains("saucedemo.com"));
        }
    }
}
