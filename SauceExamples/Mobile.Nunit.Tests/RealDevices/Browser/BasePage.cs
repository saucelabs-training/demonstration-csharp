using Common;
using Common.SauceLabs;
using OpenQA.Selenium;

namespace Appium4.NUnit.Framework.RealDevices.Browser
{
    public class BasePage
    {
        public readonly IWebDriver Driver;
        private readonly string _baseUrl;

        public SauceJavaScriptExecutor SauceJsExecutor =>
            new SauceJavaScriptExecutor(Driver);

        public Wait Wait => new Wait(Driver);
        public string BaseUrl => _baseUrl;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            _baseUrl = "https://www.saucedemo.com";
        }
    }
}