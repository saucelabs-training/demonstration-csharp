using System;
using System.Globalization;
using Common.SauceLabs;
using NUnit.Framework;

namespace Selenium.Nunit.Scripts.BestPractices.CrossBrowserExamples
{
    [SetUpFixture]
    public class NamespaceSetup
    {
        [OneTimeSetUp]
        public void RunForWholeNamespace()
        {
            SauceLabsCapabilities.BuildName = $"CrossBrowserTests-{DateTime.Now.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
