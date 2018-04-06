﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestService.models;
using RestSharp;

namespace RestService.Helpers
{
    public class ObjectBuilder
    {
        #region Orders

        /// <summary>
        /// Convert json object string to class objects
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static Order BuildOrderObject(IRestResponse response)
        {
            Order result = null;
            try
            {
                var token = JToken.Parse(response.Content); //validate if its object or array


                if (!(token is JObject)) return null;


                result = JsonConvert.DeserializeObject<Order>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static List<Order> BuildOrdersList(IRestResponse response)
        {
            List<Order> result = null;

            try
            {
                var token = JToken.Parse(response.Content); //validate if its object or array

                if (!(token is JArray)) return null;


                result = JsonConvert.DeserializeObject<List<Order>>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        #endregion Orders

        #region menu catgories

        public static MenuCategory BuildCategoryObject(IRestResponse response)
        {
            MenuCategory result = null;
            try
            {
                var token = JToken.Parse(response.Content); //validate if its object or array
           
                if (!(token is JObject)) return null;
                result = JsonConvert.DeserializeObject<MenuCategory>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return result;
        }

        public static List<MenuCategory> BuildMenuCategoryList(IRestResponse response)
        {
            List<MenuCategory> result = null;

            try
            {
                var token = JToken.Parse(response.Content); //validate if its object or array

                if (!(token is JArray)) return null;


                result = JsonConvert.DeserializeObject<List<MenuCategory>>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
        #endregion menu categories

        #region menu category items

        public static MenuCategoryItem BuildCategoryItemObject(IRestResponse response)
        {
            MenuCategoryItem result = null;
            try
            {
                var token = JToken.Parse(response.Content); //validate if its object or array

                if (!(token is JObject)) return null;
                result = JsonConvert.DeserializeObject<MenuCategoryItem>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return result;
        }

        public static List<MenuCategoryItem> BuildCategoryItemList(IRestResponse response)
        {
            List<MenuCategoryItem> result = null;

            try
            {
                var token = JToken.Parse(response.Content); //validate if its object or array

                if (!(token is JArray)) return null;


                result = JsonConvert.DeserializeObject<List<MenuCategoryItem>>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
        #endregion menu catgory items

        #region Build user object
        public static User BuildUserObject(IRestResponse response)
        {
            User userObject = null;
            try
            {
                var token = JToken.Parse(response.Content); //validate if its object or array

                if (!(token is JObject)) return null;
                userObject = JsonConvert.DeserializeObject<User>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return userObject;
        }

        public static List<User> BuildUserList(IRestResponse response)
        {
            List<User> userList = null;

            try
            {
                var token = JToken.Parse(response.Content); //validate if its object or array

                if (!(token is JArray)) return null;

                userList = JsonConvert.DeserializeObject<List<User>>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return userList;
        }
        #endregion Building os user object

        #region cart items

        public static CartItem BuildCartItemObject(IRestResponse response)
        {
            CartItem result = null;
            try
            {
                var token = JToken.Parse(response.Content); //validate if its object or array

                if (!(token is JObject)) return null;
                result = JsonConvert.DeserializeObject<CartItem>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return result;
        }

        public static List<CartItem> BuildCartItemList(IRestResponse response)
        {
            List<CartItem> result = null;

            try
            {
                var token = JToken.Parse(response.Content); //validate if its object or array

                if (!(token is JArray)) return null;


                result = JsonConvert.DeserializeObject<List<CartItem>>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
        #endregion cart items
    }
}
