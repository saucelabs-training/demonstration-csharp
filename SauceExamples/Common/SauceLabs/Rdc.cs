using System;
using System.Diagnostics;
using OpenQA.Selenium.Remote;
using RestSharp;

namespace Common.SauceLabs
{
    public class Rdc
    {
        public void UpdateTestStatus(bool isTestPassed, SessionId sessionId)
        {
            //API Docs for Legacy RDC: https://api.testobject.com/

            var client = new RestClient
            {
                BaseUrl = new Uri("https://app.testobject.com/api/rest")
            };
            var request = new RestRequest($"/v2/appium/session/{sessionId}/test", Method.PUT);
            var body = new { passed = isTestPassed };
            Trace.WriteLine("isPassed=>" + isTestPassed);
            request.AddJsonBody(body);
            var response = client.Execute(request);
        }
    }
}