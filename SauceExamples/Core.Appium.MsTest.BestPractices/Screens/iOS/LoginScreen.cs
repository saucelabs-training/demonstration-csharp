using System;
using Common;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Core.Appium.Nunit.BestPractices.Screens.iOS
{
    public class LoginScreen
    {
        private readonly IOSDriver<IOSElement> _driver;
        private readonly Wait _wait;

        public LoginScreen(IOSDriver<IOSElement> driver)
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