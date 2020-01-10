using System;
using Common;
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
            Driver.Navigate().GoToUrl("https://www.saucedemo.com");
            var isDisplayed = new Wait(Driver, 30).UntilIsDisplayedById("user-name");
            Assert.IsTrue(isDisplayed);
            Assert.IsTrue(Driver.Url.Contains("saucedemo.com"));
        }
    }
}
