using System;
using Common.TestData;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using static SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Core.Appium.Nunit.BestPractices.Tests
{
    [TestFixtureSource(typeof(DeviceCombinations), nameof(DeviceCombinations.PopularIosDevices))]
    [Parallelizable]
    public class IOSFeatures : IosTest
    {
        public IOSFeatures(string deviceName) : base(deviceName)
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
            wait.Until(ElementIsVisible(
                MobileBy.AccessibilityId("test-Username")));
        }

        [Test]
        public void ShouldLogin()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));

            var userName = wait.Until(ElementIsVisible(
                MobileBy.AccessibilityId("test-Username")));
            userName.SendKeys("standard_user");

            var password = wait.Until(ElementIsVisible(
                MobileBy.AccessibilityId("test-Password")));
            password.SendKeys("secret_sauce");

            var login = wait.Until(ElementIsVisible(
                MobileBy.AccessibilityId("test-LOGIN")));
            login.Click();

            Action isCartVisible = () => GetCartElement(wait);
            isCartVisible.Should().NotThrow();
        }

        private void GetCartElement(WebDriverWait wait)
        {
            wait.Until(ElementIsVisible(
                By.Name("test-Cart")));
        }
    }
}