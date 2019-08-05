using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;
using CRM.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM
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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new PathString("/Home/Home");
                    options.AccessDeniedPath = new PathString("/Home/Home");
                });
            

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // TODO: [COMPLETE] use static ctor instead of this call
            //DropDownFields.InitiateFields(); 

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            // TODO: [COMEPLETE] move to jobs !!!ASAP!!!
            /*
            DailyTriggerService.StartDeilyTrigger(); 

            DailyTriggerService.OnTimeTriggered += () =>
            {
                LoadDataService loadData = new LoadDataService();
            };*/
        }
    }
}
