using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class removedPotIdFromMesurment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoistureMeasurement_Pot_PotId",
                table: "MoistureMeasurement");

            migrationBuilder.AlterColumn<int>(
                name: "PotId",
                table: "MoistureMeasurement",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MoistureMeasurement_Pot_PotId",
                table: "MoistureMeasurement",
                column: "PotId",
                principalTable: "Pot",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoistureMeasurement_Pot_PotId",
                table: "MoistureMeasurement");

            migrationBuilder.AlterColumn<int>(
                name: "PotId",
                table: "MoistureMeasurement",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MoistureMeasurement_Pot_PotId",
                table: "MoistureMeasurement",
                column: "PotId",
                principalTable: "Pot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
