using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace OpenIdConnectClientExample
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
            services.AddMvc(opt => opt.RequireHttpsPermanent = false);
            
           
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            services.AddControllersWithViews();
            
            services.AddAuthentication(_ =>
        {
            _.DefaultScheme = "numan";
            _.DefaultChallengeScheme = "oidc";
        })
        .AddCookie("numan")
        .AddOpenIdConnect("oidc", _ =>
        {
            _.SignInScheme = "numan";
            _.Authority = "https://localhost:44394";
            _.ClientId = "default-client";
            _.ClientSecret = "379ea949-a2c9-4508-a51f-add1173af8c0";
            _.ResponseType = "code";
            _.ResponseMode = "query";
            _.SaveTokens = true;
        });
           
         
        }
          
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            IdentityModelEventSource.ShowPII = true;
            

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
