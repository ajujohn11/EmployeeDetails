using System;
using System.Collections.Generic;
using System.Text;

namespace ApiManager
{
    public class ApiError
    {
        public List<ErrorInfo> data { get; set; }    
    }
    public class ErrorInfo
    {
        public string field { get; set; }
        public string message { get; set; }
    }
}
