using System;
using System.Reflection;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;

namespace Appium4.NUnit.Scripts.RealDevices.NativeApp
{
    [TestClass]
    public class AndroidGetStarted
    {
        private static string RdcUsHubUrl => "https://us1.appium.testobject.com/wd/hub";
        private AndroidDriver<IWebElement> _driver;
        private static readonly string VodQANativeAppApiKey =
            Environment.GetEnvironmentVariable("VODQC_RDC_API_KEY", EnvironmentVariableTarget.User);

        [TestMethod]
        public void TestMethod1()
        {
            var capabilities = new DesiredCapabilities();
            //We can run on any version of the platform as long as it's the correct device
            //Make sure to pick an Android or iOS device based on your app
            capabilities.SetCapability("deviceName", "Google Pixel");
            /*
             *TODO first you must upload an app to Test Object so that you get your app key
             * Then, make sure you can hardcode it here just to get started
             */
            capabilities.SetCapability("testobject_api_key", new ApiKeys().Rdc.Apps.SampleAppAndroid);
            capabilities.SetCapability("name", MethodBase.GetCurrentMethod().Name);
            capabilities.SetCapability("newCommandTimeout", 90);

            _driver = new AndroidDriver<IWebElement>(new Uri(RdcUsHubUrl), capabilities,
                TimeSpan.FromSeconds(300));
            Assert.IsTrue(true);
        }
    }
}
