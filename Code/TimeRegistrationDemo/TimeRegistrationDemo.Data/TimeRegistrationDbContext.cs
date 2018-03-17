using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeRegistrationDemo.Data.Entities;

namespace TimeRegistrationDemo.Data
{
    public class TimeRegistrationDbContext : DbContext
    {
        public const string DefaultSchema = "TimeRegistration";

        public TimeRegistrationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<HolidayRequestEntity> HolidayRequests { get; set; }
        public DbSet<HolidayTypeEntity> HolidayTypes { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }
        public DbSet<UserUserRoleEntity> UserUserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultSchema);

            CreateModel(modelBuilder.Entity<HolidayTypeEntity>());
            CreateModel(modelBuilder.Entity<HolidayRequestEntity>());
            CreateModel(modelBuilder.Entity<UserEntity>());
            CreateModel(modelBuilder.Entity<UserRoleEntity>());
            CreateModel(modelBuilder.Entity<UserUserRoleEntity>());
        }

        private void CreateModel(EntityTypeBuilder<HolidayTypeEntity> entityTypeBuilder)
        {
            //table name
            entityTypeBuilder.ToTable("HolidayTypes");

            //fields
            entityTypeBuilder.Property(x => x.Id).HasColumnName("HolidayTypeId").HasMaxLength(1);
            entityTypeBuilder.Property(x => x.Description).HasColumnName("Description").HasMaxLength(30);

            //keys, constraints, ...
            entityTypeBuilder.HasKey(x => x.Id).HasName("PK_HolidayType"); //PK

            //relations
        }

        private void CreateModel(EntityTypeBuilder<HolidayRequestEntity> entityTypeBuilder)
        {
            //table name
            entityTypeBuilder.ToTable("HolidayRequests");

            //fields
            entityTypeBuilder.Property(x => x.Id).HasColumnName("HolidayRequestId").UseSqlServerIdentityColumn();
            entityTypeBuilder.Property(x => x.From).HasColumnName("From").HasColumnType("Date").IsRequired();
            entityTypeBuilder.Property(x => x.To).HasColumnName("To").HasColumnType("Date").IsRequired();
            entityTypeBuilder.Property(x => x.Remarks).HasColumnName("Remarks").HasMaxLength(200);
            entityTypeBuilder.Property(x => x.IsApproved).HasColumnName("IsApproved").IsRequired();
            entityTypeBuilder.Property(x => x.DisapprovedReason).HasColumnName("DisapprovedReason").HasMaxLength(200);

            //keys, constraints, ...
            entityTypeBuilder.HasKey(x => x.Id).HasName("PK_HolidayRequest"); //PK

            //relations
            entityTypeBuilder.HasOne(x => x.HolidayType).WithMany(x => x.HolidayRequests).HasForeignKey("HolidayTypeId").OnDelete(DeleteBehavior.Restrict); //FK
            entityTypeBuilder.HasOne(x => x.User).WithMany(x => x.HolidayRequests).HasForeignKey("UserId").OnDelete(DeleteBehavior.Restrict); //FK
        }

        private void CreateModel(EntityTypeBuilder<UserEntity> entityTypeBuilder)
        {
            //table name
            entityTypeBuilder.ToTable("Users");

            //fields
            entityTypeBuilder.Property(x => x.Id).HasColumnName("UserId").UseSqlServerIdentityColumn();
            entityTypeBuilder.Property(x => x.FirstName).HasColumnName("FirstName").HasMaxLength(50).IsRequired();
            entityTypeBuilder.Property(x => x.LastName).HasColumnName("LastName").HasMaxLength(50).IsRequired();

            //keys, constraints, ...
            entityTypeBuilder.HasKey(x => x.Id).HasName("PK_User"); //PK
            entityTypeBuilder.HasIndex(x => new { x.FirstName, x.LastName }).IsUnique().HasName("UK_User"); //UK (firstname + lastname)

            //relations
            entityTypeBuilder.HasMany(x => x.UserRoles).WithOne(x => x.User).HasForeignKey("UserId").OnDelete(DeleteBehavior.Restrict);
        }

        private void CreateModel(EntityTypeBuilder<UserRoleEntity> entityTypeBuilder)
        {
            //table name
            entityTypeBuilder.ToTable("UserRoles");

            //fields
            entityTypeBuilder.Property(x => x.Id).HasColumnName("UserRoleId").HasMaxLength(1);
            entityTypeBuilder.Property(x => x.Description).HasColumnName("Description").HasMaxLength(100);

            //keys, constraints, ...
            entityTypeBuilder.HasKey(x => x.Id).HasName("PK_UserRole"); //PK

            //relations
            entityTypeBuilder.HasMany(x => x.Users).WithOne(x => x.UserRole).HasForeignKey("UserRoleId").OnDelete(DeleteBehavior.Restrict);
        }

        private void CreateModel(EntityTypeBuilder<UserUserRoleEntity> entityTypeBuilder)
        {
            //table name
            entityTypeBuilder.ToTable("UsersUserRoles");

            //fields
            entityTypeBuilder.Property(x => x.UserId).HasColumnName("UserId");
            entityTypeBuilder.Property(x => x.UserRoleId).HasColumnName("UserRoleId").HasMaxLength(1);

            //keys, constraints, ...
            entityTypeBuilder.HasKey(x => new { x.UserId, x.UserRoleId }).HasName("PK_UserUserRole"); //PK
        }
    }
}