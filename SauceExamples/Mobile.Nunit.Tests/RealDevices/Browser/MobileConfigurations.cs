using System.Collections;
using NUnit.Framework;

namespace Appium4.NUnit.Framework.RealDevices.Browser
{
    public class MobileConfigurations
    {
        public static IEnumerable Popular
        {
            get
            {
                //Android
                //yield return new TestFixtureData("Android", "Google Pixel.*", "chrome");
                //yield return new TestFixtureData("Android", "Samsung Galaxy.*", "chrome");

                //iOS
                yield return new TestFixtureData("iOS", "iPhone 11.*", "safari");
                yield return new TestFixtureData("iOS", "iPhone 12.*", "safari");
            }
        }
    }
}