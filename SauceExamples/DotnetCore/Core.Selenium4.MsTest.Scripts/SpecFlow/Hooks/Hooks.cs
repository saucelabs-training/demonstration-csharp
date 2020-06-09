using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Core.Selenium4.MsTest.Scripts.SpecFlow.Hooks
{
    [Binding]
    public class Hooks
    {
        public IWebDriver Driver;
        private ScenarioContext _scenarioContext;

        public Hooks(IWebDriver driver, ScenarioContext scenarioContext)
        {
            Driver = driver;
            _scenarioContext = scenarioContext;
        }

        [AfterScenario]
        public void Teardown()
        {
            if (Driver == null) return;

            var passed = _scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.OK;
            ((IJavaScriptExecutor)Driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
            Driver.Quit();
        }

    }
}
