using Common;
using OpenQA.Selenium.Appium.Android;

namespace Core.Appium.Nunit.BestPractices.Screens.Android
{
    public class BaseAndroidScreen
    {
        protected BaseAndroidScreen(AndroidDriver<AndroidElement> driver)
        {
            Driver = driver;
            WaitFor = new Wait(Driver);
        }

        public Wait WaitFor { get; set; }

        public AndroidDriver<AndroidElement> Driver { get; set; }
    }
}