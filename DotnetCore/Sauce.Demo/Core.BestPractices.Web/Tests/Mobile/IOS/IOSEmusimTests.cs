using Core.BestPractices.Web.MobileWebPageObjects.IOS;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;

namespace Core.BestPractices.Web.Tests.Mobile.IOS
{
    [TestFixture]
    [TestFixtureSource(typeof(TestConfigData), nameof(TestConfigData.PopularIOSSimulators))]
    public class IOSEmusimTests : AllTestsBase
    {
        private IOSDriver<IOSElement> _driver;
        private readonly string deviceName;
        private readonly string platformVersion;

        public IOSEmusimTests(string deviceName, string platformVersion)
        {
            this.deviceName = deviceName;
            this.platformVersion = platformVersion;
        }

        [SetUp]
        public void Setup()
        {
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, deviceName);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, platformVersion);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.BrowserName, "Safari");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.AppiumVersion, "1.20.1");
            appiumOptions.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);
            appiumOptions.AddAdditionalCapability("build", Constants.BuildId);

            _driver = GetIOSDriver(appiumOptions);
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