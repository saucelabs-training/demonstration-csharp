using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Globalization;
using Common.SauceLabs;

namespace Common
{
    public class WebDriverFactory
    {
        private SauceLabsCapabilities _sauceCustomCapabilities;
        private DesiredCapabilities _desiredCapabilities;
        private string sauceHubUrl = new SauceLabsEndpoint().SauceHubUrl;

        public string SeleniumHubUrl
        {
            get
            {
                return sauceHubUrl;
            }
            set
            {
                sauceHubUrl = value;
            }
        }

        public WebDriverFactory()
        {
            _sauceCustomCapabilities = new SauceLabsCapabilities();
            _desiredCapabilities = new DesiredCapabilities();
        }

        public WebDriverFactory(SauceLabsCapabilities sauceConfig)
        {
            _sauceCustomCapabilities = sauceConfig;
            _desiredCapabilities = new DesiredCapabilities();
        }
        public IWebDriver CreateSauceDriver(string testCaseName)
        {
            SetVMCapabilities("chrome", "latest", "Windows 10");
            return SetSauceCapabilities(testCaseName, _desiredCapabilities);
        }
        public IWebDriver CreateSauceDriver(string browser, string browserVersion, string osPlatform)
        {
            return CreateSauceDriver(browser, browserVersion, osPlatform, _sauceCustomCapabilities);
        }
        public RemoteWebDriver CreateSauceDriver(
            string browser, string browserVersion, string osPlatform, SauceLabsCapabilities sauceConfiguration)
        {
            var userName = SauceUser.Name;
            var accessKey = SauceUser.AccessKey;
            if (sauceConfiguration.IsHeadless)
            {
                SeleniumHubUrl = new SauceLabsEndpoint().HeadlessSeleniumUrl;
            }
            SetUserAndKey(userName, accessKey);
            SetVMCapabilities(browser, browserVersion, osPlatform);
            //an important flag to set for Edge and possibly Safari
            _desiredCapabilities.SetCapability("avoidProxy", true);
            _desiredCapabilities = SetDebuggingCapabilities(_desiredCapabilities);
            _desiredCapabilities.SetCapability("build", SauceLabsCapabilities.BuildName);
            //_desiredCapabilities.SetCapability("tunnelIdentifier", "NikolaysTunnel");
            return GetSauceRemoteDriver();
        }
        private void SetUserAndKey(string userName, string accessKey)
        {
            _desiredCapabilities.SetCapability("username", userName);
            _desiredCapabilities.SetCapability("accessKey", accessKey);
        }
        private void SetVMCapabilities(string browser, string browserVersion, string osPlatform)
        {
            _desiredCapabilities.SetCapability(CapabilityType.BrowserName, browser);
            _desiredCapabilities.SetCapability(CapabilityType.Version, browserVersion);
            _desiredCapabilities.SetCapability(CapabilityType.Platform, osPlatform);
        }

        private RemoteWebDriver GetSauceRemoteDriver()
        {
            return new RemoteWebDriver(new Uri(SeleniumHubUrl),
                _desiredCapabilities, TimeSpan.FromSeconds(600));
        }
        private IWebDriver SetSauceCapabilities(string testCaseName, DesiredCapabilities capabilities)
        {
            _desiredCapabilities = capabilities;
            SetUserAndKey(SauceUser.Name, SauceUser.AccessKey);

            //CUSTOM SAUCE CAPABILITIES
            //These capabilities are excellent for debugging and make it much easier.
            //However, if your tests are pretty stable and you want faster tests, disable all the debugging features
            //capabilities.SetCapability("extendedDebugging", true);
            //capabilities.SetCapability("recordVideo", false);
            //capabilities.SetCapability("videoUploadOnPass", false);
            //capabilities.SetCapability("recordScreenshots", false);
            _desiredCapabilities.SetCapability("build", $"SauceExamples-{DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
            var tags = new List<string> { "withDebugging", "automationGroupName1", "automationGroupName2" };
            _desiredCapabilities.SetCapability("tags", tags);
            //capabilities.SetCapability("tunnelIdentifier", "NikolaysTunnel");

            //SAUCE TIMEOUT CAPABILITIES
            SetSauceTimeouts();
            var driver = GetSauceRemoteDriver();
            new SauceJavaScriptExecutor(driver).SetTestName(testCaseName);
            return driver;
        }


        private void SetSauceTimeouts()
        {
            //How long is a test allowed to run?
            _desiredCapabilities.SetCapability("maxDuration", 3600);
            //Selenium crash might hang a command, this is the max time allowed to wait for a Selenium command
            //Keep this low, no reason to wait around a long time for a hanging command to fail
            _desiredCapabilities.SetCapability("commandTimeout", 60);
            //How long can the browser wait before a new command?
            _desiredCapabilities.SetCapability("idleTimeout", 1000);
        }

        private DesiredCapabilities SetDebuggingCapabilities(DesiredCapabilities capabilities)
        {
            //These capabilities are excellent for debugging and make it much easier.
            //However, if your tests are pretty stable and you want faster tests, disable all the debugging features
            if (_sauceCustomCapabilities.IsDebuggingEnabled)
            {
                capabilities =
                    SetDebuggingForHeadless(_sauceCustomCapabilities.IsHeadless, capabilities);
                capabilities = 
                    SetDebuggingForNonHeadless(_sauceCustomCapabilities.IsHeadless, capabilities);
                _sauceCustomCapabilities.Tags.Add("withDebuggingEnabled");
                return capabilities;
            }

            capabilities.SetCapability("extendedDebugging", false);
            capabilities.SetCapability("recordVideo", false);
            capabilities.SetCapability("videoUploadOnPass", false);
            capabilities.SetCapability("recordScreenshots", false);
            _sauceCustomCapabilities.Tags.Add("withDebuggingDisabled");
            return capabilities;
        }

        private DesiredCapabilities SetDebuggingForNonHeadless(bool isHeadless, DesiredCapabilities capabilities)
        {
            if (isHeadless) return capabilities;
            capabilities.SetCapability("extendedDebugging", true);
            capabilities.SetCapability("recordVideo", true);
            capabilities.SetCapability("videoUploadOnPass", true);
            capabilities.SetCapability("recordScreenshots", true);
            return capabilities;
        }

        private DesiredCapabilities SetDebuggingForHeadless(bool isHeadless, DesiredCapabilities capabilities)
        {
            if (!isHeadless)
                return capabilities;
            capabilities.SetCapability("recordScreenshots", true);
            return capabilities;
        }
    }
}
