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
        public static bool HasInternetConnection()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        public static bool IsNetworkAvailable(string testHost = "http://google.com")
        {
            //Via Reachability  
            return Reachability.IsHostReachable(testHost);
        }
    }
}