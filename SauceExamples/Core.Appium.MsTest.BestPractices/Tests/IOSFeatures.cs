using Common.TestData;
using FluentAssertions;
using Core.Appium.Nunit.BestPractices.Screens.iOS;
using NUnit.Framework;

namespace Core.Appium.Nunit.BestPractices.Tests
{
    [TestFixtureSource(typeof(DeviceCombinations), nameof(DeviceCombinations.PopularIosDevices))]
    [Parallelizable]
    public class IosFeatures : IosTest
    {
        public IosFeatures(string deviceName) : base(deviceName)
        {
        }

        [Test]
        public void ShouldOpenApp()
        {
            var loginScreen = new LoginScreen(Driver);
            loginScreen.IsVisible().Should().NotThrow();
        }

        [Test]
        public void ShouldLogin()
        {
            var loginScreen = new LoginScreen(Driver);
            loginScreen.Login("standard_user", "secret_sauce");

            new ProductsScreen(Driver).IsVisible().Should().NotThrow();
        }
    }
}