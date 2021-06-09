using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using System;

namespace Core.BestPractices.Web.Tests.Mobile.Android
{
    [TestFixtureSource(typeof(TestConfigData), nameof(TestConfigData.MostPopularAndroidDevices))]
    [Parallelizable]
    public class RealDeviceAndroidWebTests : MobileBaseTest
    {
        public RealDeviceAndroidWebTests(string deviceName, string platform, string browser) :
            base(deviceName, platform, browser)
        { }

        [SetUp]
        public void AndroidSetup()
        {
            Driver = new AndroidDriver<AndroidElement>(new Uri(URI), MobileOptions);
        }

        [Test]

        public void ShouldOpenHomePage()
        {
            //60 seconds default for the connection timeout
            Driver.Navigate().GoToUrl("http://www.saucedemo.com");
            var size = Driver.Manage().Window.Size;
            Assert.AreNotEqual(0, size.Height);
        }
    }
}