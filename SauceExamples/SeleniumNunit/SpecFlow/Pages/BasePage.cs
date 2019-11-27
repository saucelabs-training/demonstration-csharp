using OpenQA.Selenium;

namespace Selenium.Nunit.Scripts.SpecFlow.Pages
{
    public class BasePage
    {
        public IWebDriver Driver { get; }

        public string BaseUrl { get; }

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            BaseUrl = "https://www.saucedemo.com";
        }
    }
}