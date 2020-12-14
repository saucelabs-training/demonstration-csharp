using System;
using NUnit.Framework;
//Docs on NUnit parallelization: https://github.com/nunit/docs/wiki/Framework-Parallel-Test-Execution
[assembly: Parallelizable(ParallelScope.Fixtures)]
//Set this value to the Maximum amount of VMs that you have in Sauce Labs
[assembly: LevelOfParallelism(100)]

namespace Core.Appium.Nunit.BestPractices.Tests
{

    public class BaseTest
    {
        public static string HubUrlPart => "ondemand.us-west-1.saucelabs.com/wd/hub";
        public string SauceUser => Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
        public string SauceAccessKey => Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);

        public string Url => $"https://{SauceUser}:{SauceAccessKey}@{HubUrlPart}";
    }
}