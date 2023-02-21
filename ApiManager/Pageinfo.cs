using System;
using System.Collections.Generic;
using System.Text;

namespace ApiManager
{
    /// <summary>
    /// 
    /// </summary>
    public class Pageinfo
    {
        public Pageinfo()
        {
            page = 1;
        }
        public int total { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public int pages { get; set; }
    }
}
