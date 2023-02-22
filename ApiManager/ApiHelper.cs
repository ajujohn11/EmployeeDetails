using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ApiManager
{
    /// <summary>
    /// 
    /// </summary>
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
            //_apiclient.DefaultRequestHeaders.Accept.Clear();
            _apiclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiclient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
        }
    }
}
