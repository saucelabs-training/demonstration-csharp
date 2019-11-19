using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;

namespace Mobile.Nunit.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class SystemSanity
    {
        [Test]
        public void ShouldPass()
        {
            Assert.Pass();
        }
    }
}
