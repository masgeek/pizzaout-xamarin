using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestService.models;
using RestSharp;
namespace RestService
{
    /// <summary>
    /// RestService s = RestService.Instance;
    /// </summary>
    public sealed class RestServiceFactory
    {
        private static RestServiceFactory myObj;
        private static String api_token, user_id;
        private string baseUrl = Helper.API_URL();
        private string url;
        private string endpoint;

        RestClient client;
        RestRequest request;

        RestServiceFactory()
        {
            // Initialize.
            client = new RestClient(baseUrl);
        }

        public static RestServiceFactory Instance { get; } = new RestServiceFactory();


        public string SetEndPoint(string endpoint)
        {
            url = baseUrl + endpoint;
            return url;
        }

        #region Define the verb methods and actions
        public async Task<IRestResponse> GetRequest(string endpointRoot)
        {

            IRestResponse restResponse = null;
            var cancellationTokenSource = new CancellationTokenSource();
            request = new RestRequest(SetEndPoint(endpointRoot), Method.GET);
            if (client != null)
            {
                restResponse = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);

            }

            return restResponse;
        }

        public void PostRequest(string endpoint)
        {
            request = new RestRequest(endpoint, Method.POST);
        }

        public void PutRequest(string endpoint)
        {
            request = new RestRequest(endpoint, Method.PUT);
        }

        public void DeleteRequest(string endpoint)
        {
            request = new RestRequest(endpoint, Method.DELETE);
        }
        #endregion
    }
}
