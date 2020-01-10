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
                yield return new TestFixtureData("Android", "8.1");
                yield return new TestFixtureData("Android", "9");
                yield return new TestFixtureData("Android", "10");

                //iOS
                yield return new TestFixtureData("iOS", "12.4");
                yield return new TestFixtureData("iOS", "11.4.1");
            }
        }
    }
}