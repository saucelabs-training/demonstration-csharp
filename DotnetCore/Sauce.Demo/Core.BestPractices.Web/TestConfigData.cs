using NUnit.Framework;
using System.Collections;

namespace Core.BestPractices.Web
{
    public class TestConfigData
    {
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