using System.Collections.Generic;

namespace Core.Appium.Nunit.BestPractices.Data
{
    public class DeviceCombinations
    {
        public static IEnumerable<object[]> MostPopularDevices => new[]
        {
            new object[] { "iPhone 11 Pro"},
            new object[] {"iPhone 12"},
            new object[] {"iPad 10.2"},
        };

        public static IEnumerable<object[]> PopularAndroidDevices => new[]
        {
            new object[] { "Google Pixel.*", ""},
            new object[] { "Samsung Galaxy.*", ""},
            new object[] { ".*", "11"},
        };
    }
}