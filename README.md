# C# Sauce Labs Demo Scripts and Frameworks
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/728698e058a04148a6a0da043ef7be1e)](https://app.codacy.com/gh/saucelabs-training/demo-csharp?utm_source=github.com&utm_medium=referral&utm_content=saucelabs-training/demo-csharp&utm_campaign=Badge_Grade_Dashboard)

| RDC Automated Tests(Appium 3)|RDC,Nunit,Native Apps|
| -------------:|:-------------:|
| [![Build Status](https://dev.azure.com/nikolayadvolodkin/SauceExamples/_apis/build/status/SauceExamples-RDC?branchName=master)](https://dev.azure.com/nikolayadvolodkin/SauceExamples/_build/latest?definitionId=21&branchName=master)|[![Build status](https://dev.azure.com/nikolayadvolodkin/SauceExamples/_apis/build/status/Appium4%20NUnit%20Scripts)](https://dev.azure.com/nikolayadvolodkin/SauceExamples/_build/latest?definitionId=22)|

This directory contains example scripts and dependencies for running automated Selenium tests on Sauce Labs using C#.

## 🖥 Web automation

* 🥇Best practice framework for web testing
  * [Production-level framework using automation best practices, parallel, cross-browser, NUnit, Selenium](./SauceExamples/Web.Tests/BestPractices)
  
* Code Examples
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
      * [Native app tests on legacy RDC](./SauceExamples/Core.Appium.MsTest.Scripts/RealDevices/NativeApp/LegacyRdc)**❗️Deprecated! Use Unified Platform**
    
  * Real Devices
    * [Native App, IOS](./SauceExamples/DotnetFramework/Appium/Appium4.NUnit.Scripts/RealDevices/NativeApp/UP/GetStartedIos.cs)
    * [Native App, Android](./SauceExamples/DotnetFramework/Appium/Appium4.NUnit.Scripts/RealDevices/NativeApp/UP/AndroidAdvanced.cs)
    * [Legacy RDC REST API and status updates](./SauceExamples/AppiumLatestOnDotNetFramework/RealDevices/NativeApp/AndroidRdcTests.cs)
    * [Specflow,MsTest in Parallel](./SauceExamples/DotnetCore/Core.Selenium4.MsTest.Scripts/SpecFlow)

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

