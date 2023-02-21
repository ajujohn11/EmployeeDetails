using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiManager
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiPagedResult<T> : ApiResponse
    {
        public Metadata meta { get; set; }
        public List<T> data { get; set; }
        public string ErrorData { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Metadata
    {
        public Pageinfo pagination { get; set; }
    }

}
