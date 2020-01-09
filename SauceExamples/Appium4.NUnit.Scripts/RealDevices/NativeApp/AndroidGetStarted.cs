using System;
using System.Reflection;
using Common;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;

namespace Appium4.NUnit.Scripts.RealDevices.NativeApp
{
    [TestFixture]
    public class AndroidGetStarted
    {
        private static string RdcUsHubUrl => "https://us1.appium.testobject.com/wd/hub";
        private AndroidDriver<AndroidElement> _driver;

        [Test]
        [Category("Android")]
        [Category("SimpleTest")]
        [Category("Rdc")]
        [Category("NativeApp")]
        [Category("Appium4NUnitScripts")]


        public void ShouldOpenApp()
        {
            var capabilities = new AppiumOptions();
            //We can run on any version of the platform as long as it's the correct device
            //Make sure to pick an Android or iOS device based on your app
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Google Pixel");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");

            /*
             * !!!!!!
             * TODO first you must upload an app to RDC so that you get your app key
             * Then, make sure you can hardcode it here just to get started
             */
            capabilities.AddAdditionalCapability("testobject_api_key", new ApiKeys().Rdc.Apps.SampleAppAndroid);
            capabilities.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);
            capabilities.AddAdditionalCapability("newCommandTimeout", 90);

            //60 seconds for the connection timeout
            _driver = new AndroidDriver<AndroidElement>(new Uri(RdcUsHubUrl), capabilities);
            var size = _driver.Manage().Window.Size;
            Assert.AreNotEqual(0, size.Height);
            
            //Always making sure to end the session at the end of any test
            _driver?.Quit();
        }
    }
}
