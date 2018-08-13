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
            services.AddTransient<IBookRepository, EFBookRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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

            //services.Configure<WebEncoderOptions>(options =>
            //{
            //    options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseStatusCodePages();
            //app.UseMvcWithDefaultRoute();
            app.UseSession();
            app.UseMvc(routes => {
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
                    template: "{genre}/Page{page:int}",
                    defaults: new { Controller = "Book", action = "List" });
                routes.MapRoute(
                    name: null,
                    template: "Page{page:int}",
                    defaults: new { Controller = "Book", action = "List", page = 1 });
                routes.MapRoute(
                    name: null,
                    template: "{genre}",
                    defaults: new { Controller = "Book", action = "List", page = 1 });



                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { Controller = "Book", action = "List", page = 1 });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Book}/{action=List}/{id?}");
                }
            );

            //переезжает в Main
            //SeedData.EnsurePopulated(app);

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
