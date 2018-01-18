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
        /// <summary>
        /// Convert json object string to class objects
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static Orders BuildOrderObject(IRestResponse response)
        {
            var token = JToken.Parse(response.Content); //validate if its object or array

            Orders result = null;
            if (!(token is JObject)) return null;

            try
            {
                result = JsonConvert.DeserializeObject<Orders>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return result;
        }

        public static List<Orders> BuildOrdersObjectArr(IRestResponse response)
        {
            var token = JToken.Parse(response.Content); //validate if its object or array

            JArray test = null;
            List<Orders> result = null;
            if (!(token is JArray)) return null;

            try
            {
                result = JsonConvert.DeserializeObject<List<Orders>>(response.Content);
                //test = JsonConvert.DeserializeObject<JArray>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return result;
        }
    }
}
