using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FileManager.DBAccess.Context;
using FileManager.Core;
using FileManager.Core.Interfaces;
using FileManager.Core.Services;
using GleamTech.AspNet.Core;
using GleamTech.FileUltimate;

namespace FileManager
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
            services.AddControllersWithViews();

            //Add GleamTech to the ASP.NET Core services container.
            //----------------------
            services.AddGleamTech();
            //----------------------

            services.AddDbContext<FileManagerContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("FileManagerContext")));

            services.AddAuthentication("UserCookie").AddCookie("UserCookie", options =>
            {
                options.Cookie.Name = "userscookie";
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
            });

            services.AddAuthorization();
            services.AddDbContext<FileManagerContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("FileManagerContext")));

            

            services.AddTransient<IUser, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Register GleamTech to the ASP.NET Core HTTP request pipeline.
            //----------------------
            app.UseGleamTech();
            //----------------------
            //Set this property only if you have a valid license key, otherwise do not 
            //set it so FileUltimate runs in trial mode.  
            //FileUltimateConfiguration.Current.LicenseKey = "QQJDJLJP34...";

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
