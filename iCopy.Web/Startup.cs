using iCopy.Database;
using iCopy.Database.Context;
using iCopy.Model.Options;
using iCopy.SERVICES.Registers;
using iCopy.Web.Helper;
using iCopy.Web.Resources;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using iCopy.ExternalServices;
using iCopy.Web.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using DBContext = iCopy.Database.Context.DBContext;
using iCopy.Web.Hubs;

namespace iCopy.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services
                .AddMvc(options =>
                {
                    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
                })
                .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(ValidationErrors));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddLocalization(o => o.ResourcesPath = "Resources");
            services.AddRouting(x => x.LowercaseUrls = true);
            services.ConfigureLocalization();
            services.AddDbContext<DBContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DBContext")));
            services.AddDbContext<AuthContext>(x => x.UseSqlServer(Configuration.GetConnectionString("AuthContext")));
            services.AddSession();
            services.AddiCopyServices();
            services.AddExternalServices();
            services.AddScoped<SharedResource>();
            services.AddScoped<ValidationErrors>();
            services.AddScoped<Constants>();
            services.AddScoped<ISelectList, SelectList>();
            services.AddSingleton<ProfilePhotoOptions>(Configuration.GetSection("Files:ProfilePhoto").Get<ProfilePhotoOptions>());
            services.AddSingleton<PrintRequestFileOptions>(Configuration.GetSection("Files:PrintRequestFile").Get<PrintRequestFileOptions>());
            services.AddSingleton<EmailServerNoReplyOptions>(Configuration.GetSection("EmailServers:no-reply").Get<EmailServerNoReplyOptions>());
            services.AddSignalR();
            services.AddAuthentication().AddCookie();
            services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<AuthContext>().AddDefaultTokenProviders();
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });
            services.Configure<AuthenticationOptions>(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });
            services.Configure<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;

                options.AccessDeniedPath = new PathString("/auth/errors/error401");
                options.LoginPath = new PathString("/auth/login/index");
                options.LogoutPath = new PathString("/auth/login/logout");
            });
            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(2);
                options.Lockout.MaxFailedAccessAttempts = 5;

                options.User.RequireUniqueEmail = true;

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;    

            });
            services.Configure<SessionOptions>(options =>
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Cookie.HttpOnly = true;
            });
            services.Configure<PasswordHasherOptions>(options =>
            {
                options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3;
                options.IterationCount = 100000;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseCookiePolicy();
            app.UseRequestLocalization();
            app.UseSession();
            app.UseAuthentication();
            app.UseSignalR(router => router.MapHub<NotificationsHub>("/notificationshub"));
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Login}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "withourareas",
                    template: "{controller=Login}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{area=auth}/{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
