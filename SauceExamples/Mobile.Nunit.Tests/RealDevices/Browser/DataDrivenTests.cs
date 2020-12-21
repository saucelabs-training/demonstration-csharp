using FluentAssertions;
using NUnit.Framework;

namespace Appium4.NUnit.Framework.RealDevices.Browser
{
    [TestFixture]
    [TestFixtureSource(typeof(MobileConfigurations),
        nameof(MobileConfigurations.Popular))]
    [Parallelizable]
    public class DataDrivenTests : BaseMobileTest
    {
        public DataDrivenTests(string platformName, string deviceName, string browserName) : 
            base(platformName, deviceName, browserName)
        {
        }

        [Test]
        public void ShouldOpenLoginPage()
        {
            new LoginPage(Driver).Open().IsLoaded.Should().BeTrue("the login page should load successfully.");
        }
    }
}
