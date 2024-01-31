using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using EIS.Data;
using EIS.Models;
using EIS.EISModels;
// Add namespace for optional routing setup

namespace EIS
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
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=10.20.10.22,1433; Database=WebAccess; User=wadb; Password=data; MultipleActiveResultSets=True;"));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=10.20.10.22,1433;Database=WebAccess;Trust Server Certificate = true;Encrypt=False;Integrated Security=False;User=wadb;Password=data;MultipleActiveResultSets=True;"));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders();
            services.AddControllersWithViews();
            services.AddRazorPages();
            //services.AddHttpContextAccessor();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                //options.User.AllowedUserNameCharacters =
                //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyz0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            // Configure request localization options
            //services.Configure<RequestLocalizationOptions>(ops =>
            //{
            //    // Define supported cultures
            //    var cultures = new CultureInfo[] { new CultureInfo("en"), new CultureInfo("hu") };
            //    ops.SupportedCultures = cultures;
            //    ops.SupportedUICultures = cultures;
            //    ops.DefaultRequestCulture = new RequestCulture("en");

            //    // Optional: add custom provider to support localization based on {culture} route value
            //    ops.RequestCultureProviders.Insert(0, new RouteSegmentRequestCultureProvider(cultures));
            //});

            //services.AddSingleton<IXResourceProvider, XmlResourceProvider>();

            //services.AddHttpClient<ITranslator, MyMemoryTranslateService>();

            //services.AddRazorPages()
            //    .AddRazorPagesOptions(ops => { ops.Conventions.Insert(0, new RouteTemplateModelConventionRazorPages()); })
            //    .AddXLocalizer<LocSource, MyMemoryTranslateService>(ops =>
            //     {
            //         ops.ResourcesPath = "LocalizationResources";
            //         ops.AutoAddKeys = true;
            //         ops.AutoTranslate = true;
            //         ops.TranslateFromCulture = "en";
            //    });

            services.AddDbContext<EISDBContext>(options => options.UseSqlServer("Server=10.20.10.22,1433;Database=TTS;Trust Server Certificate = true;Encrypt=False;Integrated Security=False;User=ttsdb;Password=data;MultipleActiveResultSets=True;"));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(options =>
             {
                 options.Cookie.Name = "RememberMeEIScookie"; // cookie name
                 options.LoginPath = "/Account/LogIn"; // view where the cookie will be issued for the first time
                 options.ExpireTimeSpan = TimeSpan.FromDays(365); // time for the cookei to last in the browser
                 options.SlidingExpiration = true; // the cookie would be re-issued on any request half way through the ExpireTimeSpan
                 //options.EventsType = typeof(CookieAuthEvent);
             });

            //services.ConfigureApplicationCookie(options =>
            //{
            //    // Cookie settings
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(1440);

            //    options.LoginPath = "/Identity/Account/Login";
            //    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            //    options.SlidingExpiration = true;
            //});

           /* services.AddDistributedMemoryCache();*/ //Need for AddSession

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(365);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            #region snippet1    
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { "en-US", "hu-HU" };
                options.SetDefaultCulture(supportedCultures[0])
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures);
            });

            //services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen(options =>
            //{
            //    //options.SwaggerDoc("v1", new OpenApiInfo
            //    //{
            //    //    Version = "v1",
            //    //    Title = "ToDo API",
            //    //    Description = "An ASP.NET Core Web API for managing ToDo items",
            //    //    TermsOfService = new Uri("https://example.com/terms"),
            //    //    Contact = new OpenApiContact
            //    //    {
            //    //        Name = "Example Contact",
            //    //        Url = new Uri("https://example.com/contact")
            //    //    },
            //    //    License = new OpenApiLicense
            //    //    {
            //    //        Name = "Example License",
            //    //        Url = new Uri("https://example.com/license")
            //    //    }
            //    //});
            //    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            //});

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.ApplicationServices.CreateScope().ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
            //app.ApplicationServices.CreateScope().ServiceProvider.GetService<TIPDBContext>().Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //IApplicationBuilder applicationBuilder = app.UseDatabaseErrorPage();
                //app.UseSwagger(options =>
                //{
                //    options.SerializeAsV2 = true;
                //});
                //app.UseSwaggerUI(options =>
                //{
                //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                //    options.RoutePrefix = string.Empty;
                //});
            }
            else
            {
                //app.UseExceptionHandler("/Home/Index");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseStatusCodePagesWithReExecute("/Home/Index/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            var webSocketOptions = new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromMinutes(2)
            };

            app.UseWebSockets(webSocketOptions);

            var supportedCultures = new[] { "en-US", "hu-HU" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseCookiePolicy();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapDefaultControllerRoute();
            //}

            //app.Use(async (context, next) =>
            //{
            //    await next();
            //    if (context.Response.StatusCode == 404)
            //    {
            //        context.Request.Path = "/Home";
            //        await next();
            //    }
            //    if (context.Response.StatusCode == 400)
            //    {
            //        context.Request.Path = "/Home";
            //        await next();
            //    }
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id=1}");
                endpoints.MapRazorPages();
            });
        }
    }
}
