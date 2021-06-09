using Core.BestPractices.Web.Pages;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace Core.BestPractices.Web.Tests.Mobile
{
    [TestFixture]
    [TestFixtureSource(typeof(TestConfigData), nameof(TestConfigData.PopularAndroidSimulators))]
    public class AndroidEmusimTests : AllTestsBase
    {
        private AndroidDriver<AndroidElement> _driver;
        private readonly string deviceName;
        private readonly string platformVersion;

        public AndroidEmusimTests(string deviceName, string platformVersion)
        {
            this.deviceName = deviceName;
            this.platformVersion = platformVersion;
        }

        [SetUp]
        public void Setup()
        {
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, deviceName);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, platformVersion);
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