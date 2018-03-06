using Microsoft.EntityFrameworkCore;
using TimeRegistrationDemo.Data.Entities;

namespace TimeRegistrationDemo.Data
{
    public class TimeRegistrationDbContext : DbContext
    {
        public TimeRegistrationDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<HolidayRequestEntity> HolidayRequests { get; set; }
        public DbSet<HolidayTypeEntity> HolidayTypes { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            /*
                modelBuilder.Entity<Blog>()
                            .Property(b => b.Url)
                            .IsRequired();
                 */
        }
    }
}

