using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;

namespace Core.Appium.Nunit.BestPractices.Tests
{
    [TestClass]
    public class AndroidFeatures : AndroidTest
    {
        [TestMethod]

        public void ShouldOpenApp()
        {
            Action screenIsVisible = LoginScreenIsVisible;
            screenIsVisible.Should().NotThrow();
        }

        private void LoginScreenIsVisible()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementIsVisible(
                MobileBy.AccessibilityId("test-Username")));
        }

        [TestMethod]

        public void ShouldLogin()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));

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
