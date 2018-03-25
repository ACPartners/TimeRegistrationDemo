using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeRegistrationDemo.Data.Migrations
{
    public partial class SeedHolidayType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SqlInsertHolidayType(migrationBuilder, "P", "Paid holiday");
            SqlInsertHolidayType(migrationBuilder, "N", "Normal holiday");
            SqlInsertHolidayType(migrationBuilder, "S", "Sick-leave");
            SqlInsertHolidayType(migrationBuilder, "M", "Maternity leave");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            SqlDeleteHolidayType(migrationBuilder, "P");
            SqlDeleteHolidayType(migrationBuilder, "N");
            SqlDeleteHolidayType(migrationBuilder, "S");
            SqlDeleteHolidayType(migrationBuilder, "M");
        }

        private void SqlInsertHolidayType(MigrationBuilder migrationBuilder, string holidayTypeId, string description)
        {
            migrationBuilder.Sql(
                $"INSERT INTO {TimeRegistrationDbContext.DefaultSchema}.[HolidayTypes] " +
                $"([HolidayTypeId],[Description]) " +
                $"VALUES ('{holidayTypeId}','{description}')");
        }

        private void SqlDeleteHolidayType(MigrationBuilder migrationBuilder, string holidayTypeId)
        {
            migrationBuilder.Sql(
                $"DELETE FROM {TimeRegistrationDbContext.DefaultSchema}.[HolidayTypes] " +
                $"WHERE [HolidayTypeId] = '{holidayTypeId}'");
        }
    }
}


