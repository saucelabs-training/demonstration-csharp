using System;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using RestSharp;

namespace Mobile.Nunit.Tests
{
    [TestFixture]
    public class MobileBrowserAndroidTests : BaseMobileTest
    {
        [Test]
        public void MobileBrowser_Android_ShouldPass()
        {
            DesiredCapabilities caps = new DesiredCapabilities();
            //this is the API key that you get from your app in Test Object
            caps.SetCapability("testobject_api_key", SauceDemoMobileBrowserAppApiKey);
            caps.SetCapability("deviceOrientation", "portrait");
            caps.SetCapability("browserName", "chrome");
            caps.SetCapability("platformVersion", "8.1");
            caps.SetCapability("platformName", "Android");
            caps.SetCapability("name", TestContext.CurrentContext.Test.Name);
            caps.SetCapability("newCommandTimeout", 90);
            _driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), caps, 
                TimeSpan.FromSeconds(600));
            _driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(_driver.Url.Contains("saucedemo.com"));
        }
        [Test]
        public void MobileBrowser_Android9_ShouldPass()
        {
            DesiredCapabilities caps = new DesiredCapabilities();
            //this is the API key that you get from your app in Test Object
            caps.SetCapability("testobject_api_key", SauceDemoMobileBrowserAppApiKey);
            caps.SetCapability("deviceOrientation", "portrait");
            caps.SetCapability("browserName", "chrome");
            caps.SetCapability("platformVersion", "9");
            caps.SetCapability("platformName", "Android");
            caps.SetCapability("name", TestContext.CurrentContext.Test.Name);
            caps.SetCapability("newCommandTimeout", 90);
            _driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), caps,
                TimeSpan.FromSeconds(600));
            _driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(_driver.Url.Contains("saucedemo.com"));
        }
        [Test]
        public void MobileBrowser_Android10_ShouldPass()
        {
            DesiredCapabilities caps = new DesiredCapabilities();
            //this is the API key that you get from your app in Test Object
            caps.SetCapability("testobject_api_key", SauceDemoMobileBrowserAppApiKey);
            caps.SetCapability("deviceOrientation", "portrait");
            caps.SetCapability("browserName", "chrome");
            caps.SetCapability("platformVersion", "10");
            caps.SetCapability("platformName", "Android");
            caps.SetCapability("name", TestContext.CurrentContext.Test.Name);
            caps.SetCapability("newCommandTimeout", 90);
            _driver = new RemoteWebDriver(new Uri(RdcServerUrlUs), caps,
                TimeSpan.FromSeconds(600));
            _driver.Navigate().GoToUrl("https://www.saucedemo.com");
            Assert.IsTrue(_driver.Url.Contains("saucedemo.com"));
        }
    }
}
