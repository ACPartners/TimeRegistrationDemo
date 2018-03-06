﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TimeRegistrationDemo.Data;

namespace TimeRegistrationDemo.Data.Migrations
{
    [DbContext(typeof(TimeRegistrationDbContext))]
    partial class TimeRegistrationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TimeRegistrationDemo.Data.Entities.HolidayRequestEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisapprovedReason");

                    b.Property<DateTime>("From");

                    b.Property<string>("HolidayTypeId");

                    b.Property<bool>("IsApproved");

                    b.Property<string>("Remarks");

                    b.Property<DateTime>("To");

                    b.HasKey("Id");

                    b.HasIndex("HolidayTypeId");

                    b.ToTable("HolidayRequests");
                });

            modelBuilder.Entity("TimeRegistrationDemo.Data.Entities.HolidayTypeEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("HolidayTypes");
                });

            modelBuilder.Entity("TimeRegistrationDemo.Data.Entities.UserEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TimeRegistrationDemo.Data.Entities.UserRoleEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<long?>("UserEntityId");

                    b.HasKey("Id");

                    b.HasIndex("UserEntityId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("TimeRegistrationDemo.Data.Entities.HolidayRequestEntity", b =>
                {
                    b.HasOne("TimeRegistrationDemo.Data.Entities.HolidayTypeEntity", "HolidayType")
                        .WithMany()
                        .HasForeignKey("HolidayTypeId");
                });

            modelBuilder.Entity("TimeRegistrationDemo.Data.Entities.UserRoleEntity", b =>
                {
                    b.HasOne("TimeRegistrationDemo.Data.Entities.UserEntity")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserEntityId");
                });
#pragma warning restore 612, 618
        }
    }
}
