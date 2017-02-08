using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Routing;
using OdeToFood.Services;
using OdeToFood.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OdeToFood
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            //Use a configuration builder
            //IHostingEnvironment to tell the configurationbuilder about the base path
            //Once "SetBasePath" is set, pass in the JsonFile (appsettings.json)
            //Then build the confiurationBuilder, and save it to a property called Configuration

            //You now can use the Configuration property to access the appsetting.json file.

            var builder = new ConfigurationBuilder()
                                .SetBasePath(env.ContentRootPath)
                                .AddJsonFile("appsettings.json")
                                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }


        public IConfiguration Configuration{ get; set; }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //IOC container
            services.AddMvc();
            services.AddSingleton<IGreeter, Greeter>();
            services.AddSingleton(Configuration);
            services.AddScoped<IRestaurantData, SqlRestaurantData>(); //Use the same object while it's in the same scope.  As in the same instance within the same http requests
            services.AddDbContext<OdeToFoodDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OdeToFood")));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<OdeToFoodDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IGreeter greeter )
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseExceptionHandler(new ExceptionHandlerOptions
            //{
            //    ExceptionHandler = context => context.Response.WriteAsync("YES ERROR")
            //});
            //app.UseWelcomePage("/welcome");

            //app.UseDefaultFiles();
            //app.UseStaticFiles();
            app.UseFileServer();
            app.UseNodeModules(env.ContentRootPath);

            app.UseIdentity();//cookies into user and process 404 page correctly, thats why its before the UseMvc middleware

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(ConfigureRoutes);

            app.Run(context => context.Response.WriteAsync("Opp, 404 page not found")); //mess errors

            //app.Run(async (context) =>
            //{
            //    var message = greeter.GetGreeting();
            //    await context.Response.WriteAsync(message);
            //});
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // Home/Index
            routeBuilder.MapRoute("Default",
                                   "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
