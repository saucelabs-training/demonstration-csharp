using System;
using Core.BestPractices.Web.Pages;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;

namespace Core.BestPractices.Web.Tests
{
    [TestFixture]
    public class EmusimWebTests : AllTestsBase
    {
        [SetUp]
        public void Setup()
        {
            _sauceUserName = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            _sauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);

            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName,
                "Google Pixel 3 XL GoogleAPI Emulator");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "11.0");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.BrowserName, "Chrome");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.AppiumVersion, "1.20.2");
            appiumOptions.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);

            Driver = new RemoteWebDriver(new SauceLabsEndpoint().EmusimUri(_sauceUserName, _sauceAccessKey),
                appiumOptions.ToCapabilities(), TimeSpan.FromSeconds(120));
        }

        private string _sauceUserName;
        private string _sauceAccessKey;

        [Test]
        public void LoginWorks()
        {
            var loginPage = new LoginPage(Driver);
            loginPage.Visit();
            loginPage.Login("standard_user");
            new ProductsPage(Driver).IsVisible().Should().NotThrow();
        }
    }
}