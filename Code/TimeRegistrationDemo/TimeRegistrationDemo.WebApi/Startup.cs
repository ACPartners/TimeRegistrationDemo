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
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
    .AddIdentityServerAuthentication(options =>
    {
        options.Authority = "http://localhost:5000";
        options.RequireHttpsMetadata = false;
        options.ApiName = "HolidayRequests";
    });
            //Authorization policies to be used in the application
            //services.AddAuthorization(options =>
            //{
            //    //Require a role 
            //    options.AddPolicy("RequireAdministratorsRole", policy => policy.RequireRole("Administrators"));
            //    //Require a value for a certain claim
            //    options.AddPolicy("RequireAdministratorsAsClaim", policy => policy.RequireClaim("role", "Administrators"));
            //    // Custom Policy can be constructed with IAuthorizationRequirements
            //    //options.AddPolicy("AtLeast21", policy => policy.Requirements.Add(new MinimumAgeRequirement(21)));
            //});

            //services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
            //public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
            //{
            //protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            //                                               MinimumAgeRequirement requirement)



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = "http://localhost:5000/connect/authorize",
                    TokenUrl = "http://localhost:5000/connect/token",
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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.OAuthClientId("mvc");
                //c.OAuth2RedirectUrl("http://localhost:55229/swagger");
                c.OAuthAppName("TimeRegistration Demo");
                c.OAuthAdditionalQueryStringParams(new { scope = "HolidayRequests" });
                
            });
            app.UseMvc();
        }
    }
}