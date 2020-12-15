using System;
using Common;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Core.Appium.Nunit.BestPractices.Screens.Android
{
    public class LoginScreen
    {
        private AndroidDriver<AndroidElement> _driver;
        private Wait _wait;

        public LoginScreen(AndroidDriver<AndroidElement> driver)
        {
            _driver = driver;
            _wait = new Wait(_driver);
        }

        public Action IsVisible()
        {
            return IsUsernameFieldVisible;
        }
        private void IsUsernameFieldVisible()
        {
            _wait.UntilIsVisible(
                MobileBy.AccessibilityId("test-Username"));
        }

        public void Login(string username, string password)
        {
            var userName = _wait.UntilIsVisible(
                MobileBy.AccessibilityId("test-Username"));
            userName.SendKeys(username);

            var passwordField = _wait.UntilIsVisible(
                MobileBy.AccessibilityId("test-Password"));
            passwordField.SendKeys(password);

            var login = _wait.UntilIsVisible(
                MobileBy.AccessibilityId("test-LOGIN"));
            login.Click();
        }
    }
}