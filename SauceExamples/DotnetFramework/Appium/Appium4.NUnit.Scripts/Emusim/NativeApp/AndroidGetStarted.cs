using System;
using Common.SauceLabs;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace Appium4.NUnit.Scripts.Emusim.NativeApp
{
    [TestFixture]
    [Parallelizable]

    public class AndroidGetStarted
    {
        private AndroidDriver<AndroidElement> _driver;

        [Test]
        [Category("Android")]
        [Category("SimpleTest")]

        public void ShouldOpenNativeAndroidApp()
        {
            //It's a best practice to store credentials in environment variables
            var sauceUser = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            var sauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
            var uri = $"https://{sauceUser}:{sauceAccessKey}@{SauceLabsEndpoint.SauceUsWestDomain}";

            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Google Pixel 3a XL GoogleAPI Emulator");
            capabilities.AddAdditionalCapability(MobileCapabilityType.AppiumVersion, "1.18.1");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "11.0");

            capabilities.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);
            capabilities.AddAdditionalCapability("newCommandTimeout", 90);

            /*
             * You need to upload your own Native Mobile App to Sauce Storage!
             * https://wiki.saucelabs.com/display/DOCS/Uploading+your+Application+to+Sauce+Storage
             * You can use either storage:<app-id> or storage:filename=
             */
            capabilities.AddAdditionalCapability("app",
                "storage:filename=Android.SauceLabs.Mobile.Sample.app.2.7.0.apk");

            //60 seconds default for the connection timeout
            _driver = new AndroidDriver<AndroidElement>(new Uri(uri), capabilities);
            var size = _driver.Manage().Window.Size;
            Assert.AreNotEqual(0, size.Height);
        }

        //Never forget to pass the test status to Sauce Labs
        [TearDown]
        public void Teardown()
        {
            if (_driver == null) return;

            var isTestPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
            ((IJavaScriptExecutor)_driver).ExecuteScript("sauce:job-result=" + (isTestPassed ? "passed" : "failed"));
            _driver.Quit();
        }
    }
}
