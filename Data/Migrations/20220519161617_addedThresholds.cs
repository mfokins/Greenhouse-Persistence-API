using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class addedThresholds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "moistureThreshold",
                table: "Pot");

            migrationBuilder.AddColumn<int>(
                name: "MoistureThresholdId",
                table: "Pot",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Threshold",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    LowerThreshold = table.Column<double>(type: "float", nullable: false),
                    HigherThreshold = table.Column<double>(type: "float", nullable: true),
                    GreenHouseId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threshold", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Threshold_Greenhouses_GreenHouseId",
                        column: x => x.GreenHouseId,
                        principalTable: "Greenhouses",
                        principalColumn: "GreenHouseId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pot_MoistureThresholdId",
                table: "Pot",
                column: "MoistureThresholdId");

            migrationBuilder.CreateIndex(
                name: "IX_Threshold_GreenHouseId",
                table: "Threshold",
                column: "GreenHouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pot_Threshold_MoistureThresholdId",
                table: "Pot",
                column: "MoistureThresholdId",
                principalTable: "Threshold",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pot_Threshold_MoistureThresholdId",
                table: "Pot");

            migrationBuilder.DropTable(
                name: "Threshold");

            migrationBuilder.DropIndex(
                name: "IX_Pot_MoistureThresholdId",
                table: "Pot");

            migrationBuilder.DropColumn(
                name: "MoistureThresholdId",
                table: "Pot");

            migrationBuilder.AddColumn<double>(
                name: "moistureThreshold",
                table: "Pot",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
