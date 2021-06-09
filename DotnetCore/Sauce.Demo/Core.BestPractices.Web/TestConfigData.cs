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

        internal static IEnumerable PopularAndroidSimulators
        {
            get
            {
                yield return new TestFixtureData("Google Pixel 3 XL GoogleAPI Emulator", "11.0");
                //duplication for more parallelization
                yield return new TestFixtureData("Google Pixel 3 XL GoogleAPI Emulator", "11.0");
                yield return new TestFixtureData("Google Pixel 3 XL GoogleAPI Emulator", "11.0");
                yield return new TestFixtureData("Google Pixel 3 XL GoogleAPI Emulator", "11.0");
                yield return new TestFixtureData("Google Pixel 3 XL GoogleAPI Emulator", "11.0");
                yield return new TestFixtureData("Google Pixel 3 XL GoogleAPI Emulator", "11.0");
                yield return new TestFixtureData("Google Pixel 3 XL GoogleAPI Emulator", "11.0");
            }
        }

        internal static IEnumerable PopularIOSSimulators
        {
            get
            {
                yield return new TestFixtureData("iPhone X Simulator", "14.3");
                //duplication for more parallelization
                yield return new TestFixtureData("iPhone X Simulator", "14.3");
                yield return new TestFixtureData("iPhone X Simulator", "14.3");
                yield return new TestFixtureData("iPhone X Simulator", "14.3");
                yield return new TestFixtureData("iPhone X Simulator", "14.3");
                yield return new TestFixtureData("iPhone X Simulator", "14.3");
                yield return new TestFixtureData("iPhone X Simulator", "14.3");
                yield return new TestFixtureData("iPhone X Simulator", "14.3");
                yield return new TestFixtureData("iPhone X Simulator", "14.3");
            }
        }

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
                yield return new TestFixtureData("Google Pixel.*", "Android", "Chrome");
                yield return new TestFixtureData("Google Pixel.*", "Android", "Chrome");
                yield return new TestFixtureData("Google Pixel.*", "Android", "Chrome");
                // duplication for massive parallel example
                yield return new TestFixtureData("HTC.*", "Android", "Chrome");
                yield return new TestFixtureData("HTC.*", "Android", "Chrome");
                yield return new TestFixtureData("Huawei.*", "Android", "Chrome");
            }
        }

        public static IEnumerable MostPopularIOSDevices
        {
            get
            {
                yield return new TestFixtureData("iPhone 11.*", "iOS", "Safari");
                yield return new TestFixtureData("iPhone 12.*", "iOS", "Safari");
                yield return new TestFixtureData("iPad 10.*", "iOS", "Safari");
                //duplication only for parallel example
                yield return new TestFixtureData("iPhone 11.*", "iOS", "Safari");
                yield return new TestFixtureData("iPhone 12.*", "iOS", "Safari");
                yield return new TestFixtureData("iPad 10.*", "iOS", "Safari");
            }
        }
    }
}