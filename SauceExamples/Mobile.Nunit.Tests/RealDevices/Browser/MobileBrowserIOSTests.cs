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
            browserCapabilities.SetCapability("platformVersion", "12.4");
            browserCapabilities.SetCapability("platformName", "iOS");
            _driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), browserCapabilities,
                TimeSpan.FromSeconds(600));
            _driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(_driver.Url.Contains("saucedemo.com"));
        }
        [Test]
        public void MobileBrowser_iOS13_2_ShouldPass()
        {
            browserCapabilities.SetCapability("platformVersion", "13.2");
            browserCapabilities.SetCapability("platformName", "iOS");
            _driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), browserCapabilities,
                TimeSpan.FromSeconds(600));
            _driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(_driver.Url.Contains("saucedemo.com"));
        }
        [Test]
        public void MobileBrowser_iOS11_4_1_ShouldPass()
        {
            browserCapabilities.SetCapability("platformVersion", "11.4.1");
            browserCapabilities.SetCapability("platformName", "iOS");
            _driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), browserCapabilities,
                TimeSpan.FromSeconds(600));
            _driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(_driver.Url.Contains("saucedemo.com"));
        }
    }
}
