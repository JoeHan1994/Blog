using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar.Extensions;
using SqlSugar.IOC;
using Blog.IRepository;
using Blog.Repository;
using Blog.IService;
using Blog.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Blog.WebApi.Utility._AutoMapper;

namespace Blog.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog.WebApi", Version = "v1" });
                #region Swagger Authentication
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { 
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Input Bearer {Token} in the below input",
                    Name = "Authentication",
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
                #endregion

            });

            #region JWT Authentication
            services.AddCustomJWT();
            #endregion

            #region AutpMapper
            services.AddAutoMapper(typeof(CustomAutoMapperProfile));
            #endregion

            #region SqlSugarIOC
            services.AddSqlSugar(new IocConfig()
            {
                ConnectionString = this.Configuration["SqlConn"],
                DbType = IocDbType.SqlServer,
                IsAutoCloseConnection = true
            });
            #endregion

            #region Add Custome IOC
            services.AddCustomIOC();
            #endregion

            services.AddMemoryCache();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog.WebApi v1"));
            }

            app.UseRouting();
            // this is Authentication
            app.UseAuthentication();
            // this is Authorization
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class IOCExtend
    {
        public static IServiceCollection AddCustomIOC(this IServiceCollection services)
        {
            //BlogNews
            services.AddScoped<IBlogNewsRepository, BlogNewsRepository>();
            services.AddScoped<IBlogNewsService, BlogNewsService>();
            //TypeInfo
            services.AddScoped<ITypeInfoRepository, TypeInfoRepository>();
            services.AddScoped<ITypeInfoService, TypeInfoService>();
            //WriterInfo
            services.AddScoped<IWriterInfoRepository, WriterInfoRepository>();
            services.AddScoped<IWriterInfoService, WriterInfoService>();

            return services;
        }
        public static IServiceCollection AddCustomJWT(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                option => {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("myblog")),
                        ValidateIssuer = true,
                        ValidIssuer = "http://localhost:5004", //jwt URL
                        ValidateAudience = true,
                        ValidAudience = "http://localhost:5001", //Web Api url
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(60)
                    };
            });
            return services;
        }
    }
}
