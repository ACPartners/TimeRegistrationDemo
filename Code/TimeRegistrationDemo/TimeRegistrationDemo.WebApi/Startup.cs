using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using TimeRegistrationDemo.Services;

namespace TimeRegistrationDemo.WebApi
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
            var authority = "quickstartidentityserver";
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = $"http://quickstartidentityserver:5000";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "HolidayRequests";
                });
                //Authorization policies to be used in the application
                services.AddAuthorization(options =>
                {
                    options.AddPolicy("RequireEmployeeRole", policy => policy.RequireRole("E"));
                    options.AddPolicy("RequireManagerRole", policy => policy.RequireRole("M"));
                    options.AddPolicy("RequireSystemAdministratorrRole", policy => policy.RequireRole("A"));
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",                    
                    AuthorizationUrl = $"http://localhost:5000/connect/authorize",
                    TokenUrl = $"http://localhost:5000/connect/token",
                    Scopes = new Dictionary<string, string>() { { "HolidayRequests", "Allow holiday requests" } }
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
            });

            services.AddMvc();



            // Register dependencies
            services.RegisterTimeRegistrationDemoServices(Configuration.GetConnectionString("TimeRegistrationDatabase"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.Use((context, next) => {
            //    //Before everything else, make sure the hostname is always the same:
            //    var request = context.Request;
            //    request.Host = new Microsoft.AspNetCore.Http.HostString("http://quickstartidentityserver");

            //    return next();
            //});
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.OAuthClientId("mvc");
                c.OAuthAppName("TimeRegistration Demo");
                c.OAuthAdditionalQueryStringParams(new { scope = "HolidayRequests" });

            });
            app.UseMvc();
            app.SeedData();
        }
    }
}