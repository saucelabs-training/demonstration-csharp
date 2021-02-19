using System;

namespace Common.SauceLabs
{
    public class App
    {
        public string SampleAppIOS => Environment.GetEnvironmentVariable("SAUCE_DEMO_IOS_RDC_API_KEY", EnvironmentVariableTarget.User);

        public string SampleAppAndroid =>
            Environment.GetEnvironmentVariable("RDC_SAUCE_DEMO_ANDROID_KEY", EnvironmentVariableTarget.User);

        public static string SauceDemoIosAppFileName => "iOS.RealDevice.Sample.ipa";
        public string SampleAppAndroidFileName = "Android.SauceLabs.Mobile.Sample.app.2.7.0.apk";

        public static string SauceDemoIosSimulatorAppFileName = "iOS.Simulator.SauceLabs.Mobile.Sample.app.2.7.0.zip";
    }
}
