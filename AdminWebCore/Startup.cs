using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWebCore.Class;
using AdminWebCore.Configurations.IdentityServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace AdminWebCore
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(cookieOptions => {
                cookieOptions.LoginPath = "/login";
            });

            services.AddMvc().AddRazorPagesOptions(options => {
                options.Conventions.AuthorizePage("/index");
                options.Conventions.AuthorizePage("/MasterAdd");
                options.Conventions.AuthorizePage("/MasterEdit");
                options.Conventions.AuthorizePage("/Masters");
                options.Conventions.AuthorizePage("/Payment");
                options.Conventions.AuthorizePage("/PaymentEdit");
                options.Conventions.AuthorizePage("/StaffAdd");
                options.Conventions.AuthorizePage("/StaffEdit");
                options.Conventions.AuthorizePage("/StaffList");
                options.Conventions.AuthorizePage("/VehicleAdd");
                options.Conventions.AuthorizePage("/VehicleEdit");
                options.Conventions.AuthorizePage("/VehicleList");
            });

           

            services.AddRazorPages();
            services.AddControllers();

            services.AddOpenApiDocument(config =>
            {
                // Document name (default to: v1)
                config.DocumentName = "AdminWebCore";

                // Document / API version (default to: 1.0.0)
                config.Version = "1.0.0";

                // Document title (default to: My Title)
                config.Title = "AdminWebCore";

                // Document description
                config.Description = "AdminWebCore documentation";
                
            });

            services.AddTransient<DatabaseMiddleware>(_ => new DatabaseMiddleware(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            


            //services.AddAuthentication("Bearer")
            //        .AddJwtBearer("Bearer", options => {
            //            options.Authority = "http://localhost/CrmDemo";
            //            options.RequireHttpsMetadata = false;
            //        });

            //services.AddIdentityServer()
            //        .AddDeveloperSigningCredential() //not something we want to use in a production environment
            //        .AddInMemoryIdentityResources(InMemoryConfig.GetIdentityResources())
            //        .AddTestUsers(InMemoryConfig.GetUsers())
            //        .AddInMemoryClients(InMemoryConfig.GetClients());



            //services.AddSwaggerDocument(config => {
            //    config.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT Token",
            //        new OpenApiSecurityScheme
            //        {
            //            Type = OpenApiSecuritySchemeType.ApiKey,
            //            Name = "Authorization",
            //            Description = "Copy 'Bearer ' + valid JWT token into field",
            //            In = OpenApiSecurityApiKeyLocation.Header
            //        }));
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

           

           // app.UseIdentityServer();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

           
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
