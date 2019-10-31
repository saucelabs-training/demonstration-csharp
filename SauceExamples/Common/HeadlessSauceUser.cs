namespace Common
{
    //TODO future version should probably inherit from an ISauceUser that forces the impl of username and accessKey
    public class HeadlessSauceUser
    {
        public string UserName => SauceUser.Name;
        public string AccessKey => SauceUser.AccessKey;
    }
}