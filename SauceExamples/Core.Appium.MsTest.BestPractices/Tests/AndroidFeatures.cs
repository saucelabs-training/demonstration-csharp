using System;
using Core.Appium.Nunit.BestPractices.Data;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;

namespace Core.Appium.Nunit.BestPractices.Tests
{
    [TestFixtureSource(typeof(BrowserCombinations), nameof(BrowserCombinations.PopularAndroidDevices))]
    [Parallelizable]
    public class AndroidFeatures : AndroidTest
    {
        public AndroidFeatures(string deviceName, string deviceVersion) : base(deviceName, deviceVersion)
        {
        }

        [Test]
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

        [Test]

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
