using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KollamAutoEng_web.Migrations
{
    /// <inheritdoc />
    public partial class faultparttb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "FaultPart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "FaultPart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FaultPart_CustomerId",
                table: "FaultPart",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_FaultPart_VehicleId",
                table: "FaultPart",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_FaultPart_Customer_CustomerId",
                table: "FaultPart",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FaultPart_Vehicle_VehicleId",
                table: "FaultPart",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaultPart_Customer_CustomerId",
                table: "FaultPart");

            migrationBuilder.DropForeignKey(
                name: "FK_FaultPart_Vehicle_VehicleId",
                table: "FaultPart");

            migrationBuilder.DropIndex(
                name: "IX_FaultPart_CustomerId",
                table: "FaultPart");

            migrationBuilder.DropIndex(
                name: "IX_FaultPart_VehicleId",
                table: "FaultPart");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "FaultPart");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "FaultPart");
        }
    }
}
