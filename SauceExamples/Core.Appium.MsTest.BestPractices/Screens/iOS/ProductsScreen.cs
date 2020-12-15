using System;
using Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.iOS;

namespace Core.Appium.Nunit.BestPractices.Screens.iOS
{
    public class ProductsScreen
    {
        private readonly IOSDriver<IOSElement> _driver;
        private readonly Wait _wait;

        public ProductsScreen(IOSDriver<IOSElement> driver)
        {
            _driver = driver;
            //20 seconds is a really good timeout for mobile unless your app is Very slow
            _wait = new Wait(_driver, 20);
        }

        public Action IsVisible()
        {
            return IsCartElementVisible;
        }

        private void IsCartElementVisible()
        {
            _wait.UntilIsVisible(By.Name("test-Cart"));
        }
    }
}