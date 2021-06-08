using NUnit.Framework;
using OpenQA.Selenium.Safari;
using System.Collections;

namespace Core.BestPractices.Web
{
    public class TestConfigData
    {
        static readonly SafariOptions safariOptions = new()
        {
            BrowserVersion = "latest",
            PlatformName = "macOS 10.15"
        };
        public static IEnumerable PopularRealDevices
        {
            get
            {
                yield return new TestFixtureData(safariOptions, "375x812", "iphone x");
                yield return new TestFixtureData(safariOptions, "375x812", "iphone 1");
                yield return new TestFixtureData(safariOptions, "375x812", "iphone 2");
                yield return new TestFixtureData(safariOptions, "375x812", "iphone 3");
                yield return new TestFixtureData(safariOptions, "375x812", "iphone 4");
            }
        }
        public static IEnumerable MostPopularAndroidDevices
        {
            get
            {
                yield return new TestFixtureData("Google Pixel 4", "Android", "Chrome");
                yield return new TestFixtureData("Google Pixel 4 XL", "Android", "Chrome");
                yield return new TestFixtureData("Google Pixel 5", "Android", "Chrome");
            }
        }

        public static IEnumerable MostPopularIOSDevices
        {
            get
            {
                yield return new TestFixtureData("iPhone 11.*", "iOS", "Safari");
                yield return new TestFixtureData("iPhone 12.*", "iOS", "Safari");
                yield return new TestFixtureData("iPad 10.*", "iOS", "Safari");
            }
        }
    }
}