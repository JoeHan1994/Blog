using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.WebApi.Utility._Filter
{
    //FilterContain: Function/Expection/Source/Authorization
    public class CustomResourceFilterAttribute : Attribute, IResourceFilter
    {
        private readonly IMemoryCache _memoryCache;
        public CustomResourceFilterAttribute(IMemoryCache memoryCache)
        {
            _memoryCache = _memoryCache;
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            string path = context.HttpContext.Request.Path; // api/test/getcache
            string route = context.HttpContext.Request.QueryString.Value; //?name=name
            string key = path + route;// api/test/getcache?name=name
            _memoryCache.Set(key,context.Result);
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            string path = context.HttpContext.Request.Path; // api/test/getcache
            string route = context.HttpContext.Request.QueryString.Value; //?name=name
            string key = path + route;// api/test/getcache?name=name
            if(_memoryCache.TryGetValue(key,out object value))
            {
                context.Result = (Microsoft.AspNetCore.Mvc.IActionResult)value;
            }
        }
    }
}
