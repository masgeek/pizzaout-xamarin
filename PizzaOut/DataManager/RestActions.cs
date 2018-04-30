using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using PizzaData.Helpers;
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

        private RestActions _restActions;

       // private static object myObject = new object();

        private static IRestResponse _restResponse;


        public RestActions()
        {
            _rest = new RestServiceFactory();
        }
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
        public async Task<User> LoginUserRest(string username, string password)
        {
            User user = new User
            {
                USER_NAME = username,

                PASSWORD = password,
            };

            Dictionary<string, object> loginPost = new Dictionary<string, object>
            {
                {"USER_NAME", user.USER_NAME},
                {"PASSWORD", user.PASSWORD},
            };


            _restResponse = await _rest.PostRequest("v1/users/login", loginPost);

            User userModel =  ObjectBuilder.BuildUserObject(_restResponse);

           return userModel;

        }

        public async Task<List<MenuCategory>> GetMenuCategories()
        {
            _restResponse = await _rest.GetRequest("v1/menucategories");

            List<MenuCategory> menuCategoryList = ObjectBuilder.BuildMenuCategoryList(_restResponse);

            return menuCategoryList;

        }
    }
}