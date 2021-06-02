using OpenQA.Selenium;

namespace Core.BestPractices.Web
{
    class Browser
    {
        private readonly IWebDriver _driver;

        public Browser(IWebDriver driver)
        {
            _driver = driver;
        }

        public IJavaScriptExecutor JS => (IJavaScriptExecutor)_driver;
    }
}
