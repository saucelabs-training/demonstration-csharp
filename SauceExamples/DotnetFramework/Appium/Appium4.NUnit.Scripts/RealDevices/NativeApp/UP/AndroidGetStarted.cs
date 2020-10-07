using System;
using Common.SauceLabs;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace Appium4.NUnit.Scripts.RealDevices.NativeApp.UP
{
    [TestFixture]
    [Parallelizable]

    public class AndroidGetStarted
    {
        private static string HubUrl => "ondemand.us-west-1.saucelabs.com/wd/hub";
        private AndroidDriver<AndroidElement> _driver;

        [Test]
        [Category("Android")]
        [Category("SimpleTest")]
        [Category("Rdc")]
        [Category("NativeApp")]
        [Category("Appium4NUnitScripts")]

        public void ShouldOpenNativeAndroidApp()
        {
            var capabilities = new AppiumOptions();
            //We can run on any version of the platform as long as it's the correct device
            //Make sure to pick an Android or iOS device based on your app
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Google Pixel 4");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            capabilities.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);
            capabilities.AddAdditionalCapability("newCommandTimeout", 90);

            /*
             * You need to upload your own Native Mobile App to Sauce Storage!
             * https://wiki.saucelabs.com/display/DOCS/Uploading+your+Application+to+Sauce+Storage
             * You can use either storage:<app-id> or storage:filename=
             */
            capabilities.AddAdditionalCapability("app",
                "storage:filename=Android.SauceLabs.Mobile.Sample.app.2.5.0.apk");
            var sauceUser = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            var sauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
            var uri = $"https://{sauceUser}:{sauceAccessKey}@{HubUrl}";
            //60 seconds for the connection timeout
            _driver = new AndroidDriver<AndroidElement>(new Uri(uri), capabilities);
            var size = _driver.Manage().Window.Size;
            Assert.AreNotEqual(0, size.Height);

            //Always making sure to end the session at the end of any test
            _driver?.Quit();
        }
    }
}
