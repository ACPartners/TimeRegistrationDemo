using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TimeRegistrationDemo.Data.Migrations
{
    public partial class MakeIsApprovedNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                schema: "TimeRegistration",
                table: "HolidayRequests",
                nullable: true,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                schema: "TimeRegistration",
                table: "HolidayRequests",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
