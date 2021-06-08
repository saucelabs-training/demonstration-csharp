using NUnit.Framework;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;

namespace Core.BestPractices.Web.Tests
{
    [TestFixture]
    public class AllTestsBase
    {
        public RemoteWebDriver Driver;

        public string SauceUserName;
        public string SauceAccessKey;
        public Dictionary<string, object> SauceOptions;
        public string ScreenerApiKey;
    }
}