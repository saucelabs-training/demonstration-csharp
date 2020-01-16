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
                yield return new TestFixtureData("Android", "8.1", "chrome");
                yield return new TestFixtureData("Android", "9", "chrome");
                yield return new TestFixtureData("Android", "10", "chrome");

                //iOS
                yield return new TestFixtureData("iOS", "12.4", "safari");
                yield return new TestFixtureData("iOS", "11.4.1", "safari");
            }
        }
    }
}