using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;
[assembly: Parallelize(Workers = 100, Scope = ExecutionScope.MethodLevel)]

namespace Core.Selenium4.MsTest.Scripts.SpecFlow.StepDefinitions
{
    [Binding]
    public class ParallelSteps
    {
        private IWebDriver _driver;
        private ScenarioContext _scenarioContext;

        public ParallelSteps(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _driver = driver;
            _scenarioContext = scenarioContext;
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
