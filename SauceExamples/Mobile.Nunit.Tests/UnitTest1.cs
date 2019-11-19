using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;

namespace Mobile.Nunit.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class UnitTest1
    {
        private SessionId _sessionId;
        private AndroidDriver<IWebElement> _driver;
        private static string USurl => "https://us1.appium.testobject.com/wd/hub";

        private static readonly string RottenTomatoesApiKey =
            Environment.GetEnvironmentVariable("ROTTEN_TOMATOES_API_KEY", EnvironmentVariableTarget.User);
        private static readonly string VodQANativeAppApiKey =
            Environment.GetEnvironmentVariable("VODQC_RDC_API_KEY", EnvironmentVariableTarget.User);
        public TestContext TestContext { get; set; }
        [Test]
        public void ShouldPass()
        {
            Assert.Pass();
        }
        [Test]
        public void ShouldFail()
        {
            Assert.Fail();
        }
        //[Test]
        //public void ShouldFailAndSetTestStatusToFail()
        //{
        //    var capabilities = new DesiredCapabilities();
        //    //Setting only the 2 capabilities below will run this test on 
        //    //any Android 7 device and this test runs in about 50s
        //    capabilities.SetCapability("platformName", "Android");
        //    capabilities.SetCapability("platformVersion", "7");
        //    //TODO first you must upload an app to Test Object so that you get your app key
        //    capabilities.SetCapability("testobject_api_key", VodQANativeAppApiKey);
        //    capabilities.SetCapability("name", MethodBase.GetCurrentMethod().Name);
        //    capabilities.SetCapability("newCommandTimeout", 90);

        //    _driver = new AndroidDriver<IWebElement>(new Uri(USurl), capabilities,
        //        TimeSpan.FromSeconds(300));
        //    Assert.IsTrue(false);
        //}

    }
}
