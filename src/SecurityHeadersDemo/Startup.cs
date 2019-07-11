using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityHeadersDemo.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SecurityHeadersDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("InMemory"));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddMvc(options => options.CacheProfiles.Remove("cache-control")).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
