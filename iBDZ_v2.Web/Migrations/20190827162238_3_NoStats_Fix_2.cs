using Microsoft.EntityFrameworkCore.Migrations;

namespace iBDZ.Web.Migrations
{
    public partial class _3_NoStats_Fix_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainStationId",
                table: "TrainDelays",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainDelays_TrainStationId",
                table: "TrainDelays",
                column: "TrainStationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainDelays_TrainStations_TrainStationId",
                table: "TrainDelays",
                column: "TrainStationId",
                principalTable: "TrainStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainDelays_TrainStations_TrainStationId",
                table: "TrainDelays");

            migrationBuilder.DropIndex(
                name: "IX_TrainDelays_TrainStationId",
                table: "TrainDelays");

            migrationBuilder.DropColumn(
                name: "TrainStationId",
                table: "TrainDelays");
        }
    }
}
