using Core.BestPractices.Web.Pages;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace Core.BestPractices.Web.Tests.Mobile
{
    [TestFixture]
    public class IOSEmusimWebTests : AllTestsBase
    {
        private IOSDriver<IOSElement> _driver;

        [SetUp]
        public void Setup()
        {
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "iPhone XS Max Simulator");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "14.3");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.BrowserName, "Safari");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.AppiumVersion, "1.20.1");
            appiumOptions.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);

            _driver = new IOSDriver<IOSElement>(new SauceLabsEndpoint().EmusimUri(SauceUserName, SauceAccessKey), appiumOptions);
        }
        [TearDown]
        public void EmusimTeardown()
        {
            if (_driver == null) return;

            ExecuteSauceCleanupSteps(_driver);
            _driver.Quit();
        }

        [Test]
        public void LoginPageOpens()
        {

            _driver.Navigate().GoToUrl("https://www.saucedemo.com");
            IsVisible().Should().NotThrow();
        }

        public Action IsVisible()
        {
            return IsUsernameVisible;
        }

        private void IsUsernameVisible()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#user-name")));
        }
    }
}