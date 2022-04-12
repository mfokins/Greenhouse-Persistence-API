using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class addedGreenhouseStufftry2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TemperatureMesurments",
                table: "TemperatureMesurments");

            migrationBuilder.RenameTable(
                name: "TemperatureMesurments",
                newName: "TemperatureMesurment");

            migrationBuilder.AddColumn<string>(
                name: "GreenHouseId",
                table: "TemperatureMesurment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TemperatureMesurment",
                table: "TemperatureMesurment",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Greenhouses",
                columns: table => new
                {
                    GreenHouseId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Greenhouses", x => x.GreenHouseId);
                });

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
                name: "Pot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GreenHouseId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pot_Greenhouses_GreenHouseId",
                        column: x => x.GreenHouseId,
                        principalTable: "Greenhouses",
                        principalColumn: "GreenHouseId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemperatureMesurment_GreenHouseId",
                table: "TemperatureMesurment",
                column: "GreenHouseId");

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
                name: "IX_Pot_GreenHouseId",
                table: "Pot",
                column: "GreenHouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_TemperatureMesurment_Greenhouses_GreenHouseId",
                table: "TemperatureMesurment",
                column: "GreenHouseId",
                principalTable: "Greenhouses",
                principalColumn: "GreenHouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemperatureMesurment_Greenhouses_GreenHouseId",
                table: "TemperatureMesurment");

            migrationBuilder.DropTable(
                name: "CO2Mesurment");

            migrationBuilder.DropTable(
                name: "HumidityMesurment");

            migrationBuilder.DropTable(
                name: "LuminosityMesurment");

            migrationBuilder.DropTable(
                name: "Pot");

            migrationBuilder.DropTable(
                name: "Greenhouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TemperatureMesurment",
                table: "TemperatureMesurment");

            migrationBuilder.DropIndex(
                name: "IX_TemperatureMesurment_GreenHouseId",
                table: "TemperatureMesurment");

            migrationBuilder.DropColumn(
                name: "GreenHouseId",
                table: "TemperatureMesurment");

            migrationBuilder.RenameTable(
                name: "TemperatureMesurment",
                newName: "TemperatureMesurments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TemperatureMesurments",
                table: "TemperatureMesurments",
                column: "Id");
        }
    }
}
