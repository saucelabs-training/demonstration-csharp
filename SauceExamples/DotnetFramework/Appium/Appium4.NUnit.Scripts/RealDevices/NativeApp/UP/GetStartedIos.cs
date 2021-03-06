﻿using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;

namespace Appium4.NUnit.Scripts.RealDevices.NativeApp.UP
{
    [TestFixture]
    [Parallelizable]
    public class GetStartedIos
    {
        private static string HubUrl => "ondemand.us-west-1.saucelabs.com/wd/hub";
        private IOSDriver<IOSElement> _driver;

        [Test]
        [Category("SimpleTest")]
        [Category("NativeApp")]

        public void ShouldOpenNativeIosApp()
        {
            var sauceUser = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            var sauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
            var uri = $"https://{sauceUser}:{sauceAccessKey}@{HubUrl}";

            var capabilities = new AppiumOptions();
            //We can run on any version of the platform as long as it's the correct device
            //Make sure to pick an Android or iOS device based on your app
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "iPhone 11 Pro Max");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");
            capabilities.AddAdditionalCapability(MobileCapabilityType.Language, "en");
            capabilities.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);

            /*
             * You need to upload your own Native Mobile App to Sauce Storage!
             * https://wiki.saucelabs.com/display/DOCS/Uploading+your+Application+to+Sauce+Storage
             * You can use either storage:<app-id> or storage:filename=
             */
            capabilities.AddAdditionalCapability("app", 
                "storage:filename=iOS.RealDevice.SauceLabs.Mobile.Sample.app.2.7.0.ipa");

            //60 seconds for the connection timeout
            _driver = new IOSDriver<IOSElement>(new Uri(uri), capabilities);
            var windowHeight = int.Parse(_driver.Manage().Window.Size.Height.ToString());
            Assert.Greater(windowHeight, 0);

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
