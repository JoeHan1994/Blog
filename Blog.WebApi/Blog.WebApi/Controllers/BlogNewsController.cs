using AutoMapper;
using Blog.IService;
using Blog.Model;
using Blog.Model.Dto;
using Blog.WebApi.Utility.APIResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogNewsController : ControllerBase
    {
        private readonly IBlogNewsService _blogNewsService;
        private readonly IMapper _mapper;
        public BlogNewsController(IBlogNewsService blogNewsService, IMapper mapper)
        {
            this._blogNewsService = blogNewsService;
            this._mapper = mapper;
        }

        [HttpGet("BlogNews")]
        public async Task<ActionResult<APIResult>> GetBlogNews()
        {
            var id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var data = await _blogNewsService.QueryAsync(c=>c.WriterId == id);
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
                WriterId = Convert.ToInt32(this.User.FindFirst("Id").Value)
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

        [HttpGet("BlogNewsPage")]
        public async Task<APIResult> GetBlogNewsPage( int page, int size)
        {
            RefAsync<int> total = 0;
            var blognews = await _blogNewsService.QueryAsync(page,size, total);
            try
            {
                var blognewsDto = _mapper.Map<List<BlogNewsDto>>(blognews);
                return APIResultHelper.Success(blognewsDto, total);
            }
            catch (Exception ex)
            {
                return APIResultHelper.Error("AutoMapper failed");
            }
        }
    }
}
