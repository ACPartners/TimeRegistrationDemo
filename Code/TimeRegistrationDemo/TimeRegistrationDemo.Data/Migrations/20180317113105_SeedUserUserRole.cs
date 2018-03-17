using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeRegistrationDemo.Data.Migrations
{
    public partial class SeedUserUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SqlInsertUserUserRole(migrationBuilder, "John", "Doe", "E");
            SqlInsertUserUserRole(migrationBuilder, "Adam", "Smith", "M");
            SqlInsertUserUserRole(migrationBuilder, "Sarah", "Brown", "A");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //simply delete all records
            migrationBuilder.Sql($"DELETE FROM {TimeRegistrationDbContext.DefaultSchema}.[UsersUserRoles]");
        }

        private void SqlInsertUserUserRole(MigrationBuilder migrationBuilder, string firstName, string lastName, string userRoleId)
        {
            var sqlSelectUser = $"SELECT [UserId] FROM {TimeRegistrationDbContext.DefaultSchema}.[Users] " +
                                $"WHERE [FirstName] = '{firstName}' AND [LastName] = '{lastName}'";

            migrationBuilder.Sql(
                $"INSERT INTO {TimeRegistrationDbContext.DefaultSchema}.[UsersUserRoles] " +
                $"([UserId],[UserRoleId]) " +
                $"VALUES ( ({sqlSelectUser}), '{userRoleId}')");
        }
    }
}
