using Blog.IService;
using Blog.Model;
using Blog.WebApi.Utility.APIResult;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogNewsController : ControllerBase
    {
        private readonly IBlogNewsService _blogNewsService;
        public BlogNewsController(IBlogNewsService blogNewsService)
        {
            this._blogNewsService = blogNewsService;
        }

        [HttpGet("BlogNews")]
        public async Task<ActionResult<APIResult>> GetBlogNews()
        {
            var data = await _blogNewsService.QueryAsync();
            if(data == null)
            {
                return APIResultHelper.Error("Not Found any data");
            }
            return APIResultHelper.Success(data);
        }
        [HttpPost("Create")]
        public async Task<ActionResult<APIResult>> Create(string title, string content, int typeId)
        {
            BlogNews blogNews = new BlogNews()
            {
                BrowseCount = 0,
                LikeCount = 0,
                Time = DateTime.Now,
                Title = title,
                Content = content,
                TypeId = typeId,
                WriterId = 1
            }; 
            var data = await _blogNewsService.CreateAsync(blogNews);
            if (!data)
            {
                return APIResultHelper.Error("Add failed");
            }
            return APIResultHelper.Success(data);
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult<APIResult>> Delete(int Id)
        {
            var data = await _blogNewsService.DeleteAsync(Id);
            if (!data)
            {
                return APIResultHelper.Error("Deleted failed");
            }
            return APIResultHelper.Success(data);
        }
        [HttpPut("Edit")]
        public async Task<ActionResult<APIResult>> Edit(int Id,string title, string content, int typeId)
        {
            var blogNews = await _blogNewsService.FindAsync(Id);
            if (blogNews == null) return APIResultHelper.Error("Not Found this BlogNews");
            blogNews.Title = title;
            blogNews.Content = content;
            blogNews.TypeId = typeId;
            var data = await _blogNewsService.EditAsync(blogNews);
            if (!data)
            {
                return APIResultHelper.Error("Edit failed");
            }
            return APIResultHelper.Success(data);
        }
    }
}
