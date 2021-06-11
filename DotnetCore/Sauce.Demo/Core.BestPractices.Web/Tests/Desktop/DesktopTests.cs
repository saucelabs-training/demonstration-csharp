using Core.BestPractices.Web.Pages;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace Core.BestPractices.Web.Tests.Desktop
{
    [TestFixtureSource(typeof(TestConfigData), nameof(TestConfigData.PopularDesktopCombinations))]
    [TestFixture]
    [Parallelizable]
    public class DesktopTests : WebTestsBase
    {
        public string BrowserVersion { get; }
        public string PlatformName { get; }
        public DriverOptions BrowserOptions { get; }

        public DesktopTests(string browserVersion, string platformName, DriverOptions browserOptions)
        {
            if (string.IsNullOrEmpty(browserVersion))
                BrowserVersion = browserVersion;
            if (string.IsNullOrEmpty(platformName))
                PlatformName = platformName;
            BrowserOptions = browserOptions;
        }
        [SetUp]
        public void SetupDesktopTests()
        {
            if (BrowserOptions.BrowserName == "chrome")
                ((ChromeOptions)BrowserOptions).AddAdditionalCapability("sauce:options", SauceOptions, true);
            else
                BrowserOptions.AddAdditionalCapability("sauce:options", SauceOptions);
            Driver = GetDesktopDriver(BrowserOptions.ToCapabilities());
        }
        [Test]
        public void LoginWorks()
        {
            var loginPage = new LoginPage(Driver);
            loginPage.Visit();
            loginPage.Login("standard_user");
            new ProductsPage(Driver).IsVisible().Should().NotThrow();
        }

        [Test]
        public void InvalidCredentialsFail()
        {
            var loginPage = new LoginPage(Driver);
            loginPage.Visit();
            loginPage.Login("locked_out_user");
            new ProductsPage(Driver).IsVisible().Should().Throw<WebDriverTimeoutException>("locked out user shouldn't be able to login");
        }

        [Test]
        public void ProblemUserLogsIn()
        {
            var loginPage = new LoginPage(Driver);
            loginPage.Visit();
            loginPage.Login("problem_user");
            new ProductsPage(Driver).IsVisible().Should().NotThrow();
        }

        [Test]
        public void PerformanceUserLogsIn()
        {
            var loginPage = new LoginPage(Driver);
            loginPage.Visit();
            loginPage.Login("performance_glitch_user");
            new ProductsPage(Driver).IsVisible().Should().NotThrow();
        }
    }
}