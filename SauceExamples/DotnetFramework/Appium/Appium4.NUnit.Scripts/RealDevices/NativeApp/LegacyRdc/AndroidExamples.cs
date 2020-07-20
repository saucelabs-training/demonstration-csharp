using System;
using Common.SauceLabs;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Appium4.NUnit.Scripts.RealDevices.NativeApp.LegacyRdc
{
    [TestFixture]
    [Parallelizable]
    public class AndroidExamples
    {
        /*
         * This is the very basic setup that you should have for your test automation.
         * This include a setup and teardown, Quit(), and results update.
         * Ultimately, you should always use the POM that can be found here:
         * Selenium.Nunit.Framework.BestPractices.test
         */
        private static string RdcUsHubUrl => "https://us1.appium.testobject.com/wd/hub";
        private AppiumDriver<AndroidElement> _driver;
        private SessionId _sessionId;

        [SetUp]
        public void Setup()
        {
            var capabilities = new AppiumOptions();
            //We can run on any version of the platform as long as it's the correct device
            //Make sure to pick an Android or iOS device based on your app
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Google Pixel 4");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            //make sure you set locale as sometimes it opens in a different location and throws off locations
            capabilities.AddAdditionalCapability("locale", "en");
            capabilities.AddAdditionalCapability("language", "en");
            //The next major version of Appium (2.x) will **require** this capability
            capabilities.AddAdditionalCapability("automationName", "UiAutomator2");
            //It's a good practice to set an appWaitActivity so that the automation knows when the app is loaded
            capabilities.AddAdditionalCapability("appWaitActivity", "com.swaglabsmobileapp.MainActivity");
            //It's a good practice to use the latest appium version
            capabilities.AddAdditionalCapability("appiumVersion", "1.17.1");
            /*
             * !!!!!!
             * TODO first you must upload an app to RDC so that you get your app key
             * Then, make sure you can hardcode it here just to get started
             */
            capabilities.AddAdditionalCapability("testobject_api_key", new ApiKeys().Rdc.Apps.SampleAppAndroid);
            capabilities.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);
            //It's important to keep the newCommandTimeout on the higher end as Real Devices are slow
            capabilities.AddAdditionalCapability("newCommandTimeout", 180);

            /*
             Getting Error: OpenQA.Selenium.WebDriverException : The HTTP request to the remote WebDriver server for URL 
            https://us1.appium.testobject.com/wd/hub/session timed out after 60 seconds.
                ----> System.Net.WebException : The operation has timed out

            Solution: Try changing to a more popular device
             */
            _driver = new AndroidDriver<AndroidElement>(new Uri(RdcUsHubUrl), capabilities);
        }
        [TearDown]
        public void Teardown()
        {
            if (_driver == null) return;

            _sessionId = _driver.SessionId;
            _driver.Quit();
            //TODO fix this as it doesn't seem to update the status for failed tests
            var isTestPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
            new SimpleSauce().Rdc.UpdateTestStatus(isTestPassed, _sessionId);
        }

        [Test]
        [Category("Android")]
        [Category("SimpleTest")]
        [Category("Rdc")]
        [Category("NativeApp")]
        [Category("Appium4NUnitScripts")]

        public void ShouldOpenApp()
        {
            Assert.DoesNotThrow(() => LoginScreenIsVisible());
        }

        private void LoginScreenIsVisible()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementIsVisible(
                MobileBy.AccessibilityId("test-Username")));
        }

        [Test]
        [Category("Android")]
        [Category("SimpleTest")]
        [Category("Rdc")]
        [Category("NativeApp")]
        [Category("Appium4NUnitScripts")]
        [Retry(1)]

        public void ShouldLogin()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));

            var userName = wait.Until(ExpectedConditions.ElementIsVisible(
                MobileBy.AccessibilityId("test-Username")));
            userName.SendKeys("standard_user");

            var password = wait.Until(ExpectedConditions.ElementIsVisible(
                MobileBy.AccessibilityId("test-Password")));
            password.SendKeys("secret_sauce");

            var login = wait.Until(ExpectedConditions.ElementIsVisible(
                MobileBy.AccessibilityId("test-LOGIN")));
            login.Click();

            Assert.DoesNotThrow(() => GetCartElement(wait), "The cart element wasn't displayed");
        }

        private void GetCartElement(WebDriverWait wait)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(
                            By.XPath("//android.view.ViewGroup[@content-desc='test-Cart']")));
        }
    }
}
