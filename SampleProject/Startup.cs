using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleProject.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LazZiya.Localization.Resources;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using LazZiya.ExpressLocalization;

namespace SampleProject
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
                //Localization for identity error messages
                //.AddErrorDescriber<IdentityErrorDescriberLocalization>() 
                .AddEntityFrameworkStores<ApplicationDbContext>();

            var cultures = new CultureInfo[]
            {
                new CultureInfo("en"),
                new CultureInfo("tr"),
                new CultureInfo("ar")
            };

            services.AddMvc()
                //Configure below localization settings with one step:
                //- Add supported cultures
                //- Add DataAnnotation locaization
                //- Add ModelBinding localization
                //- Define global route template for culture parameter
                //- Configure Request localization
                //- Configure view localization 
                //- Register CultureLocalizer for using shared resources for string loclization in views
                .AddExpressLocalizationOptions(ops =>
                {
                    ops.LocalizationOptions = lo => lo.ResourcesPath = "";

                    ops.LocalizationResourcesTypes = lr =>
                    {
                        lr.DataAnnotations = typeof(DataAnnotationsResource);
                        lr.IdentityDescriber = typeof(IdentityErrorDescriberResource);
                        lr.ModelBinding = typeof(ModelBindingResource);
                        lr.Views = typeof(ViewLocalizationResource);
                    };
                    
                    ops.RequestLocalizationOptions = rlo =>
                    {
                        rlo.SupportedCultures = cultures;
                        rlo.SupportedUICultures = cultures;
                        rlo.DefaultRequestCulture = new RequestCulture("en");
                        rlo.RequestCultureProviders.Insert(0, new RouteValueRequestCultureProvider(cultures, "en"));
                    };

                    ops.MvcOptions = mvcOps => { };
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
