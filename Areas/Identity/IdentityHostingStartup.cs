﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVC.Areas.Identity.Data;
using MVC.Data;

[assembly: HostingStartup(typeof(MVC.Areas.Identity.IdentityHostingStartup))]
namespace MVC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MVCAuthDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("MVCAuthDbContextConnection")));

                services.AddDefaultIdentity<User>(options => {
                    options.Password.RequireLowercase = false; //do zmiany
                    options.Password.RequireUppercase = false;
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireNonAlphanumeric = false;
                })

                     .AddEntityFrameworkStores<MVCAuthDbContext>();
            });
        }
    }
}
