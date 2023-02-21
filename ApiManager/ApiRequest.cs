using System;
using System.Collections.Generic;
using System.Text;

namespace ApiManager
{
    public class ApiRequest
    {
        public ApiRequest()
        {
            page = 1;
            limit = 10;
        }
        public int page { get; set; }
        public int limit { get; set; }
        public int? id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string status { get; set; }
    }
}
