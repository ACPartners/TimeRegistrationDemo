using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TimeRegistrationDemo.Data.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TimeRegistration");

            migrationBuilder.CreateTable(
                name: "HolidayTypes",
                schema: "TimeRegistration",
                columns: table => new
                {
                    HolidayTypeId = table.Column<string>(maxLength: 1, nullable: false),
                    Description = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayType", x => x.HolidayTypeId);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "TimeRegistration",
                columns: table => new
                {
                    UserRoleId = table.Column<string>(maxLength: 1, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.UserRoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "TimeRegistration",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "HolidayRequests",
                schema: "TimeRegistration",
                columns: table => new
                {
                    HolidayRequestId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DisapprovedReason = table.Column<string>(maxLength: 200, nullable: true),
                    From = table.Column<DateTime>(type: "Date", nullable: false),
                    HolidayTypeId = table.Column<string>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 200, nullable: true),
                    To = table.Column<DateTime>(type: "Date", nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayRequest", x => x.HolidayRequestId);
                    table.ForeignKey(
                        name: "FK_HolidayRequests_HolidayTypes_HolidayTypeId",
                        column: x => x.HolidayTypeId,
                        principalSchema: "TimeRegistration",
                        principalTable: "HolidayTypes",
                        principalColumn: "HolidayTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HolidayRequests_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "TimeRegistration",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsersUserRoles",
                schema: "TimeRegistration",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    UserRoleId = table.Column<string>(maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUserRole", x => new { x.UserId, x.UserRoleId });
                    table.ForeignKey(
                        name: "FK_UsersUserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "TimeRegistration",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersUserRoles_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalSchema: "TimeRegistration",
                        principalTable: "UserRoles",
                        principalColumn: "UserRoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HolidayRequests_HolidayTypeId",
                schema: "TimeRegistration",
                table: "HolidayRequests",
                column: "HolidayTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayRequests_UserId",
                schema: "TimeRegistration",
                table: "HolidayRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UK_User",
                schema: "TimeRegistration",
                table: "Users",
                columns: new[] { "FirstName", "LastName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersUserRoles_UserRoleId",
                schema: "TimeRegistration",
                table: "UsersUserRoles",
                column: "UserRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HolidayRequests",
                schema: "TimeRegistration");

            migrationBuilder.DropTable(
                name: "UsersUserRoles",
                schema: "TimeRegistration");

            migrationBuilder.DropTable(
                name: "HolidayTypes",
                schema: "TimeRegistration");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "TimeRegistration");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "TimeRegistration");
        }
    }
}
