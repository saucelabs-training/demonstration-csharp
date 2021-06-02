using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Core.BestPractices.Web
{
    public class TestConfigData
    {
        public static IEnumerable MostPopularRealDevices
        {
            get
            {
                yield return new TestFixtureData("Google Pixel 4", "Android", "Chrome");
                yield return new TestFixtureData("Google Pixel 4 XL", "Android", "Chrome");
                yield return new TestFixtureData("Google Pixel 5", "Android", "Chrome");
            }
        }
    }
}