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
    public class IOSEmusimTests : EmusimBaseTest
    {
        private IOSDriver<IOSElement> _driver;

        public IOSEmusimTests(string deviceName, string platformVersion) : base(deviceName, platformVersion)
        {
        }

        [SetUp]
        public void Setup()
        {
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, DeviceName);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, PlatformVersion);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.BrowserName, "Safari");
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