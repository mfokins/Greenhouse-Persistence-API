using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class addedGreenhouseStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GreenHouseId",
                table: "TemperatureMesurments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GreenHouseId",
                table: "TemperatureMesurments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
