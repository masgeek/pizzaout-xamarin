using System;
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
        public static Orders BuildOrderObject(IRestResponse response)
        {
            Orders result = null;
            try
            {
                var token = JToken.Parse(response.Content); //validate if its object or array


                if (!(token is JObject)) return null;


                result = JsonConvert.DeserializeObject<Orders>(response.Content);
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
        public static List<Orders> BuildOrdersList(IRestResponse response)
        {
            List<Orders> result = null;

            try
            {
                var token = JToken.Parse(response.Content); //validate if its object or array

                if (!(token is JArray)) return null;


                result = JsonConvert.DeserializeObject<List<Orders>>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        #endregion Orders

        #region menu catgories

        public static MenuCategories BuildCategoryObject(IRestResponse response)
        {
            MenuCategories result = null;
            try
            {
                var token = JToken.Parse(response.Content); //validate if its object or array
           
                if (!(token is JObject)) return null;
                result = JsonConvert.DeserializeObject<MenuCategories>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return result;
        }

        public static List<MenuCategories> BuildMenuCategoryList(IRestResponse response)
        {
            List<MenuCategories> result = null;

            try
            {
                var token = JToken.Parse(response.Content); //validate if its object or array

                if (!(token is JArray)) return null;


                result = JsonConvert.DeserializeObject<List<MenuCategories>>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
        #endregion menu categories

        #region menu catgory items

        public static MenuCategoryItems BuildCategoryItemObject(IRestResponse response)
        {
            MenuCategoryItems result = null;
            try
            {
                var token = JToken.Parse(response.Content); //validate if its object or array

                if (!(token is JObject)) return null;
                result = JsonConvert.DeserializeObject<MenuCategoryItems>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return result;
        }

        public static List<MenuCategoryItems> BuildCategoryItemList(IRestResponse response)
        {
            List<MenuCategoryItems> result = null;

            try
            {
                var token = JToken.Parse(response.Content); //validate if its object or array

                if (!(token is JArray)) return null;


                result = JsonConvert.DeserializeObject<List<MenuCategoryItems>>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
        #endregion menu catgory items
    }
}
