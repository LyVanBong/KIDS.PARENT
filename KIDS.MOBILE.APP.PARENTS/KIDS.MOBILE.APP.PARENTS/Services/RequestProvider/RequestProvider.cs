﻿using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.RequestProvider;
using KIDS.MOBILE.APP.PARENTS.Models.Response;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.Services.RequestProvider
{
    class RequestProvider : IRequestProvider
    {
        private IRestClient _client;
        private IRestRequest _request;
        public RequestProvider()
        {
            _client = new RestClient();
        }
        public async Task<ResponseModel<T>> GetAsync<T>(string uri,IReadOnlyCollection<RequestParameter> parameters = null)
        {
            try
            {
                CreateClients(uri);
                if(parameters != null && parameters.Any())
                {
                    foreach(var item in parameters)
                    {
                        _request.AddQueryParameter(item.Key, item.Value);
                    }
                }
                var response = await _client.ExecuteAsync<ResponseModel<T>>(_request);
                var data = response.StatusCode == HttpStatusCode.OK ? JsonConvert.DeserializeObject<ResponseModel<T>>(response.Content) : default;
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void CreateClients(string uri, Method method = Method.GET)
        {
            try
            {
                uri = AppConstants.UrlApiApp + uri;
                _client.BaseUrl = new Uri(uri);
                _client.Timeout = 10000;
                _request = new RestRequest(method);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseModel<T>> PostAsync<T>(string uri, IReadOnlyCollection<RequestParameter> parameters)
        {
            try
            {
                CreateClients(uri, Method.POST);
                if (parameters != null && parameters.Any())
                {
                    foreach (var item in parameters)
                    {
                        _request.AddParameter(item.Key, item.Value);
                    }
                }

                var response = await _client.ExecuteAsync<ResponseModel<T>>(_request);
                var data = response.StatusCode == HttpStatusCode.OK
                    ? JsonConvert.DeserializeObject<ResponseModel<T>>(response.Content)
                    : default;
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseModel<T>> PutAsync<T>(string uri, IReadOnlyCollection<RequestParameter> parameters)
        {
            try
            {
                CreateClients(uri, Method.PUT);
                if (parameters != null && parameters.Any())
                {
                    foreach (var item in parameters)
                    {
                        _request.AddQueryParameter(item.Key, item.Value);
                    }
                }

                var response = await _client.ExecuteAsync<ResponseModel<T>>(_request);
                var data = response.StatusCode == HttpStatusCode.OK
                    ? JsonConvert.DeserializeObject<ResponseModel<T>>(response.Content)
                    : default;
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseModel<T>> DeleteAsync<T>(string uri, IReadOnlyCollection<RequestParameter> parameters)
        {
            try
            {
                CreateClients(uri, Method.DELETE);
                if (parameters != null && parameters.Any())
                {
                    foreach (var item in parameters)
                    {
                        _request.AddQueryParameter(item.Key, item.Value);
                    }
                }

                var response = await _client.ExecuteAsync<ResponseModel<T>>(_request);
                var data = response.StatusCode == HttpStatusCode.OK
                    ? JsonConvert.DeserializeObject<ResponseModel<T>>(response.Content)
                    : default;
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
