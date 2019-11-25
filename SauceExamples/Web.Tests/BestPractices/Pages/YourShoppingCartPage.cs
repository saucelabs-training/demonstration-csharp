using OpenQA.Selenium;
using Web.Tests.BestPractices.Elements;

namespace Web.Tests.BestPractices.Pages
{
    internal class YourShoppingCartPage : BasePage
    {
        public YourShoppingCartPage(IWebDriver driver) : base(driver)
        {
        }

        public CartComponent Cart => new CartComponent(_driver);

        internal CheckoutInformationPage Checkout()
        {
            Wait.UntilIsVisibleByCss("a[class='btn_action checkout_button']").Click();
            return new CheckoutInformationPage(_driver);
        }

        internal YourShoppingCartPage Open()
        {
            _driver.Navigate().GoToUrl($"{BaseUrl}/cart.html");
            return this;
        }
    }
}