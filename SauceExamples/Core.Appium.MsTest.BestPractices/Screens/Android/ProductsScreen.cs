using System;
using Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

namespace Core.Appium.Nunit.BestPractices.Screens.Android
{
    public class ProductsScreen
    {
        private readonly AndroidDriver<AndroidElement> _driver;
        private readonly Wait _wait;

        public ProductsScreen(AndroidDriver<AndroidElement> driver)
        {
            _driver = driver;
            _wait = new Wait(_driver);
        }

        public Action IsVisible()
        {
            return IsCartElementVisible;
        }

        private void IsCartElementVisible()
        {
            _wait.UntilIsVisible(By.XPath("//android.view.ViewGroup[@content-desc='test-Cart']"));
        }
    }
}