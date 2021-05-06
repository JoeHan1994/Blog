using Blog.IService;
using Blog.JWT.Utility._MD5;
using Blog.JWT.Utility.APIResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// this is Authorization
/// </summary>

namespace Blog.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoizeController : ControllerBase
    {
        private readonly IWriterInfoService _WriterInfoService;
        public AuthoizeController(IWriterInfoService writerInfoService)
        {
            _WriterInfoService = writerInfoService;
        }

        [HttpPost("Login")]
        public async Task<ApiResult> Login(string username, string password)
        {
            string pwd = MD5Helper.MD5Encrypt32(password);
            var writerInfo = await _WriterInfoService.FindAsync(c=>c.UserName == username&&c.UserPwd == pwd);
            if (writerInfo!=null)
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name,writerInfo.Name),
                    new Claim("Id",writerInfo.Id.ToString()),
                    new Claim("UserName",writerInfo.UserName),
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("myblog"));
                var token = new JwtSecurityToken(
                    issuer: "http://localhost:5004", //Jwt application url
                    audience: "http://localhost:5001", //Web Api Url
                    claims:claims,
                    notBefore:DateTime.Now,
                    expires:DateTime.Now.AddHours(1),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return APIResultHelper.Success(jwtToken);
            }
            else
            {
                return APIResultHelper.Error("UserName or Password is incorrect");
            }
        }
    }
}
