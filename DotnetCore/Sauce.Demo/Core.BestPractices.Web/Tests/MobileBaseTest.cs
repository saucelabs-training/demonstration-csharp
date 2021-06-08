using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;

namespace Core.BestPractices.Web.Tests
{
    public class MobileBaseTest : AllTestsBase
    {
        public readonly string DeviceName;
        public readonly string Platform;
        public readonly string Browser;
        public string URI;
        private static string HubUrl => "ondemand.us-west-1.saucelabs.com/wd/hub";


        public MobileBaseTest(string deviceName, string platform, string browser)
        {
            DeviceName = deviceName;
            Platform = platform;
            Browser = browser;
        }
        [SetUp]
        public void MobileBaseSetup()
        {
            URI = $"https://{SauceUserName}:{SauceAccessKey}@{HubUrl}";

            MobileOptions = new AppiumOptions();
            MobileOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, DeviceName);
            MobileOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, Platform);
            MobileOptions.AddAdditionalCapability(MobileCapabilityType.BrowserName, Browser);
            MobileOptions.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);
            MobileOptions.AddAdditionalCapability("newCommandTimeout", 90);
        }

        public AppiumOptions MobileOptions { get; set; }

        //Never forget to pass the test status to Sauce Labs
        [TearDown]
        public void Teardown()
        {
            if (Driver == null) return;

            ExecuteSauceCleanupSteps(Driver);
            Driver.Quit();
        }
    }
}