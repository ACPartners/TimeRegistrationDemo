using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeRegistrationDemo.Data;

namespace TimeRegistrationDemo.WebApi
{
    public static class DatabaseSeeder
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService <TimeRegistrationDbContext> ();
                context.Database.Migrate();
            }
 
        }
    }
}
