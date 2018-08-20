using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using testCoreApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Identity;
using testCoreApp.Infrastructure;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.AspNetCore.Routing;

namespace testCoreApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:BooksLibrary:ConnectionString"]));

            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:BooksIdentity:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
                    options.Password.RequireNonAlphanumeric = false)
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IBookRepository, EFBookRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddMvc(
            //options => 
            //idk, but
            //options.MaxModelValidationErrors = 20
            )
            .AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddMemoryCache();
            services.AddSession();

            services.Configure<RouteOptions>(options => 
                options.LowercaseUrls = true);

            //services.Configure<WebEncoderOptions>(options =>
            //{
            //    options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Debug);
            loggerFactory.AddDebug(LogLevel.Debug);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();
            app.UseSession();
            app.UseAuthentication();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "Error",
                    template: "Error",
                    defaults: new { controller = "Error", action = "Error" });

                routes.MapRoute(
                    name: "paginationAuthor",
                    template: "Authors/Page{page}",
                    defaults: new { Controller = "Author", action = "List" });
                routes.MapRoute(
                    name: null,
                    template: "Authors",
                    defaults: new { Controller = "Author", action = "List", page = 1 });

                routes.MapRoute(
                    name: null,
                    template: "{genre}/Page{page}",
                    defaults: new { Controller = "Book", action = "List" },
                    constraints: new { page = new IntRouteConstraint()});
                routes.MapRoute(
                    name: null,
                    template: "Page{page:int}",
                    defaults: new { Controller = "Book", action = "List", page = 1 });
                routes.MapRoute(
                    name: null,
                    template: "{genre}",
                    defaults: new { Controller = "Book", action = "List", page = 1 });


                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}");
                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { Controller = "Book", action = "List", page = 1 });
            });

            //переезжает в Main
            //SeedData.EnsurePopulated(app);

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            //app.Use(async (context, next) =>
            //{
            //    await next.Invoke();
            //    if (context.Response.StatusCode == 403)
            //    {
            //        await context.Response.WriteAsync("it's 403", Encoding.UTF8);
            //    }
            //});

            //app.Use(async (context, next) =>
            //{
            //    context.Items["idk"] = true;
            //    await next.Invoke();

            //});

            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Headers["User-Agent"].Any(h => h.ToLower().Contains("mozilla1")) || context.Items["idk"] as bool? == true)
            //    {
            //        context.Response.StatusCode = 403;
            //    }
            //    else
            //    {
            //        await next.Invoke();
            //    }
            //});

            ////===
            //app.UseMiddleware<ContentMiddleware>();
            ////===

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("111");
            //});

            ////===
            //int x = 5;
            //int y = 8;
            //int z = 0;
            //app.Use(async (context, next) =>
            //{
            //    z = x * y;
            //    await next.Invoke();
            //});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync($"x * y = {z}");
            //});
            ////не вызовется
            //app.UseMiddleware<ContentMiddleware>();
            ////не вызовется
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync($"x * y = {z * 2}");
            //});
            ////===

            ////===
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("<html><body>");
            //    await context.Response.WriteAsync("<div>Inside middleware defined using app.Use</div>");
            //    await next();
            //    await context.Response.WriteAsync("</body></html>");
            //});

            //app.Run(async context => {
            //    await context.Response.WriteAsync("<div>Inside middleware defined using app.Run</div>");
            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("<html><body>");
            //    await context.Response.WriteAsync("<div>Another Middleware defined using app.Use</div>");
            //    await next();
            //    await context.Response.WriteAsync("</body></html>");
            //});
            ////===

        }
    }
}
