using System;

namespace Core.BestPractices.Web
{
    public static class Constants
    {
        public static string BaseUrl => "https://www.saucedemo.com";

        public static string BuildId { get => buildId; set => buildId = value; }

        private static string buildId = DateTime.Now.ToString("F");
    }
}