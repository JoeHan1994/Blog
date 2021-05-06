using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.JWT.Utility.APIResult
{
    public class ApiResult
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public Result Result { get; set; }
    }

    public class Result
    {
        public int Total { get; set; }
        public dynamic data { get; set; }
    }
}
