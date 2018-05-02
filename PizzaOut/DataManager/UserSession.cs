using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace PizzaOut.DataManager
{
    public class UserSession
    {
        /// <summary>
        /// Get the id of the logged in user
        /// </summary>
        /// <returns></returns>
        public static string GetUserId()
        {
            return "10";
        }

        public static bool IsLoggedIn()
        {
            return true;
        }
    }
}