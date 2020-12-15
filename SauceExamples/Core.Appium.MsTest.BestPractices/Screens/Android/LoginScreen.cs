using System;
using Common;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Core.Appium.Nunit.BestPractices.Screens.Android
{
    public class LoginScreen : BaseAndroidScreen
    {
        public LoginScreen(AndroidDriver<AndroidElement> driver) : base(driver)
        {
        }

        public Action IsVisible()
        {
            return IsUsernameFieldVisible;
        }

        private void IsUsernameFieldVisible()
        {
            Synchronizer.UntilIsVisible(
                MobileBy.AccessibilityId("test-Username"));
        }

        public void Login(string username, string password)
        {
            var userName = Synchronizer.UntilIsVisible(
                MobileBy.AccessibilityId("test-Username"));
            userName.SendKeys(username);

            var passwordField = Synchronizer.UntilIsVisible(
                MobileBy.AccessibilityId("test-Password"));
            passwordField.SendKeys(password);

            var login = Synchronizer.UntilIsVisible(
                MobileBy.AccessibilityId("test-LOGIN"));
            login.Click();
        }
    }
}