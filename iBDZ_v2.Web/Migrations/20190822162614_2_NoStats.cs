using Microsoft.EntityFrameworkCore.Migrations;

namespace iBDZ.Web.Migrations
{
    public partial class _2_NoStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocationY",
                table: "TrainStations",
                newName: "Longitute");

            migrationBuilder.RenameColumn(
                name: "LocationX",
                table: "TrainStations",
                newName: "Latitude");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitute",
                table: "TrainStations",
                newName: "LocationY");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "TrainStations",
                newName: "LocationX");
        }
    }
}
