using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Westwind.AspNetCore.LiveReload;

namespace SecurityHeadersTalk
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddRazorRuntimeCompilation();

            if (_env.IsDevelopment())
            {
                services.AddLiveReload();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseLiveReload();
            }

            // Hand coded but I would use a library.  In .NET - use NWebSec
            app.Use(async (context, next) =>
            {
                //context.Response.Headers.Add("x-frame-options", "DENY");

                context.Response.Headers.Add("x-xss-protection", "0");
                //context.Response.Headers.Add("x-xss-protection", "1;");
                //context.Response.Headers.Add("x-xss-protection", "1; mode=block");

                //context.Response.Headers.Add("content-security-policy", "script-src 'self' 'unsafe-inline'; style-src 'self'; img-src 'self' www.google.com; media-src 'none'");
                //context.Response.Headers.Add("feature-policy", "geolocation 'none'");

                await next();
            });
            
            
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
        }
    }
}