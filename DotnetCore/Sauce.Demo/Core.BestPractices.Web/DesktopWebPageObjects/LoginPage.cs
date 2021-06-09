using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Core.BestPractices.Web.Pages
{
    public class LoginPage : BaseWebPage
    {
        private By _usernameLocator = By.CssSelector("#user-name");

        private By UsernameLocator { get => _usernameLocator; }

        public LoginPage(IWebDriver driver) : base(driver) { }

        public LoginPage Visit()
        {
            Driver.Navigate().GoToUrl(BaseUrl);
            return this;
        }

        internal Dictionary<string, object> GetPerformance()
        {
            var metrics = new Dictionary<string, object>
            {
                ["type"] = "sauce:performance"
            };
            return (Dictionary<string, object>)((IJavaScriptExecutor)Driver).ExecuteScript("sauce:log", metrics);
        }

        public ProductsPage Login(string username)
        {
            //SauceJsExecutor.LogMessage(
            //    $"Start login with user=>{username} and pass=>{password}");
            var usernameField = Wait.UntilIsVisible(UsernameLocator);
            usernameField.SendKeys(username);
            Driver.FindElement(By.CssSelector("#password")).SendKeys("secret_sauce");
            Driver.FindElement(By.CssSelector(".btn_action")).Click();
            //SauceJsExecutor.LogMessage($"{MethodBase.GetCurrentMethod().Name} success");
            return new ProductsPage(Driver);
        }

        public Action IsVisible()
        {
            return IsElementVisible;
        }

        private void IsElementVisible()
        {
            new Wait(Driver).UntilIsVisible(UsernameLocator);
        }
    }
}