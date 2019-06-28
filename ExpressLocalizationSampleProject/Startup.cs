using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpressLocalizationSampleProject.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using LazZiya.ExpressLocalization;
using ExpressLocalizationSampleProject.LocalizationResources;

namespace ExpressLocalizationSampleProject
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
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAntiforgery();

            var cultures = new CultureInfo[]
            {
                new CultureInfo("en"),
                new CultureInfo("ar"),
                new CultureInfo("cs"),
                new CultureInfo("de"),
                new CultureInfo("es"),
                new CultureInfo("fa"),
                new CultureInfo("fr"),
                new CultureInfo("hi"),
                new CultureInfo("hu"),
                new CultureInfo("it"),
                new CultureInfo("ja"),
                new CultureInfo("ko"),
                new CultureInfo("nl"),
                new CultureInfo("pl"),
                new CultureInfo("pt"),
                new CultureInfo("pt-br"),
                new CultureInfo("ru"),
                new CultureInfo("sv"),
                new CultureInfo("tr"),
                new CultureInfo("vi"),
                new CultureInfo("zh"),

            };
            services.AddSession();

            services.AddMvc()
                .AddExpressLocalization<ExpressLocalizationResource, ViewLocalizationResource>(ops =>
                {
                    ops.RequestLocalizationOptions = o =>
                    {
                        o.SupportedCultures = cultures;
                        o.SupportedUICultures = cultures;
                        o.DefaultRequestCulture = new RequestCulture("en");
                    };
                    ops.ResourcesPath = "LocalizationResources";
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            //add localization middleware to the app
            app.UseRequestLocalization();

            app.UseMvc();
        }
    }
}
