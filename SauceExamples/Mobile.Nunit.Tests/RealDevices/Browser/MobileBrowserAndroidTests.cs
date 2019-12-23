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
            browserCapabilities.SetCapability("platformVersion", "8.1");
            browserCapabilities.SetCapability("platformName", "Android");
            _driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), browserCapabilities,
                TimeSpan.FromSeconds(600));
            _driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(_driver.Url.Contains("saucedemo.com"));
        }
        [Test]
        public void MobileBrowser_Android9_ShouldPass()
        {
            browserCapabilities.SetCapability("platformVersion", "9");
            browserCapabilities.SetCapability("platformName", "Android");
            _driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), browserCapabilities,
                TimeSpan.FromSeconds(600));
            _driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(_driver.Url.Contains("saucedemo.com"));
        }
        [Test]
        public void MobileBrowser_Android10_ShouldPass()
        {
            browserCapabilities.SetCapability("platformVersion", "10");
            browserCapabilities.SetCapability("platformName", "Android");
            _driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), browserCapabilities,
                TimeSpan.FromSeconds(600));
            _driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(_driver.Url.Contains("saucedemo.com"));
        }
    }
}
