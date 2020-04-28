using System;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework;

namespace Selenium3.Nunit.Scripts.SimpleExamples
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [Category("Parallel selenium tests at the class level")]
    class ParallelTestsAtMethods
    {
        [Test]
        public void Test1()
        {
            Thread.Sleep(3000);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Assert.Pass();
        }
        [Test]
        public void Test2()
        {
            Thread.Sleep(3000);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Assert.Pass();
        }
        [Test]
        public void Test3()
        {
            Thread.Sleep(3000);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Assert.Pass();
        }
    }
}
