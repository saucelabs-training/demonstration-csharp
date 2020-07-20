using System;
using Common.SauceLabs;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;

namespace Appium4.NUnit.Scripts.RealDevices.NativeApp.LegacyRdc
{
    [TestFixture]
    public class IOsGetStarted
    {
        private static string RdcUsHubUrl => "https://us1.appium.testobject.com/wd/hub";
        private IOSDriver<IOSElement> _driver;

        [Test]
        [Category("Android")]
        [Category("SimpleTest")]
        [Category("Rdc")]
        [Category("NativeApp")]
        [Category("Appium4NUnitScripts")]

        public void ShouldOpenNativeIOSApp()
        {
            var capabilities = new AppiumOptions();
            //We can run on any version of the platform as long as it's the correct device
            //Make sure to pick an Android or iOS device based on your app
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "iPhone X");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");

            /*
             * !!!!!!
             * TODO first you must upload an app to RDC so that you get your app key
             * Then, make sure you can hardcode it here just to get started
             */
            capabilities.AddAdditionalCapability("testobject_api_key", new ApiKeys().Rdc.Apps.SampleAppIOS);
            capabilities.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);
            capabilities.AddAdditionalCapability("newCommandTimeout", 90);
            //TODO it's a best practice to set the appium version so that you're always getting the latest
            capabilities.AddAdditionalCapability("appiumVersion", "1.16.0");


            //60 seconds for the connection timeout
            _driver = new IOSDriver<IOSElement>(new Uri(RdcUsHubUrl), capabilities);
            var size = int.Parse(_driver.Manage().Window.Size.Height.ToString());
            Assert.Greater(size, 0);

            //Always making sure to end the session at the end of any test
            _driver?.Quit();
        }
    }
}
