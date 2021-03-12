using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.WebApi.Utility.APIResult
{
    public static class APIResultHelper
    {
        public static APIResult Success(dynamic data)
        {
            return new APIResult
            {
                Code = 200,
                data = data,
                Msg = "Succeeded",
                Total = 0
            };
        }
        public static APIResult Success(dynamic data, RefAsync<int> total)
        {
            return new APIResult
            {
                Code = 200,
                data = data,
                Msg = "Succeeded",
                Total = total
            };
        }
        public static APIResult Error(string msg)
        {
            return new APIResult
            {
                Code = 500,
                data = null,
                Msg = msg,
                Total = 0
            };
        }
    }
}
