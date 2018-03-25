using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeRegistrationDemo.Data;

namespace TimeRegistrationDemo.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterTimeRegistrationDemoDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TimeRegistrationDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
