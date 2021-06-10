using Core.BestPractices.Web.MobileWebPageObjects.Android;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;

namespace Core.BestPractices.Web.Tests.Mobile.Android
{
    [TestFixtureSource(typeof(TestConfigData), nameof(TestConfigData.MostPopularAndroidDevices))]
    [Parallelizable]
    public class RealDeviceAndroidWebTests : MobileBaseTest
    {
        public new AndroidDriver<AndroidElement> Driver { get; set; }
        public RealDeviceAndroidWebTests(string deviceName, string platform, string browser) :
            base(deviceName, platform, browser)
        { }

        [SetUp]
        public void AndroidSetup()
        {
            Driver = GetAndroidDriver(MobileOptions);
        }
        [TearDown]
        public void Teardown()
        {
            if (Driver == null) return;

            ExecuteSauceCleanupSteps(Driver);
            Driver.Quit();
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