using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class addedPots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LuminosityMeasurement");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Pot",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "moistureThreshold",
                table: "Pot",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "MoistureMeasurement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Moisture = table.Column<double>(type: "float", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PotId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoistureMeasurement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoistureMeasurement_Pot_PotId",
                        column: x => x.PotId,
                        principalTable: "Pot",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoistureMeasurement_PotId",
                table: "MoistureMeasurement",
                column: "PotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoistureMeasurement");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Pot");

            migrationBuilder.DropColumn(
                name: "moistureThreshold",
                table: "Pot");

            migrationBuilder.CreateTable(
                name: "LuminosityMeasurement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GreenHouseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsLit = table.Column<bool>(type: "bit", nullable: false),
                    Lux = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LuminosityMeasurement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LuminosityMeasurement_Greenhouses_GreenHouseId",
                        column: x => x.GreenHouseId,
                        principalTable: "Greenhouses",
                        principalColumn: "GreenHouseId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LuminosityMeasurement_GreenHouseId",
                table: "LuminosityMeasurement",
                column: "GreenHouseId");
        }
    }
}
