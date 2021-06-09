using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;

namespace Core.BestPractices.Web.Pages
{
    public class MobileLoginPage
    {
        private By _usernameLocator = By.CssSelector("#user-name");

        private By UsernameLocator { get => _usernameLocator; }
        public AndroidDriver<AndroidElement> Driver { get; private set; }
        public Wait Wait { get; private set; }

        public MobileLoginPage(AndroidDriver<AndroidElement> driver)
        {
            Driver = driver;
            Wait = new Wait(Driver);
        }

        public MobileLoginPage Visit()
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