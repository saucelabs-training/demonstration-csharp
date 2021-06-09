using Core.BestPractices.Web.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.iOS;
using System;

namespace Core.BestPractices.Web.MobileWebPageObjects.IOS
{
    public class LoginPage
    {
        private By _usernameLocator = By.CssSelector("#user-name");

        private By UsernameLocator { get => _usernameLocator; }
        public IOSDriver<IOSElement> Driver { get; private set; }
        public Wait Wait { get; private set; }

        public LoginPage(IOSDriver<IOSElement> driver)
        {
            Driver = driver;
            Wait = new Wait(Driver);
        }

        public LoginPage Visit()
        {
            Driver.Navigate().GoToUrl(Constants.BaseUrl);
            return this;
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