using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
[assembly: Parallelize(Workers = 100, Scope = ExecutionScope.MethodLevel)]

namespace Core.Selenium4.MsTest.Scripts.SpecFlow.StepDefinitions
{
    [Binding]
    public class ParallelSteps
    {
        private readonly IWebDriver _driver;

        public ParallelSteps(IWebDriver driver)
        {
            _driver = driver;
        }

        [When(@"a user navigates to Sauce Demo")]
        public void WhenAUserNavigatesToSauceDemo()
        {
            _driver.Navigate().GoToUrl("https://www.saucedemo.com");
        }
        
        [Then(@"a user sees the Sauce Demo login page")]
        public void ThenAUserSeesTheSauceDemoLoginPage()
        {
            Assert.IsTrue(_driver.Url.Contains("saucedemo.com"));
        }
    }
}
