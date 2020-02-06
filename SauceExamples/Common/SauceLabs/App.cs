using System;

namespace Common.SauceLabs
{
    public class App
    {
        public string SampleAppAndroid =>
            Environment.GetEnvironmentVariable("RDC_SAUCE_DEMO_ANDROID_KEY", EnvironmentVariableTarget.User);
        public string VodQaReactNative =>
            Environment.GetEnvironmentVariable("VODQC_RDC_API_KEY", EnvironmentVariableTarget.User);
        public string SauceDemoOnMobileBrowser =>
            Environment.GetEnvironmentVariable(
                "SAUCE_DEMO_MOBILE_WEB_RDC_API_KEY", EnvironmentVariableTarget.User);
    }
}
