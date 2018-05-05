using System;

namespace RestService.Helpers
{
    public class Helper
    {
        public static string UriScheme = "https://";
        //private static string BaseUrl = "pizzaout.so/"; //live url
        private const string BaseUrl = "pizza.tsobu.co.ke/"; //test url
        //private static string BaseUrl = "192.168.100.4/pizza/"; //local test url

        private const string ApiEndpoint = "api/";

        /// <summary>
        /// Create the full api enpoint url
        /// </summary>
        /// <returns></returns>
        public static string API_URL()
        {
            return UriScheme + BaseUrl + ApiEndpoint;
        }

        /// <summary>
        /// Triger ussd dialler function
        /// </summary>
        /// <param name="ussd"></param>
        /// <returns></returns>
        private static string UssdToCallableUri(string ussd)
        {
            return ussd;
        }

        public static long GetTimeStamp()
        {
            var timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            return timeStamp;
        }
    }
}
