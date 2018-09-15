using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Plugin.Connectivity;
using UIKit;

namespace PizzaOut.IOS.DataManager
{
    public class AppHelper
    {
        /// <summary>
        /// Do we have internet connection.?
        /// </summary>
        /// <returns><c>true</c>, if internet connection is there, <c>false</c> otherwise.</returns>
        public static bool HasInternetConnection()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        /// <summary>
        /// Is the network available.
        /// </summary>
        /// <returns><c>true</c>, if network is available, <c>false</c> otherwise.</returns>
        /// <param name="testHost">Test host.</param>
        public static bool IsNetworkAvailable(string testHost = "http://google.com")
        {
            //Via Reachability  
            if (HasInternetConnection())
            {
                return Reachability.IsHostReachable(testHost);
            }
            return false;
        }
    }
}