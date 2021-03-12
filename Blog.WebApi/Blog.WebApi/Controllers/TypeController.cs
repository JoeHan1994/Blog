using Blog.IService;
using Blog.Model;
using Blog.WebApi.Utility.APIResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeInfoService _typeInfoService;
        public TypeController(ITypeInfoService typeInfoService)
        {
            this._typeInfoService = typeInfoService;
        }


        [HttpGet("Types")]
        public async Task<ActionResult<APIResult>> GetType()
        {
            var types = await _typeInfoService.QueryAsync();
            if (types == null)
            {
                return APIResultHelper.Error("Not Found any data");
            }
            return APIResultHelper.Success(types);
        }
        [HttpPost("Create")]
        public async Task<ActionResult<APIResult>> Create(string name)
        {
            #region Data verification
            if (string.IsNullOrWhiteSpace(name)) return APIResultHelper.Error($"Parameter {nameof(name)} is null");
            #endregion
            TypeInfo typeInfo = new TypeInfo()
            {
                Name = name
            };
            var data = await _typeInfoService.CreateAsync(typeInfo);
            if (!data)
            {
                return APIResultHelper.Error("Add failed");
            }
            return APIResultHelper.Success(data);
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult<APIResult>> Delete(int Id)
        {
            var data = await _typeInfoService.DeleteAsync(Id);
            if (!data)
            {
                return APIResultHelper.Error("Deleted failed");
            }
            return APIResultHelper.Success(data);
        }
        [HttpPut("Edit")]
        public async Task<ActionResult<APIResult>> Edit(int Id, string name)
        {
            var typeInfo = await _typeInfoService.FindAsync(Id);
            if (typeInfo == null) return APIResultHelper.Error("Not Found this Type of BlogNew");
            typeInfo.Name = name;
            var data = await _typeInfoService.EditAsync(typeInfo);
            if (!data)
            {
                return APIResultHelper.Error("Edit failed");
            }
            return APIResultHelper.Success(data);
        }
    }
}
