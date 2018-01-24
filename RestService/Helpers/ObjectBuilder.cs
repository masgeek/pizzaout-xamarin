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
        public static List<Orders> BuildOrdersObjectArr(IRestResponse response)
        {
            List<Orders> result = null;

            try
            {
                var token = JToken.Parse(response.Content); //validate if its object or array

            //JArray test = null;
      
            if (!(token is JArray)) return null;
        
          
                result = JsonConvert.DeserializeObject<List<Orders>>(response.Content);
                //test = JsonConvert.DeserializeObject<JArray>(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
