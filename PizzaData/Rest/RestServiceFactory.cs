﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PizzaData.Helpers;
using RestSharp;

namespace PizzaData.Rest
{
    /// <summary>
    /// RestService s = RestService.Instance;
    /// </summary>
    public sealed class RestServiceFactory
    {
        private static RestServiceFactory myObj;
        private static String api_token, user_id;
        private readonly string _baseUrl = Helper.API_URL();
        private string url;
        //private string endpoint;

        RestClient client;
        RestRequest request;

        public RestServiceFactory()
        {
            // Initialize.
            client = new RestClient(_baseUrl);
        }

        public static RestServiceFactory Instance { get; } = new RestServiceFactory();


        public string SetEndPoint(string _endpoint)
        {
            url = _baseUrl + _endpoint;
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

        public async Task<IRestResponse> PostRequest(string endpoint, Dictionary<string, object> postObject)
        {
            IRestResponse response = null;
            var cancellationTokenSource = new CancellationTokenSource();

            request = new RestRequest(SetEndPoint(endpoint), Method.POST)
            {
                RequestFormat = DataFormat.Json,
                AlwaysMultipartFormData = true
            };

            //request.AddParameter("text/json", json, ParameterType.RequestBody);
            //request.AddJsonBody(json);
           foreach (var pair in postObject)
            {
                request.AddParameter(pair.Key, pair.Value);
            }

            if (client != null)
            {
                response = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
            }

            return response;
        }


        public async Task<IRestResponse> PutRequest(string endpoint, Dictionary<string, object> postObject)
        {
            IRestResponse response = null;
            var cancellationTokenSource = new CancellationTokenSource();

            var _endpoint = SetEndPoint(endpoint);

            request = new RestRequest(_endpoint, Method.PUT)
            {
                RequestFormat = DataFormat.Json,
                AlwaysMultipartFormData = true
            };

            //request.AddParameter("text/json", json, ParameterType.RequestBody);
            //request.AddJsonBody(json);

            foreach (var pair in postObject)
            {
                request.AddParameter(pair.Key, pair.Value);
            }

            if (client != null)
            {
                response = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
            }

            return response;
        }

        public void DeleteRequest(string endpoint)
        {
            request = new RestRequest(endpoint, Method.DELETE);
        }
        #endregion
    }
}
