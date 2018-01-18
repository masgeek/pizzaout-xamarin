using System;

namespace RestService
{
    public class Helper
    {
        public static string SCHEME = "http://";
        private static string BASE_URL = "pizzaout.so/"; //live url
        //private static string BASE_URL = "pizza.tsobu.co.ke/"; //test url
                        
        private static string API_ENDPOINT = "api/";

        /// <summary>
        /// Create the full api enpoint url
        /// </summary>
        /// <returns></returns>
        public static string API_URL()
        {
            return SCHEME + BASE_URL + API_ENDPOINT;
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
    }
}
