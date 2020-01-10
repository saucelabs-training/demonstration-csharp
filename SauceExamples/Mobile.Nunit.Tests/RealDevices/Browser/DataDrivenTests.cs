using System;
using Common;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;

namespace Appium4.NUnit.Framework.RealDevices.Browser
{
    [TestFixture]
    [TestFixtureSource(typeof(MobileConfigurations),
        nameof(MobileConfigurations.Popular))]
    [Parallelizable]
    public class DataDrivenTests : BaseMobileTest
    {
        public DataDrivenTests(string platformName, string platformVersion) : 
            base(platformName, platformVersion)
        {
        }

        [Test]
        public void AndroidDataDrivenMobileBrowserTest()
        {
            new LoginPage(Driver).Open().IsLoaded.Should().BeTrue("the login page should load successfully.");
        }
    }
}
