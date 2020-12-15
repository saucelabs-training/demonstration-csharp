using Common;
using OpenQA.Selenium.Appium.Android;

namespace Core.Appium.Nunit.BestPractices.Screens.Android
{
    public class BaseAndroidScreen
    {
        protected BaseAndroidScreen(AndroidDriver<AndroidElement> driver)
        {
            Driver = driver;
            Synchronizer = new Wait(Driver);
        }

        public Wait Synchronizer { get; set; }

        public AndroidDriver<AndroidElement> Driver { get; set; }
    }
}