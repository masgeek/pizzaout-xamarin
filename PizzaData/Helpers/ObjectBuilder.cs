using System;
using System.Collections.Generic;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PizzaData.models;
using RestSharp;

namespace PizzaData.Helpers
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
                Crashes.TrackError(ex);
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


                if ((token is JArray))
                {
                    userObject = new User
                    {
                        HAS_ERRORS = true,
                        ERROR_LIST = JsonConvert.DeserializeObject<List<ErrorModel>>(response.Content)
                    };
                }
                else
                {
                    userObject = JsonConvert.DeserializeObject<User>(response.Content);
                    userObject.HAS_ERRORS = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
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


                var j = token;
              //Analytics.TrackEvent(token.ToString());
                if (!(token is JObject)) return null;
                result = JsonConvert.DeserializeObject<CartItem>(response.Content);
            }
            catch (Exception ex)
            {
                Analytics.TrackEvent(ex.Message);
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

        #region Delivery location, time and date
        public static List<Location> BuildLocationList(IRestResponse response)
        {
            List<Location> result = null;

            try
            {
                var token = JToken.Parse(response.Content); //validate if its object or array

                if (!(token is JArray)) return null;


                result = JsonConvert.DeserializeObject<List<Location>>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public static List<DeliveryTime> BuildDeliveryTimeList(IRestResponse response)
        {
            List<DeliveryTime> result = null;

            try
            {
                var token = JToken.Parse(response.Content); //validate if its object or array

                if (!(token is JArray)) return null;


                result = JsonConvert.DeserializeObject<List<DeliveryTime>>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
        #endregion
    }
}
