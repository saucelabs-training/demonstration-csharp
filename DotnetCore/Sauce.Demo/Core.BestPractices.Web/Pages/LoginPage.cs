using System.Collections.Generic;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Core.BestPractices.Web.Pages
{
    public class LoginPage : BaseWebPage
    {
        public LoginPage(IWebDriver driver) : base(driver){}

        private readonly By _loginButtonLocator = By.ClassName("btn_action");
        public bool IsLoaded => new Wait(Driver, _loginButtonLocator).IsVisible();
        public IWebElement PasswordField => Driver.FindElement(By.Id("password"));
        public IWebElement LoginButton => Driver.FindElement(_loginButtonLocator);
        private readonly By _usernameLocator = By.Id("user-name");
        public IWebElement UsernameField => Driver.FindElement(_usernameLocator);

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
            var usernameField = Wait.UntilIsVisible(_usernameLocator);
            usernameField.SendKeys(username);
            PasswordField.SendKeys("secret_sauce");
            LoginButton.Click();
            //SauceJsExecutor.LogMessage($"{MethodBase.GetCurrentMethod().Name} success");
            return new ProductsPage(Driver);
        }

        public void TakeSnapshot()
        {
            JavaScriptExecutor.ExecuteScript("/*@visual.snapshot*/", "Login Page");
        }
    }
}