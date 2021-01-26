using System;
using Common.SauceLabs;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;

namespace Appium4.NUnit.Scripts.Emusim.NativeApp
{
    [TestFixture]
    [Parallelizable]
    public class GetStartedIos
    {
        private IOSDriver<IOSElement> _driver;

        //Never forget to pass the test status to Sauce Labs
        [TearDown]
        public void Teardown()
        {
            if (_driver == null) return;

            var isTestPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
            ((IJavaScriptExecutor)_driver).ExecuteScript("sauce:job-result=" + (isTestPassed ? "passed" : "failed"));
            _driver.Quit();
        }
        [Test]
        [Category("SimpleTest")]
        [Category("NativeApp")]

        public void ShouldOpenNativeIosApp()
        {
            var sauceUser = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            var sauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
            var uri = $"https://{sauceUser}:{sauceAccessKey}@{SauceLabsEndpoint.SauceUsWestDomain}";

            var capabilities = new AppiumOptions();
            //We can run on any version of the platform as long as it's the correct device
            //Make sure to pick an Android or iOS device based on your app
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "iPhone XS Max Simulator");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "14.0");
            capabilities.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);

            /*
             * You need to upload your own Native Mobile App to Sauce Storage!
             * https://wiki.saucelabs.com/display/DOCS/Uploading+your+Application+to+Sauce+Storage
             * You can use either storage:<app-id> or storage:filename=
             * iOS Simulator has to be a .zip
             */
            capabilities.AddAdditionalCapability("app",
                $"storage:d696db1c-ae9a-4b9b-813a-2e5f90177ca8");

            //60 seconds for the connection timeout
            _driver = new IOSDriver<IOSElement>(new Uri(uri), capabilities);
            var windowHeight = int.Parse(_driver.Manage().Window.Size.Height.ToString());
            Assert.Greater(windowHeight, 0);

        }


    }
}
