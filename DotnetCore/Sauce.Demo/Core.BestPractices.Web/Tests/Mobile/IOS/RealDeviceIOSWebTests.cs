using Core.BestPractices.Web.MobileWebPageObjects.IOS;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium.Appium.iOS;

namespace Core.BestPractices.Web.Tests.Mobile.IOS
{
    [TestFixtureSource(typeof(TestConfigData), nameof(TestConfigData.MostPopularIOSDevices))]
    [Parallelizable]
    public class RealDeviceIOSWebTests : MobileBaseTest
    {
        [SetUp]
        public void IOSSetup()
        {
            Driver = GetIOSDriver(MobileOptions);
        }

        [TearDown]
        public void Teardown()
        {
            if (Driver == null) return;

            ExecuteSauceCleanupSteps(Driver);
            Driver.Quit();
        }

        public new IOSDriver<IOSElement> Driver { get; set; }

        public RealDeviceIOSWebTests(string deviceName, string platform, string browser) :
            base(deviceName, platform, browser)
        {
        }

        [Test]
        public void ShouldOpenHomePage()
        {
            var loginPage = new LoginPage(Driver);
            loginPage.Visit();
            loginPage.IsVisible().Should().NotThrow();
        }
    }
}