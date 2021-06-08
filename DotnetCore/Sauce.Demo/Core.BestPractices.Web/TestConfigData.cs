using NUnit.Framework;
using OpenQA.Selenium.Chrome;
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

        static readonly ChromeOptions chromeOptions = new()
        {
            BrowserVersion = "latest",
            PlatformName = "Windows 10"
        };
        public static IEnumerable PopularVisualResolutions
        {
            get
            {
                yield return new TestFixtureData(safariOptions, "375x812", "Iphone X");
                yield return new TestFixtureData(safariOptions, "1280x1024", "1024p");
                yield return new TestFixtureData(safariOptions, "1920x1080", "1080p");
                yield return new TestFixtureData(chromeOptions, "412x732", "Pixel XL");
                yield return new TestFixtureData(chromeOptions, "412x869", "Galaxy Note 10+");
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