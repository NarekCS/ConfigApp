using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfiguringApps.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConfiguringApps
{
    public class Startup
    {
        public Startup(IConfiguration configuartion)
        {
            Configuartion = configuartion;
        }
        public IConfiguration Configuartion { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UptimeService>();
            services.AddMvc().AddMvcOptions(options => { options.RespectBrowserAcceptHeader = true; });
        }
     
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if ((Configuartion.GetSection("ShortCircuitMiddleware")?.GetValue<bool>("EnableBrowserShortCircuit")).Value)
            {
                app.UseMiddleware<BrowserTypeMiddleware>();
                app.UseMiddleware<ShortCircuitMiddleware>();
            }
            if (env.IsDevelopment())
             {
                //app.UseMiddleware<ErrorMiddleware>();
                //app.UseMiddleware<BrowserTypeMiddleware>();
                //app.UseMiddleware<ShortCircuitMiddleware>();
                //app.UseMiddleware<ContentMiddleware>();
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            /*app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });*/
            //app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                    });
        }
    }
}
