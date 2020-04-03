using System;
using OpenQA.Selenium.Remote;
using RestSharp;
using RestSharp.Authenticators;

namespace Common.SauceLabs
{
    public class EmusimAPI
    {
        public void UpdateTestStatus(bool isTestPassed, SessionId sessionId)
        {
            //API Docs: https://wiki.saucelabs.com/display/DOCS/Job+Methods#JobMethods-UpdateJob

            var client = new RestClient
            {
                BaseUrl = new Uri("https://saucelabs.com/rest/v1"),
                Authenticator = new HttpBasicAuthenticator(SauceUser.Name, SauceUser.AccessKey)
            };
            var request = new RestRequest($"/{SauceUser.Name}/jobs/{sessionId}", Method.PUT)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(new { passed = isTestPassed });
            var response = client.Execute(request);
        }
    }
}