using OpenQA.Selenium.Remote;

namespace Common.SauceLabs
{
    public class SauceSetup
    {
        private DesiredCapabilities _desiredCaps;
        public string UserName { get; private set; }
        public string AccessKey { get; private set; }

        public SauceSetup()
        {
        }

        public DesiredCapabilities DesiredCaps { get => _desiredCaps; set => _desiredCaps = value; }

        public DesiredCapabilities GetDesiredCapabilities()
        {
            _desiredCaps = new DesiredCapabilities();
            UserName = SauceUser.Name;
            AccessKey = SauceUser.AccessKey;
            return DesiredCaps;
        }
    }
}