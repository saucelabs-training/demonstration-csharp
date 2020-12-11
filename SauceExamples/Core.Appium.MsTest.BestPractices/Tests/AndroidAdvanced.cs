using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Support.UI;

namespace Core.Appium.MsTest.BestPractices.Tests
{
    [TestClass]
    public class AndroidAdvanced
    {
        /*
         * This is the very basic setup that you should have for any test automation.
         * Good automation includes:
         *  - Setup(), Teardown(), Quit(), and results update.
         *  - Page Object Model (example here Selenium.Nunit.Framework.BestPractices.test)
         * 
         */
        private static string HubUrlPart => "ondemand.us-west-1.saucelabs.com/wd/hub";
        private AndroidDriver<AndroidElement> _driver;
        public TestContext TestInfo { get; set; }

        [TestInitialize]
        public void Setup()
        {
            var sauceUser = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            var sauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
            var uri = $"https://{sauceUser}:{sauceAccessKey}@{HubUrlPart}";

            var capabilities = new AppiumOptions();
            //We can run on any version of the platform as long as it's the correct device
            //Make sure to pick an Android or iOS device based on your app
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Google Pixel.*");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            capabilities.AddAdditionalCapability("name", TestInfo.TestName);
            capabilities.AddAdditionalCapability("newCommandTimeout", 90);

            /*
             * You need to upload your own Native Mobile App to Sauce Storage!
             * https://wiki.saucelabs.com/display/DOCS/Uploading+your+Application+to+Sauce+Storage
             * You can use either storage:<app-id> or storage:filename=
             */
            capabilities.AddAdditionalCapability("app",
                "storage:filename=Android.SauceLabs.Mobile.Sample.app.2.5.0.apk");


            /*
             Getting Error: OpenQA.Selenium.WebDriverException : The HTTP request to the remote WebDriver server for URL 
            https://us1.appium.testobject.com/wd/hub/session timed out after 60 seconds.
                ----> System.Net.WebException : The operation has timed out
            Solution: Try changing to a more popular device
             */
            _driver = new AndroidDriver<AndroidElement>(new Uri(uri), capabilities);
        }
        [TestCleanup]
        public void Teardown()
        {
            if (_driver == null) return;

            var isTestPassed = TestInfo.CurrentTestOutcome == UnitTestOutcome.Passed;
            ((IJavaScriptExecutor)_driver).ExecuteScript("sauce:job-result=" + (isTestPassed ? "passed" : "failed"));
            _driver.Quit();
        }

        [TestMethod]

        public void ShouldOpenApp()
        {
            Action screenIsVisible = LoginScreenIsVisible;
            screenIsVisible.Should().NotThrow();
        }

        private void LoginScreenIsVisible()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementIsVisible(
                MobileBy.AccessibilityId("test-Username")));
        }

        [TestMethod]

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

            Action isCartVisible = () => GetCartElement(wait);
            isCartVisible.Should().NotThrow();
        }

        private void GetCartElement(WebDriverWait wait)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(
                            By.XPath("//android.view.ViewGroup[@content-desc='test-Cart']")));
        }
    }
}
