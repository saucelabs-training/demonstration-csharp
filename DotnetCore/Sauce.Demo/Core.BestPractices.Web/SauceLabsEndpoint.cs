using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BestPractices.Web
{
    public class SauceLabsEndpoint
    {
        private static string EmusimDomain = "@ondemand.saucelabs.com:443" + "/wd/hub";
        public static string SauceUsWestDomain = "ondemand.us-west-1.saucelabs.com/wd/hub";
        public string SauceHubUrl => "https://ondemand.saucelabs.com/wd/hub";
        public Uri SauceHubUri => new Uri(SauceHubUrl);
        public Uri UsWestHubUri => new Uri($"https://{SauceUsWestDomain}");

        public string HeadlessSeleniumUrl => "https://ondemand.us-east-1.saucelabs.com/wd/hub";

        public string HeadlessRestApiUrl => "https://us-east-1.saucelabs.com/rest/v1";

        public Uri EmusimUri(string sauceUser, string sauceKey) =>
            new($"https://{sauceUser}:{sauceKey}{EmusimDomain}");
    }
}
