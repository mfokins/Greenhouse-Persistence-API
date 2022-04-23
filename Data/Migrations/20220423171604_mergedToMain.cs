using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class mergedToMain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CO2Mesurment");

            migrationBuilder.DropTable(
                name: "HumidityMesurment");

            migrationBuilder.DropTable(
                name: "LuminosityMesurment");

            migrationBuilder.DropTable(
                name: "TemperatureMesurment");

            migrationBuilder.CreateTable(
                name: "DioxideCarbonMeasurement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Co2Measurement = table.Column<double>(type: "float", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GreenHouseId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DioxideCarbonMeasurement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DioxideCarbonMeasurement_Greenhouses_GreenHouseId",
                        column: x => x.GreenHouseId,
                        principalTable: "Greenhouses",
                        principalColumn: "GreenHouseId");
                });

            migrationBuilder.CreateTable(
                name: "HumidityMeasurement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GreenHouseId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumidityMeasurement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HumidityMeasurement_Greenhouses_GreenHouseId",
                        column: x => x.GreenHouseId,
                        principalTable: "Greenhouses",
                        principalColumn: "GreenHouseId");
                });

            migrationBuilder.CreateTable(
                name: "LuminosityMeasurement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lux = table.Column<int>(type: "int", nullable: false),
                    IsLit = table.Column<bool>(type: "bit", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GreenHouseId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "TemperatureMeasurement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Temperature = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GreenHouseId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureMeasurement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemperatureMeasurement_Greenhouses_GreenHouseId",
                        column: x => x.GreenHouseId,
                        principalTable: "Greenhouses",
                        principalColumn: "GreenHouseId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DioxideCarbonMeasurement_GreenHouseId",
                table: "DioxideCarbonMeasurement",
                column: "GreenHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_HumidityMeasurement_GreenHouseId",
                table: "HumidityMeasurement",
                column: "GreenHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_LuminosityMeasurement_GreenHouseId",
                table: "LuminosityMeasurement",
                column: "GreenHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_TemperatureMeasurement_GreenHouseId",
                table: "TemperatureMeasurement",
                column: "GreenHouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DioxideCarbonMeasurement");

            migrationBuilder.DropTable(
                name: "HumidityMeasurement");

            migrationBuilder.DropTable(
                name: "LuminosityMeasurement");

            migrationBuilder.DropTable(
                name: "TemperatureMeasurement");

            migrationBuilder.CreateTable(
                name: "CO2Mesurment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GreenHouseId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CO2Mesurment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CO2Mesurment_Greenhouses_GreenHouseId",
                        column: x => x.GreenHouseId,
                        principalTable: "Greenhouses",
                        principalColumn: "GreenHouseId");
                });

            migrationBuilder.CreateTable(
                name: "HumidityMesurment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GreenHouseId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumidityMesurment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HumidityMesurment_Greenhouses_GreenHouseId",
                        column: x => x.GreenHouseId,
                        principalTable: "Greenhouses",
                        principalColumn: "GreenHouseId");
                });

            migrationBuilder.CreateTable(
                name: "LuminosityMesurment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GreenHouseId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LuminosityMesurment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LuminosityMesurment_Greenhouses_GreenHouseId",
                        column: x => x.GreenHouseId,
                        principalTable: "Greenhouses",
                        principalColumn: "GreenHouseId");
                });

            migrationBuilder.CreateTable(
                name: "TemperatureMesurment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GreenHouseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Temperature = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureMesurment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemperatureMesurment_Greenhouses_GreenHouseId",
                        column: x => x.GreenHouseId,
                        principalTable: "Greenhouses",
                        principalColumn: "GreenHouseId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CO2Mesurment_GreenHouseId",
                table: "CO2Mesurment",
                column: "GreenHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_HumidityMesurment_GreenHouseId",
                table: "HumidityMesurment",
                column: "GreenHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_LuminosityMesurment_GreenHouseId",
                table: "LuminosityMesurment",
                column: "GreenHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_TemperatureMesurment_GreenHouseId",
                table: "TemperatureMesurment",
                column: "GreenHouseId");
        }
    }
}
