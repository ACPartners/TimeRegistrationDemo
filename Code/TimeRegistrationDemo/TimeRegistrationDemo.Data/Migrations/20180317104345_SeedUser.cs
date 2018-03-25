using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeRegistrationDemo.Data.Migrations
{
    public partial class SeedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SqlInsertUser(migrationBuilder, "John", "Doe");
            SqlInsertUser(migrationBuilder, "Adam", "Smith");
            SqlInsertUser(migrationBuilder, "Sarah", "Brown");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            SqlDeleteUser(migrationBuilder, "John", "Doe");
            SqlDeleteUser(migrationBuilder, "Adam", "Smith");
            SqlDeleteUser(migrationBuilder, "Sarah", "Brown");
        }

        private void SqlInsertUser(MigrationBuilder migrationBuilder, string firstName, string lastName)
        {
            migrationBuilder.Sql(
                $"INSERT INTO {TimeRegistrationDbContext.DefaultSchema}.[Users] " +
                $"([FirstName],[LastName]) " +
                $"VALUES ('{firstName}','{lastName}')");
        }

        private void SqlDeleteUser(MigrationBuilder migrationBuilder, string firstName, string lastName)
        {
            migrationBuilder.Sql(
                $"DELETE FROM {TimeRegistrationDbContext.DefaultSchema}.[Users] " +
                $"WHERE [FirstName] = '{firstName}' AND [LastName] = '{lastName}'");
        }
    }
}
