using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Common.SauceLabs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Extensions;
using RestSharp.Serialization.Json;

namespace Core.Appium.MsTest.Scripts.RealDevices.NativeApp
{
    [TestClass]
    [TestCategory("MsTest")]
    [TestCategory("Rdc")]
    [TestCategory("Android")]

    public class DownloadAssets
    {
        private string _sessionId = "c2ed0655d1bd4ce6ab204bc52932cdbb";
        private AndroidDriver<IWebElement> _driver;
        private string JobID;
        public static string HubUrlPart => "ondemand.us-west-1.saucelabs.com/wd/hub";
        public string Url => $"https://{SauceUser}:{SauceAccessKey}@{HubUrlPart}";

        public TestContext TestContext { get; set; }
        public string SauceUser => Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);

        public string SauceAccessKey =>
            Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);

        [TestInitialize]
        public void Setup()
        {
            var capabilities = new AppiumOptions();
            //We can run on any version of the platform as long as it's the correct device
            //Make sure to pick an Android or iOS device based on your app
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Google Pixel.*");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            capabilities.AddAdditionalCapability("idleTimeout", 300);
            capabilities.AddAdditionalCapability("newCommandTimeout", 300);
            capabilities.AddAdditionalCapability("name", TestContext.TestName);
            capabilities.AddAdditionalCapability("appWaitActivity", "com.swaglabsmobileapp.MainActivity");
            capabilities.AddAdditionalCapability("noReset", "true");
            /*
             * You need to upload your own Native Mobile App to Sauce Storage!
             * https://wiki.saucelabs.com/display/DOCS/Uploading+your+Application+to+Sauce+Storage
             * You can use either storage:<app-id> or storage:filename=
             */
            var appFileName = new ApiKeys().Rdc.Apps.SampleAppAndroidFileName;
            capabilities.AddAdditionalCapability("app","storage:filename=" + appFileName);


            /*
             Getting Error: OpenQA.Selenium.WebDriverException : The HTTP request to the remote WebDriver server for URL 
            https://us1.appium.testobject.com/wd/hub/session timed out after 60 seconds.
                ----> System.Net.WebException : The operation has timed out
            Solution: Try changing to a more popular device
             */
            _driver = new AndroidDriver<IWebElement>(new Uri(Url), 
                capabilities, TimeSpan.FromSeconds(180));
            var sauceUrl = _driver.Capabilities.GetCapability("testobject_test_report_url").ToString();
            var lastSlashIndex = sauceUrl.LastIndexOf("/", StringComparison.Ordinal);
            JobID = sauceUrl.Substring(lastSlashIndex + 1);
        }


        [TestMethod]
        public void DownloadTestAssets()
        {

        }

        [TestCleanup]
        public async Task Teardown()
        {
            _driver?.Quit();


            var client = new RestClient
            {
                BaseUrl = new Uri("https://api.us-west-1.saucelabs.com/v1/rdc/"),
                Authenticator = new HttpBasicAuthenticator(SauceUser, SauceAccessKey)
            };

            var request = new RestRequest($"jobs/{JobID}", Method.GET)
            {
                RequestFormat = DataFormat.Json
            };
            var response = client.Execute(request);
            Thread.Sleep(500);
            dynamic deserializeObject = JsonConvert.DeserializeObject(response.Content);

            //Get each of the locations
            var videoUrl = deserializeObject["video_url"].Value;
            var frameworkLogUrl = deserializeObject["framework_log_url"].Value;
            var deviceLogUrl = deserializeObject["device_log_url"].Value;

            //Download each of the assets that you want. It's your job to make sure
            //to parse out a unique name for every single file that you want to save
            DownloadFile(frameworkLogUrl, "frameworkLog.txt");
            DownloadFile(deviceLogUrl, "deviceLog.txt");
            if(videoUrl != null)
                DownloadFile(videoUrl, "video.mp4");

            //client = new RestClient(videoUrl) { Authenticator = new HttpBasicAuthenticator(SauceUser, SauceAccessKey) };
            //request = new RestRequest("");
            //var response2 = await client.ExecuteTaskAsync(request);

            //var directory = @"C:\SauceResources\";
            //Directory.CreateDirectory(directory);
            //response.RawBytes.SaveAs($"{directory}video.mp4");
        }

        public void DownloadFile(string url, string fileName)
        {
            //RestClient restClient = new RestClient($@"{url}");
            var restClient = new RestClient($@"{url}") { Authenticator = new HttpBasicAuthenticator(SauceUser, SauceAccessKey) };
            var fileBytes = restClient.DownloadData(new RestRequest("#", Method.GET));
            var directory = @"C:\SauceResources\";
            File.WriteAllBytes(Path.Combine(directory, fileName), fileBytes);
        }
    }
}
