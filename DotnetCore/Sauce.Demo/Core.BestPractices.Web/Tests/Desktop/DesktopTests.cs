using Core.BestPractices.Web.Pages;
using Core.BestPractices.Web.Tests.Desktop;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Core.BestPractices.Web.Tests
{
    //[TestFixtureSource(typeof(TestConfigData), nameof(TestConfigData.MostPopularConfigurations))]
    [TestFixture]
    [Parallelizable]
    public class DesktopTests : WebTestsBase
    {
        [SetUp]
        public void SetupDesktopTests()
        {
            var browserOptions = new EdgeOptions
            {
                BrowserVersion = "latest",
                PlatformName = "Windows 10"
                //AcceptInsecureCertificates = true //Insecure Certs are Not supported by Edge
            };

            browserOptions.AddAdditionalCapability("sauce:options", SauceOptions);
            Driver = GetDesktopDriver(browserOptions.ToCapabilities());
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
            new ProductsPage(Driver).IsVisible().Should().Throw<WebDriverTimeoutException>();
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