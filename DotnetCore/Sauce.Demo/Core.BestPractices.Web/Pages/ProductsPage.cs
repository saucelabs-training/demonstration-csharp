using OpenQA.Selenium;
using System;

namespace Core.BestPractices.Web.Pages
{
    public class ProductsPage : BaseWebPage
    {
        private readonly string _pageUrlPart;

        public ProductsPage(IWebDriver driver) : base(driver)
        {
            _pageUrlPart = "inventory.html";
        }

        public bool IsLoaded => Wait.UntilIsDisplayedById("inventory_filter_container");

        private IWebElement LogoutLink => Driver.FindElement(By.Id("logout_sidebar_link"));

        private IWebElement HamburgerElement => Driver.FindElement(By.ClassName("bm-burger-button"));

        public int ProductCount =>
            Driver.FindElements(By.ClassName("inventory_item")).Count;

        public CartComponent Cart => new CartComponent(Driver);

        public void Logout()
        {
            HamburgerElement.Click();
            LogoutLink.Click();
        }

        internal ProductsPage Open()
        {
            Driver.Navigate().GoToUrl($"{BaseUrl}/{_pageUrlPart}");
            return this;
        }

        public void AddFirstProductToCart()
        {
            Wait.UntilIsVisibleByCss("button[class='btn_primary btn_inventory']").Click();
        }

        public Action IsVisible()
        {
            return IsCartElementVisible;
        }

        private void IsCartElementVisible()
        {
            Wait.UntilIsDisplayedById("inventory_filter_container");
        }
    }
}