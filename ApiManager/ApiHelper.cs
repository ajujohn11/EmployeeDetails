using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ApiManager
{
    public class ApiHelper
    {
        private readonly HttpClient _apiclient;
        private readonly string _baseUri = "https://gorest.co.in/public-api/";
        private readonly string _token = "fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56";

        /// <summary>
        /// 
        /// </summary>
        public HttpClient ApiClient 
        { 
            get
            {
                return _apiclient;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ApiHelper()
        {
            _apiclient = new HttpClient
            {
                BaseAddress = new Uri(_baseUri)
            };
            _apiclient.DefaultRequestHeaders.Accept.Clear();
            _apiclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiclient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
        }
    }
}
