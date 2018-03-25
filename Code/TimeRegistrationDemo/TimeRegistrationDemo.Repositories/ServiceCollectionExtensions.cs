using Microsoft.Extensions.DependencyInjection;
using TimeRegistrationDemo.Repositories.Implementations;
using TimeRegistrationDemo.Repositories.Interfaces;

namespace TimeRegistrationDemo.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterTimeRegistrationDemoRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IHolidayRequestRepository, HolidayRequestRepository>();
            services.AddTransient<IHolidayTypeRepository, HolidayTypeRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            // Register data layer
            services.RegisterTimeRegistrationDemoDbContext(connectionString);
        }
    }
}
