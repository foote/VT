using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Hubs;

namespace WebApplication2
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
            services.AddSignalR();

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = "oidc";
            //})
            //.AddCookie(options =>
            //{
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            //    options.Cookie.Name = "mvcimplicit";
            //});

            //services.AddOpenIdConnect("oidc", options =>
            //{
            //    options.ClientId = "MvcClient";
            //    options.Authority = "https://localhost:44356";
            //    options.RequireHttpsMetadata = false;
            //    options.GetClaimsFromUserInfoEndpoint = true;
            //    options.ResponseType = "code token";

            //    options.Scope.Clear();
            //    options.Scope.Add("openid");
            //    options.Scope.Add("sitecore.profile");
            //    options.Scope.Add("offline_access");
            //    options.Scope.Add("sitecore.profile.api");

            //    options.SaveTokens = true;

            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        NameClaimType = JwtClaimTypes.Name,
            //        RoleClaimType = JwtClaimTypes.Role,
            //    };
            //});
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseFileServer();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Color}/{action=Index}/{id?}");
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }
    }
}
