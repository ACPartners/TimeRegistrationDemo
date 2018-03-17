using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeRegistrationDemo.Data.Migrations
{
    public partial class SeedUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SqlInsertUserRole(migrationBuilder, "E", "Employee");
            SqlInsertUserRole(migrationBuilder, "M", "Manager");
            SqlInsertUserRole(migrationBuilder, "A", "System administrator");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            SqlDeleteUserRole(migrationBuilder, "E");
            SqlDeleteUserRole(migrationBuilder, "M");
            SqlDeleteUserRole(migrationBuilder, "A");
        }

        private void SqlInsertUserRole(MigrationBuilder migrationBuilder, string userRoleId, string description)
        {
            migrationBuilder.Sql(
                $"INSERT INTO {TimeRegistrationDbContext.DefaultSchema}.[UserRoles] " +
                $"([UserRoleId],[Description]) " +
                $"VALUES ('{userRoleId}','{description}')");
        }

        private void SqlDeleteUserRole(MigrationBuilder migrationBuilder, string userRoleId)
        {
            migrationBuilder.Sql(
                $"DELETE FROM {TimeRegistrationDbContext.DefaultSchema}.[UserRoles] " +
                $"WHERE [UserRoleId] = '{userRoleId}'");
        }
    }
}
