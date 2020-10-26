using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoboAspNET1.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PraksaWebAPI.BLL;
using PraksaWebAPI.BLL.Interfaces;
using PraksaWebAPI.DAL;
using PraksaWebAPI.DAL.Interfaces;
using PraksaWebAPI.Helpers;

namespace PraksaWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IEmployeeBLL, EmployeeBLL>();
            services.AddSingleton<IEmployeeDAL, EmployeeDAL>();
            //services.AddSingleton<IEmployeeDAL, SecoundEmployeeDAL>();

            services.AddSingleton<IRoleBLL, RoleBLL>();
            services.AddSingleton<IRoleDAL, RoleDAL>();
            //services.AddSingleton<IRoleDAL, SecoundRoleDAL>();

            services.AddSingleton<IDepartmentBLL, DepartmentBLL>();
            services.AddSingleton<IDepartmentDAL, DepartmentDAL>();
            //services.AddSingleton<IDepartmentDAL, SecoundDepartmentDAL>();


            services.AddSingleton<ITaskBLL, TaskBLL>();
            services.AddSingleton<ITaskDAL, TaskDAL>();
            //services.AddSingleton<ITaskDAL, SecoundTaskDAL>();

            services.AddSingleton<IDocumentBLL, DocumentBLL>();
            services.AddSingleton<IDocumentDAL, DocumentDAL>();

            services.AddSingleton<IShareBLL, ShareBLL>();
            services.AddSingleton<IShareDAL, ShareDAL>();

            services.AddSingleton<ITaskCommentBLL, TaskCommentBLL>();
            services.AddSingleton<ITaskCommentDAL, TaskCommentDAL>();


            services.AddSingleton<IGoogleDriveApiHelper, GoogleDriveApiHelper>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = Configuration["Jwt:Issuer"],
                      ValidAudience = Configuration["Jwt:Issuer"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                  };
              });
            services.AddDataProtection();

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin();
                                      builder.AllowAnyHeader();
                                      builder.AllowAnyMethod();
                                  });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDataProtectionProvider provider)
        {
            Init(provider);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

           
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseGzipMiddleware();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void Init(IDataProtectionProvider provider)
        {
            ProtectionHelper.Singletion.SetConfig(Configuration);
            ProtectionHelper.Singletion.SetProvider(provider, Configuration["DataProtection:Key:Value"]);
            ProtectionHelper.Singletion.GetSectionValue("DataProtection:Key");
        }
    }
}
