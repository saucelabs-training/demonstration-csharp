# C# Sauce Labs Demo Scripts and Frameworks
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/728698e058a04148a6a0da043ef7be1e)](https://app.codacy.com/gh/saucelabs-training/demo-csharp?utm_source=github.com&utm_medium=referral&utm_content=saucelabs-training/demo-csharp&utm_campaign=Badge_Grade_Dashboard)

This directory contains example scripts and dependencies for running automated Selenium tests on Sauce Labs using C#.

## 🖥 Web automation

* 🥇Best practice framework for web testing
  * [Production-level framework doing web automation on desktop and mobile. Using best practices, parallel, cross-browser, NUnit, Selenium, Appium, Visual](./DotnetCore/Sauce.Demo/Core.BestPractices.Web)
  
* Code Examples
  * Dotnet Core
    * [Simple W3C Selenium test](./DotnetCore/Sauce.Demo/Core.Selenium.Examples/SimpleSauceTests.cs)
  * Dotnet Framework
    * [Quick start with Selenium](./SauceExamples/SeleniumNunit/SimpleExamples/SimpleSauceTest.cs)
    * [Parallel, cross-browser, NUnit, Selenium](./SauceExamples/Web.Tests/BestPractices/test)
    * [Selenium W3C examples](./SauceExamples/Selenium4DotNetFramework/Selenium4SauceTests.cs)
    * [CICD with Azure DevOps](https://ultimateqa.com/tfs-vsts-and-azure-devops/#Sauce_Labs_with_Azure_DevOps)
    * [Examples,Selenium,MsTest](./SauceExamples/SeleniumMsTest)
    * [Sauce Labs REST API](./SauceExamples/SeleniumNunit/SimpleExamples/RestApiForVdc.cs)
    * [Set pass/fail status,Selenium,NUnit](https://github.com/saucelabs-training/demo-csharp/blob/5d7e8731e4120ae381d8ff14bcf58d672b3bc2fc/SauceExamples/Web.Tests/BestPractices/test/BaseTest.cs#L60)
    * [Set pass/fail status,Selenium,MsTest](https://github.com/saucelabs-training/demo-csharp/blob/5d7e8731e4120ae381d8ff14bcf58d672b3bc2fc/SauceExamples/SeleniumMsTest/ParallelTests/DataDriven/DataDrivenCrossBrowserParallelMethods.cs#L84)
    * [Visual E2E](./SauceExamples/SeleniumNunit/Visual)
    * [Front-end perf testing examples](./SauceExamples/SeleniumNunit/SaucePerformance/PerformanceDemo.cs)
    * [Performance testing and nework throttling](./SauceExamples/SeleniumNunit/SaucePerformance/CustomCapabilitiesTests.cs)


## 📱Mobile automation

### [📕 Mobile Testing Training Tutorials](https://github.com/saucelabs-training/demo-java/blob/master/TRAINING.md)

* Examples
  * Emusim
    * Web
      * [Web test on emusim w/ Android](./SauceExamples/Core.Appium.MsTest.Scripts/Emusim/Browser/AndroidWebTests.cs)
    * Native app
      * [Native app tests](./SauceExamples/DotnetFramework/Appium/Appium4.NUnit.Scripts/RealDevices/NativeApp/UP)
      * Legacy RDC **❗️Deprecated! Use Unified Platform**
        * [Native app tests on legacy RDC](./SauceExamples/Core.Appium.MsTest.Scripts/RealDevices/NativeApp/LegacyRdc)
    
  * Real Devices
    * [Native App, IOS](./SauceExamples/DotnetFramework/Appium/Appium4.NUnit.Scripts/RealDevices/NativeApp/UP/GetStartedIos.cs)
    * [Native App, Android](./SauceExamples/DotnetFramework/Appium/Appium4.NUnit.Scripts/RealDevices/NativeApp/UP/AndroidAdvanced.cs)
    * [Specflow,MsTest in Parallel](./SauceExamples/DotnetCore/Core.Selenium4.MsTest.Scripts/SpecFlow)
    * [Download test assets from real devices](./SauceExamples/Core.Appium.MsTest.Scripts/RealDevices/NativeApp/DownloadAssets.cs)
    * Legacy RDC **❗️Deprecated! Use Unified Platform**
      * [Legacy RDC REST API status updates](./SauceExamples/Core.Appium.MsTest.Scripts/RealDevices/NativeApp/LegacyRdc/iOSExamples.cs)

* Automation best practices
  * [Mobile automation framework,Nunit,.NET Core](./SauceExamples/Core.Appium.MsTest.BestPractices)

## Parallelization capabilities of unit testing libraries

How do different libraries parallelize tests?

|MsTest|NUnit|xUnit|SpecFlow|
|:-------------:|:-------------:|:-------------:|:-------------:|
|Test method|Test class|Test class|Test class|

## 🚀 CICD

**Important**
> **Azure DevOps Sauce Labs Plugin does NOT work!** You cannot view Sauce Labs videos inside of Azure DevOps. You need to go to saucelabs.com to view the test assets. Furthermore, you can add richer logging to your tests and add test asset links directly into the logs (but this is extra work).


[How to configure Azure DevOps with Sauce Labs](https://ultimateqa.com/tfs-vsts-and-azure-devops/#Sauce_Labs_with_Azure_DevOps)

