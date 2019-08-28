using Microsoft.EntityFrameworkCore.Migrations;

namespace iBDZ.Web.Migrations
{
    public partial class _2_NoStats_Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitute",
                table: "TrainStations",
                newName: "Longitude");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "TrainStations",
                newName: "Longitute");
        }
    }
}
