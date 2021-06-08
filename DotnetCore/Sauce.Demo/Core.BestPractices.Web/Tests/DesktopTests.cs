using Core.BestPractices.Web.Pages;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using System;

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
    }
}