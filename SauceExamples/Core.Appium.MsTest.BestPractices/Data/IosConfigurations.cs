using System.Collections.Generic;

namespace Core.Appium.Nunit.BestPractices.Tests
{
    public class IosConfigurations
    {
        public static IEnumerable<object[]> MostPopularDevices => new[]
        {
            new object[] { "iPhone 11 Pro"},
            new object[] {"iPhone 12"},
            new object[] {"iPad 10.2"},
        };
    }
}