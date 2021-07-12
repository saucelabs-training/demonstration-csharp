# Automation Best Practices Workshop

## Pre-reqs

* .NET5 or greater
* C# 9 or greater

## Setup

* Download and install [.NET5](https://dotnet.microsoft.com/download)
* Download and install Visual Studio
* Download [this code branch 30 min before the workshop](https://github.com/saucelabs-training/demo-csharp/tree/2_hr_workshop)
* Get your sauce username and access key
* Store them in environment variables on your machine

## Simple desktop web test

* Open Visual Studio
* Build solution
* Run test called `EdgeW3C` in `Core.Selenium.Examples` project
* Head to [saucelabs.com](https://accounts.saucelabs.com/am/XUI/#login/) to look at the running test

🎖Success is when the test runs

ℹ️ [Web testing best practices tutorial](https://www.youtube.com/watch?v=r9K-2OJUmOE)

## Cross-browser testing

⭐️ Cross-browser functional bugs rarely exist in today's web technology and doing cross-browser functional testing on an entire test suite is inneficient

⭐️ It's more common to find cross-browser rendering issues, especially for responsive web apps. This problem is solved with visual testing.

* Go to 
* Add `[TestFixtureSource(typeof(TestConfigData), nameof(TestConfigData.PopularDesktopCombinations))]` to the top of the class
* Run the tests and notice how fast the parallel build is
* Check out the [Insights Tab](https://app.saucelabs.com/analytics/test-overview)

🎖Success is when all the tests run

ℹ️ [Learn about parallelization with different test runners](https://ultimateqa.com/parallelization-in-csharp/)

## Emusim web testing

* Navigate to `Emusim.Web.Start.AndroidEmusimTests`
* Try to run the android tests using command line

```bash
cd demo-csharp\DotnetCore\Sauce.Demo
dotnet test .\Core.Selenium.Examples --filter TestCategory=android-end
```

🎖The tests should pass

👁Let's take a look at what's going on in this test?

## More resources

We covered so much in a short period of time! Here are some more important resources

ℹ️ [Mobile automation training resources](https://github.com/saucelabs-training/demo-java/blob/master/TRAINING.md#mobile-automation-with-appium)
ℹ️ [Testing best practices](https://github.com/nadvolod/testing-best-practices)
