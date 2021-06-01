using OpenQA.Selenium;

namespace Core.BestPractices.Web.Pages
{
    public class BaseWebPage
    {
        public readonly IWebDriver Driver;

        //public SauceJavaScriptExecutor SauceJsExecutor =>
        //    new SauceJavaScriptExecutor(_driver);

        public Wait Wait => new(Driver);
        public string BaseUrl { get; }

        public BaseWebPage(IWebDriver driver)
        {
            Driver = driver;
            BaseUrl = "https://www.saucedemo.com";
        }
    }
}