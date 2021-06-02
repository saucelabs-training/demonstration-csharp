using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Core.BestPractices.Web.Tests
{
    [TestFixture]
    public class WebTestsBase : AllTestsBase
    {
        public string SauceUserName;
        public string SauceAccessKey;
        public Dictionary<string, object> SauceOptions;
        public string ScreenerApiKey;

        [SetUp]
        public void Setup()
        {
            SauceUserName = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            SauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
            ScreenerApiKey = Environment.GetEnvironmentVariable("SCREENER_API_KEY", EnvironmentVariableTarget.User);
            SauceOptions = new Dictionary<string, object>
            {
                ["username"] = SauceUserName,
                ["accessKey"] = SauceAccessKey,
                ["name"] = TestContext.CurrentContext.Test.Name
            };
        }
    }
}