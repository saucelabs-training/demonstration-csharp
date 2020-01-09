using System;
using OpenQA.Selenium.Remote;
using RestSharp;

namespace Common
{
    public class Rdc
    {
        public void UpdateTestStatus(bool isTestPassed, SessionId sessionId)
        {
            var client = new RestClient
            {
                BaseUrl = new Uri("https://app.testobject.com/api/rest")
            };
            var request = new RestRequest($"/v2/appium/session/{sessionId}/test", Method.PUT)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(new { passed = isTestPassed });
            client.Execute(request);
        }
    }
}