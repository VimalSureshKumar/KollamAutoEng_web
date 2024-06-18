using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KollamAutoEng_web.Migrations
{
    /// <inheritdoc />
    public partial class finalmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "Fault_Description",
                table: "Fault");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Vehicle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "Vehicle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleBrandBrandId",
                table: "Vehicle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleModelModelId",
                table: "Vehicle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Fault",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Fault",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VehicleBrand",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleBrand", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "VehicleModel",
                columns: table => new
                {
                    ModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    VehicleBrandBrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModel", x => x.ModelId);
                    table.ForeignKey(
                        name: "FK_VehicleModel_VehicleBrand_VehicleBrandBrandId",
                        column: x => x.VehicleBrandBrandId,
                        principalTable: "VehicleBrand",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_VehicleBrandBrandId",
                table: "Vehicle",
                column: "VehicleBrandBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_VehicleModelModelId",
                table: "Vehicle",
                column: "VehicleModelModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Fault_CustomerId",
                table: "Fault",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Fault_VehicleId",
                table: "Fault",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModel_VehicleBrandBrandId",
                table: "VehicleModel",
                column: "VehicleBrandBrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fault_Customer_CustomerId",
                table: "Fault",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Fault_Vehicle_VehicleId",
                table: "Fault",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleBrand_VehicleBrandBrandId",
                table: "Vehicle",
                column: "VehicleBrandBrandId",
                principalTable: "VehicleBrand",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleModel_VehicleModelModelId",
                table: "Vehicle",
                column: "VehicleModelModelId",
                principalTable: "VehicleModel",
                principalColumn: "ModelId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fault_Customer_CustomerId",
                table: "Fault");

            migrationBuilder.DropForeignKey(
                name: "FK_Fault_Vehicle_VehicleId",
                table: "Fault");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_VehicleBrand_VehicleBrandBrandId",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_VehicleModel_VehicleModelModelId",
                table: "Vehicle");

            migrationBuilder.DropTable(
                name: "VehicleModel");

            migrationBuilder.DropTable(
                name: "VehicleBrand");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_VehicleBrandBrandId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_VehicleModelModelId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Fault_CustomerId",
                table: "Fault");

            migrationBuilder.DropIndex(
                name: "IX_Fault_VehicleId",
                table: "Fault");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "VehicleBrandBrandId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "VehicleModelModelId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Fault");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Fault");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Vehicle",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Vehicle",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Fault_Description",
                table: "Fault",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
