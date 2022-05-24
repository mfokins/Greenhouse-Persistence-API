using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class C02MeasurementRemoval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Co2Measurement",
                table: "DioxideCarbonMeasurement");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Co2Measurement",
                table: "DioxideCarbonMeasurement",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
