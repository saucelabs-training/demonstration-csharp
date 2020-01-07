using System;
using System.Reflection;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Appium4.NUnit.Scripts.RealDevices.NativeApp
{
    [TestClass]
    public class AndroidGetStarted
    {
        private static string RdcUsHubUrl => "https://us1.appium.testobject.com/wd/hub";
        private IWebDriver _driver;

        [TestMethod]
        [TestCategory("Android")]
        [TestCategory("SimpleTest")]
        [TestCategory("Rdc")]
        [TestCategory("NativeApp")]
        [TestCategory("Appium4NUnitScripts")]


        public void ShouldOpenApp()
        {
            var capabilities = new DesiredCapabilities();
            //We can run on any version of the platform as long as it's the correct device
            //Make sure to pick an Android or iOS device based on your app
            capabilities.SetCapability("deviceName", "Google Pixel");
            capabilities.SetCapability("platformName", "Android");

            /*
             * !!!!!!
             * TODO first you must upload an app to RDC so that you get your app key
             * Then, make sure you can hardcode it here just to get started
             */
            capabilities.SetCapability("testobject_api_key", new ApiKeys().Rdc.Apps.SampleAppAndroid);
            capabilities.SetCapability("name", MethodBase.GetCurrentMethod().Name);
            capabilities.SetCapability("newCommandTimeout", 90);

            _driver = new RemoteWebDriver(new Uri(RdcUsHubUrl), capabilities,
                TimeSpan.FromSeconds(300));
            var size = _driver.Manage().Window.Size;
            Assert.AreNotEqual(0, size.Height);
            
            //Always making sure to end the session at the end of any test
            _driver?.Quit();
        }
    }
}
