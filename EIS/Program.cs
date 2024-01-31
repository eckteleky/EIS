using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using EIS.Data;
using EIS.Models;
using System.Security.Authentication;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace EIS
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await ContextSeed.SeedRolesAsync(userManager, roleManager);
                    await ContextSeed.SeedSuperAdminAsync(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            //webBuilder.ConfigureKestrel(serverOptions =>
            //{
            //});

            //webBuilder.UseKestrel(kestrelOptions =>
            //{
            //    kestrelOptions.ConfigureHttpsDefaults(httpsOptions =>
            //    {
            //        httpsOptions.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13;
            //    });
            //});
            webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
            webBuilder.UseIISIntegration();
            webBuilder.UseStartup<Startup>();
        });
        // .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        //    if (environment == "Development")
        //    {
        //        webBuilder
        //            .UseKestrel()
        //            .ConfigureKestrel(serverOptions =>
        //            {
        //                serverOptions.Limits.MaxRequestBodySize = 10 * 1024;

        //                serverOptions.ConfigureHttpsDefaults(listenOptions =>
        //                {
        //                    listenOptions.SslProtocols = SslProtocols.Tls12;
        //                });
        //            })
        //            .UseStartup<Startup>();
        //    }
        //    else
        //    {
        //        webBuilder
        //            .CaptureStartupErrors(true)
        //            .UseSetting("detailedErrors", "true")
        //            .UseStartup<Startup>();
        //    }
        //});
        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //   WebHost.CreateDefaultBuilder(args)
        //       .ConfigureKestrel(options =>
        //       {
        //           options.ListenLocalhost(5001, listenOptions =>
        //           {
        //               listenOptions.Protocols = HttpProtocols.Http2;
        //               listenOptions.UseHttps();
        //           });
        //       })
        //       .UseStartup<Startup>();
    }
}
