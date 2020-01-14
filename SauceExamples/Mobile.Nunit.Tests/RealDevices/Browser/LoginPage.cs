using System.Collections.Generic;
using System.Reflection;
using Common;
using OpenQA.Selenium;

namespace Appium4.NUnit.Framework.RealDevices.Browser
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        private readonly By _loginButtonLocator = By.ClassName("btn_action");
        public bool IsLoaded => new Wait(Driver, _loginButtonLocator).IsVisible();

        public LoginPage Open()
        {
            Driver.Navigate().GoToUrl(BaseUrl);
            return this;
        }
    }
}