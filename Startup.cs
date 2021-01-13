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
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;
using MVC.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

namespace MVC
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
            services.AddLocalization(OptionsServiceCollectionExtensions =>
            {
                OptionsServiceCollectionExtensions.ResourcesPath = "Resources";
            });

            services.AddMvc()
                .AddViewLocalization(
                OptionsServiceCollectionExtensions =>
                { OptionsServiceCollectionExtensions.ResourcesPath = "Resources"; })
                .AddDataAnnotationsLocalization()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            services.Configure<RequestLocalizationOptions>(OptionsServiceCollectionExtensions =>
            {
                var supportedCultures = new List<CultureInfo> {
                    new CultureInfo("en"),
                    new CultureInfo("pl")};
                OptionsServiceCollectionExtensions.DefaultRequestCulture = new RequestCulture("en");
                OptionsServiceCollectionExtensions.SupportedCultures = supportedCultures;
                OptionsServiceCollectionExtensions.SupportedUICultures = supportedCultures;
            });

            services.AddHttpClient();


            services.AddControllersWithViews();
            services.AddDefaultIdentity<User>(options => {
                options.Password.RequireLowercase = false; //do zmiany
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<MVCAuthDbContext>();

            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddHttpClient();

            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ApplicationDbContext")));

            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromSeconds(240);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddServerSideBlazor();
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



            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseStatusCodePages(async context =>
            {
                context.HttpContext.Response.ContentType = "text/plain";
                await context.HttpContext.Response.WriteAsync(
                    "Error, status code: " +
                    context.HttpContext.Response.StatusCode);
            });
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();

            app.UseRequestLocalization(options.Value);

            app.UseSession();
        }
    }
}
