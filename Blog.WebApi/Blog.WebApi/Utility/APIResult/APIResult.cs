﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.WebApi.Utility.APIResult
{
    public class APIResult
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public int Total { get; set; }
        public dynamic data { get; set; }
    }
}
