using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using PizzaData.models;
using PizzaData.Rest;
using RestService.Helpers;
using RestSharp;
using UIKit;

namespace PizzaOut.DataManager
{
    public class RestActions
    {
        private static RestServiceFactory _rest;

        private static RestActions _restActions;

        private static object myObject = new object();

        private static IRestResponse restResponse;

        /*public static RestActions GetInstance()
        {
            lock (myObject)
            {
                if (_restActions == null)
                {
                    _restActions = new RestActions();

                }
                return _restActions;
            }
        }*/

        /// <summary>
        /// Log in the user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<User> LoginUserRest(string username, string password)
        {
            User user = new User
            {
                USERNAME = username,
            
                PASSWORD = password,
            };

            Dictionary<string, object> userRegisterPost = new Dictionary<string, object>
            {
                {"USER_NAME", user.USERNAME},
                {"PASSWORD", user.PASSWORD},
            };


            restResponse = await _rest.PostRequest("v1/users/login", userRegisterPost);

            var userModel = ObjectBuilder.BuildUserObject(restResponse);

            return userModel;

        }
    }
}