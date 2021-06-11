﻿using Core.BestPractices.Web.Pages;
using Core.BestPractices.Web.Tests.Mobile.IOS;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace Core.BestPractices.Web.Tests.Mobile
{
    [TestFixture]
    [TestFixtureSource(typeof(TestConfigData), nameof(TestConfigData.PopularAndroidSimulators))]
    public class AndroidEmusimTests : EmusimBaseTest
    {
        private AndroidDriver<AndroidElement> _driver;

        public AndroidEmusimTests(string deviceName, string platformVersion) : base(deviceName, platformVersion)
        {
        }

        [SetUp]
        public void Setup()
        {
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, DeviceName);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, PlatformVersion);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.BrowserName, "Chrome");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.AppiumVersion, "1.20.2");
            appiumOptions.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);
            appiumOptions.AddAdditionalCapability("build", Constants.BuildId);

            _driver = GetAndroidDriver(appiumOptions);
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
            var loginPage = new LoginPage(_driver);
            loginPage.Visit();
            loginPage.IsVisible().Should().NotThrow();
        }
    }
}