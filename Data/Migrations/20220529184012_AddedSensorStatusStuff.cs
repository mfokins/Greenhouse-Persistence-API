using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddedSensorStatusStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MoistureSensorStatusId",
                table: "Pot",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SensorStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsWorking = table.Column<bool>(type: "bit", nullable: false),
                    GreenHouseId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensorStatus_Greenhouses_GreenHouseId",
                        column: x => x.GreenHouseId,
                        principalTable: "Greenhouses",
                        principalColumn: "GreenHouseId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pot_MoistureSensorStatusId",
                table: "Pot",
                column: "MoistureSensorStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SensorStatus_GreenHouseId",
                table: "SensorStatus",
                column: "GreenHouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pot_SensorStatus_MoistureSensorStatusId",
                table: "Pot",
                column: "MoistureSensorStatusId",
                principalTable: "SensorStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pot_SensorStatus_MoistureSensorStatusId",
                table: "Pot");

            migrationBuilder.DropTable(
                name: "SensorStatus");

            migrationBuilder.DropIndex(
                name: "IX_Pot_MoistureSensorStatusId",
                table: "Pot");

            migrationBuilder.DropColumn(
                name: "MoistureSensorStatusId",
                table: "Pot");
        }
    }
}
