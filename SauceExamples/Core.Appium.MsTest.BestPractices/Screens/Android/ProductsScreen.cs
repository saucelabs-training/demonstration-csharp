using System;
using Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

namespace Core.Appium.Nunit.BestPractices.Screens.Android
{
    public class ProductsScreen : BaseAndroidScreen
    {
        public ProductsScreen(AndroidDriver<AndroidElement> driver) : base(driver)
        {
        }
        public Action IsVisible()
        {
            return IsCartElementVisible;
        }

        private void IsCartElementVisible()
        {
            Synchronizer.UntilIsVisible(By.XPath("//android.view.ViewGroup[@content-desc='test-Cart']"));
        }
    }
}