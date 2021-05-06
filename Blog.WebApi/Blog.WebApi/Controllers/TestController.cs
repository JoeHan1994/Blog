using Blog.WebApi.Utility._Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("NoAuthoriza")]
        public string NoAuthoriza()
        {
            return "this is NoAuthoriza";
        }

        [Authorize]
        [HttpGet("Authoriza")]
        public string Authoriza()
        {
            return "this is Authoriza";
        }

        [TypeFilter(typeof(CustomResourceFilterAttribute))]
        [HttpGet]
        public IActionResult GetCache(string name)
        {
            return new JsonResult(new { 
                name = name,
                age = 18,
                sex = true
            });
        }
    }

}
