using System;

namespace PizzaData.Helpers
{
    public class Helper
    {
#pragma warning disable 169
        //public static string UriScheme = "https://";
        public static string UriScheme = "http://";
        private static string BaseUrl = "pizzaout.so/"; //live url

        //private const string BaseUrl = "pizza.tsobu.co.ke/"; //test url
        //private static string BaseUrl = "192.168.100.4/pizza/"; //local test url
        //private static string BaseUrl = "41.89.65.170:81/pizza/"; //local test url
        //private static string BaseUrl = "192.168.126.1:81/pizza/"; //local test url


        private const string ApiEndpoint = "api/";
#pragma warning restore 169

        /// <summary>
        /// Create the full api enpoint url
        /// </summary>
        /// <returns></returns>
        public static string API_URL()
        {
            return UriScheme + BaseUrl + ApiEndpoint;
  
        }

        public static long GetTimeStamp()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        public static string Currency()
        {
            return "USD";
        }
    }
}
