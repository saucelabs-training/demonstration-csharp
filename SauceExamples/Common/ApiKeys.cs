using System;

namespace Common
{
    public class ApiKeys
    {
        public RdcKeys Rdc => new RdcKeys();
    }

    public class RdcKeys
    {
        public App Apps => new App();
    }

    public class App
    {
        public string SampleAppAndroid => 
            Environment.GetEnvironmentVariable("RDC_SAUCE_DEMO_ANDROID_KEY", EnvironmentVariableTarget.User);
        public string VodQaReactNative =>
            Environment.GetEnvironmentVariable("VODQC_RDC_API_KEY", EnvironmentVariableTarget.User);
    }
}
