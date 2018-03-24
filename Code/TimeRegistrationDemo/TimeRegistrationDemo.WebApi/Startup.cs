using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeRegistrationDemo.Data;
using TimeRegistrationDemo.Data.Entities;
using TimeRegistrationDemo.Repositories.Implementations;
using TimeRegistrationDemo.Repositories.Interfaces;
using TimeRegistrationDemo.Services.Implementations;
using TimeRegistrationDemo.Services.Interfaces;
using TimeRegistrationDemo.Services.Validation;

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
            services.AddMvc();

            // Register dependencies
            RegisterTimeRegistrationDemoServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        //todo move methods to projects itself
        private void RegisterTimeRegistrationDemoServices(IServiceCollection services)
        {
            //services
            services.AddTransient<IRegisterHolidayRequestService, RegisterHolidayRequestService>();

            //validators
            services.AddTransient<IValidator<HolidayRequestEntity>, HolidayRequestValidator>();

            RegisterTimeRegistrationDemoRepositories(services);
        }

        private void RegisterTimeRegistrationDemoRepositories(IServiceCollection services)
        {
            services.AddTransient<IHolidayRequestRepository, HolidayRequestRepository>();
            services.AddTransient<IHolidayTypeRepository, HolidayTypeRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            RegisterTimeRegistrationDemoDbContext(services);
        }

        private void RegisterTimeRegistrationDemoDbContext(IServiceCollection services)
        {
            services.AddDbContext<TimeRegistrationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TimeRegistrationDatabase")));
        }
    }
}