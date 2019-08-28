using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iBDZ.Web.Migrations
{
    public partial class _3_NoStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Delay",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Trains");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeOfDeparture",
                table: "Trains",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "TrainDelays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TrainId = table.Column<int>(nullable: true),
                    Delay = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainDelays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainDelays_Trains_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Trains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainDelays_TrainId",
                table: "TrainDelays",
                column: "TrainId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainDelays");

            migrationBuilder.DropColumn(
                name: "TimeOfDeparture",
                table: "Trains");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Delay",
                table: "Trains",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Trains",
                nullable: false,
                defaultValue: 0);
        }
    }
}
