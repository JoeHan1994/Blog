using AutoMapper;
using Blog.IService;
using Blog.Model;
using Blog.Model.Dto;
using Blog.WebApi.Utility._MD5;
using Blog.WebApi.Utility.APIResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WriterInfoController : ControllerBase
    {
        private readonly IWriterInfoService _writerInfoService;
        private readonly IMapper _mapper;
        public WriterInfoController(IWriterInfoService writerInfoService, IMapper mapper)
        {
            this._writerInfoService = writerInfoService;
            this._mapper = mapper;
        }
        [HttpPost("Create")]
        public async Task<APIResult> Create(string name, string username, string userpwd)
        {
            WriterInfo writerInfo = new WriterInfo()
            {
                Name = name,
                UserName = username,
                UserPwd = MD5Helper.MD5Encrypt32(userpwd)
            };
            var oldWriter = await _writerInfoService.FindAsync(c=>c.UserName==username);
            if (oldWriter!=null)
            {
                return APIResultHelper.Error("User have existed.");
            }
            bool isCreated = await _writerInfoService.CreateAsync(writerInfo);
            if (!isCreated) return APIResultHelper.Error("Create failed");
            return APIResultHelper.Success(writerInfo);
        }
        [HttpPut("Edit")]
        public async Task<APIResult> Edit(string name)
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var writerinfo = await _writerInfoService.FindAsync(id);
            writerinfo.Name = name;
            var isEdit = await _writerInfoService.EditAsync(writerinfo);
            if (!isEdit) return APIResultHelper.Error("Edit failed");
            return APIResultHelper.Success(writerinfo);
        }
        [HttpGet("FindWriter")]
        public async Task<APIResult> FindWriter(int id)
        {
            var writer = await _writerInfoService.FindAsync(id);
            var writerDto = _mapper.Map<WriterDto>(writer);
            return APIResultHelper.Success(writerDto);
        }
    }
}
