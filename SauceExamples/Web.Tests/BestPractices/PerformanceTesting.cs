using NUnit.Framework;
using Web.Tests.Pages;


namespace Web.Tests.BestPractices
{
    [TestFixture]
    [Parallelizable]
    public class PerformanceTesting : BaseTest
    {
        private SauceDemoLoginPage _loginPage;

        public PerformanceTesting(string browser, string version, string os) :
            base(browser, version, os)
        {
        }

        [Test]
        public void LoginPageSpeedIndexIsWithin20Percent()
        {
            _loginPage.Open();
            var performanceMetrics = _loginPage.GetPerformance();

            Assert.That(performanceMetrics["speedIndex"], Is.EqualTo(415).Within(20).Percent);
        }
        [SetUp]
        public void RunBeforeEveryTest()
        {
            _loginPage = new SauceDemoLoginPage(Driver);
        }
    }
}
