# C# Sauce Labs Demo Scripts and Frameworks
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/728698e058a04148a6a0da043ef7be1e)](https://app.codacy.com/gh/saucelabs-training/demo-csharp?utm_source=github.com&utm_medium=referral&utm_content=saucelabs-training/demo-csharp&utm_campaign=Badge_Grade_Dashboard)

| RDC Automated Tests(Appium 3)|RDC,Nunit,Native Apps|
| -------------:|:-------------:|
| [![Build Status](https://dev.azure.com/nikolayadvolodkin/SauceExamples/_apis/build/status/SauceExamples-RDC?branchName=master)](https://dev.azure.com/nikolayadvolodkin/SauceExamples/_build/latest?definitionId=21&branchName=master)|[![Build status](https://dev.azure.com/nikolayadvolodkin/SauceExamples/_apis/build/status/Appium4%20NUnit%20Scripts)](https://dev.azure.com/nikolayadvolodkin/SauceExamples/_build/latest?definitionId=22)|

This directory contains example scripts and dependencies for running automated Selenium tests on Sauce Labs using C#.

## Web automation

* Popular Examples
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
* Automation best practices
  * [Production-level framework using automation best practices, parallel, cross-browser, NUnit, Selenium](./SauceExamples/Web.Tests/BestPractices)

## Mobile automation

* Popular Examples
  * Emusim
    * [Native app testing](./SauceExamples/DotnetFramework/Appium/Appium4.NUnit.Scripts/Emusim/NativeApp)
  * Real Devices
    * [Native App, IOS](./SauceExamples/DotnetFramework/Appium/Appium4.NUnit.Scripts/RealDevices/NativeApp/UP/GetStartedIos.cs)
    * [Native App, Android](./SauceExamples/DotnetFramework/Appium/Appium4.NUnit.Scripts/RealDevices/NativeApp/UP/AndroidAdvanced.cs)
    * [Legacy RDC REST API and status updates](./SauceExamples/AppiumLatestOnDotNetFramework/RealDevices/NativeApp/AndroidRdcTests.cs)
    * [Specflow,MsTest in Parallel](./SauceExamples/DotnetCore/Core.Selenium4.MsTest.Scripts/SpecFlow)

* Automation best practices
  * [Mobile automation framework,Nunit,.NET Core](./SauceExamples/Core.Appium.MsTest.BestPractices)

### Must Have Appium Resources

1. [Appium Pro newsletter by Jonathan Lipps](https://appiumpro.com/)
2. [Appium Desktop w/ local emulator](https://www.youtube.com/watch?v=0P8mkguf2z8&list=PL67l1VPxOnT5FXKf5YvGoT9TuCdSmLhv_&index=3)
3. [Appium Desktop w/ Sauce labs](https://youtu.be/IOSUBda2-g4?t=1570)
4. [Appium locator strategies](https://ultimateqa.com/getting-started-with-appium/#Element_Location_Strategies)

## Parallelization capabilities of unit testing libraries

How do different libraries parallelize tests?

|MsTest|NUnit|xUnit|SpecFlow|
|:-------------:|:-------------:|:-------------:|:-------------:|
|Test method|Test class|Test class|Test class|

## CICD

**Important**
> **Azure DevOps Sauce Labs Plugin does NOT work!** You cannot view Sauce Labs videos inside of Azure DevOps. You need to go to saucelabs.com to view the test assets. Furthermore, you can add richer logging to your tests and add test asset links directly into the logs (but this is extra work).


[How to configure Azure DevOps with Sauce Labs](https://ultimateqa.com/tfs-vsts-and-azure-devops/#Sauce_Labs_with_Azure_DevOps)

