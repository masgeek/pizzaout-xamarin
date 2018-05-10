using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Microsoft.AppCenter.Crashes;
using PizzaData.models;
using UIKit;

namespace PizzaOut.DataManager
{
    public class UserSession
    {
        private static NSUserDefaults _user;
        public static void  SetUserSession(User userModel)
        {
            try
            {
                if (userModel != null)
                {
                    _user = NSUserDefaults.StandardUserDefaults;
                    _user.SetInt(userModel.USER_ID, "USER_ID");
                    _user.SetString(userModel.USER_TYPE, "USER_TYPE");
                    _user.SetString(userModel.USER_NAME, "USER_NAME");
                    _user.SetString(userModel.SURNAME, "SURNAME");
                    _user.SetString(userModel.EMAIL, "EMAIL");
                    _user.SetString(userModel.MOBILE, "MOBILE");
                    _user.SetString(userModel.API_TOKEN, "API_TOKEN");
                    _user.SetString(userModel.HELPLINE, "HELPLINE");
                    _user.Synchronize();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// Get the id of the logged in user
        /// </summary>
        /// <returns></returns>
        public static int GetUserId()
        {
            nint user_id = NSUserDefaults.StandardUserDefaults.IntForKey("USER_ID");

            return Int32.Parse(user_id.ToString());
        }

        public static bool IsLoggedIn()
        {
            var userLoggedIn = GetUserId();
            return userLoggedIn > 0;
        }

        /// <summary>
        /// Logout the user and clear nsdefaults values
        /// </summary>
        public static void UserLogout()
        {
            try
            {
                _user.RemoveObject("USER_ID");
                _user.RemoveObject("USER_TYPE");
                _user.RemoveObject("USER_NAME");
                _user.RemoveObject("SURNAME");
                _user.RemoveObject("EMAIL");
                _user.RemoveObject("MOBILE");
                _user.RemoveObject("API_TOKEN");
                _user.RemoveObject("HELPLINE");
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        public static string HelpLine()
        {
            var helpline = NSUserDefaults.StandardUserDefaults.StringForKey("HELPLINE");
            return helpline;
        }
    }
}