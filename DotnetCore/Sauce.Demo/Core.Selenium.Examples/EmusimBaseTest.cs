namespace Core.Selenium.Examples
{
    public class EmusimBaseTest : AllTestsBase
    {
        public string DeviceName;
        public string PlatformVersion;

        public EmusimBaseTest(string deviceName, string platformVersion)
        {
            DeviceName = deviceName;
            PlatformVersion = platformVersion;
        }
    }
}