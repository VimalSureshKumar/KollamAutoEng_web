using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KollamAutoEng_web.Migrations
{
    /// <inheritdoc />
    public partial class removedAPIcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fault_Customer_CustomerId",
                table: "Fault");

            migrationBuilder.DropForeignKey(
                name: "FK_Fault_Vehicle_VehicleId",
                table: "Fault");

            migrationBuilder.DropForeignKey(
                name: "FK_FaultPart_Appointment_AppointmentId",
                table: "FaultPart");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Customer_CustomerId",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_VehicleBrand_BrandId",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_VehicleModel_ModelId",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModel_VehicleBrand_BrandId",
                table: "VehicleModel");

            migrationBuilder.DropIndex(
                name: "IX_VehicleModel_BrandId",
                table: "VehicleModel");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_BrandId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_ModelId",
                table: "Vehicle");

            migrationBuilder.AddColumn<int>(
                name: "VehicleBrandBrandId",
                table: "VehicleModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleBrandBrandId",
                table: "Vehicle",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleModelModelId",
                table: "Vehicle",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModel_VehicleBrandBrandId",
                table: "VehicleModel",
                column: "VehicleBrandBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_VehicleBrandBrandId",
                table: "Vehicle",
                column: "VehicleBrandBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_VehicleModelModelId",
                table: "Vehicle",
                column: "VehicleModelModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fault_Customer_CustomerId",
                table: "Fault",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fault_Vehicle_VehicleId",
                table: "Fault",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FaultPart_Appointment_AppointmentId",
                table: "FaultPart",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "AppointmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Customer_CustomerId",
                table: "Vehicle",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleBrand_VehicleBrandBrandId",
                table: "Vehicle",
                column: "VehicleBrandBrandId",
                principalTable: "VehicleBrand",
                principalColumn: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleModel_VehicleModelModelId",
                table: "Vehicle",
                column: "VehicleModelModelId",
                principalTable: "VehicleModel",
                principalColumn: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModel_VehicleBrand_VehicleBrandBrandId",
                table: "VehicleModel",
                column: "VehicleBrandBrandId",
                principalTable: "VehicleBrand",
                principalColumn: "BrandId");
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
                name: "FK_FaultPart_Appointment_AppointmentId",
                table: "FaultPart");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Customer_CustomerId",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_VehicleBrand_VehicleBrandBrandId",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_VehicleModel_VehicleModelModelId",
                table: "Vehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModel_VehicleBrand_VehicleBrandBrandId",
                table: "VehicleModel");

            migrationBuilder.DropIndex(
                name: "IX_VehicleModel_VehicleBrandBrandId",
                table: "VehicleModel");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_VehicleBrandBrandId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_VehicleModelModelId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "VehicleBrandBrandId",
                table: "VehicleModel");

            migrationBuilder.DropColumn(
                name: "VehicleBrandBrandId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "VehicleModelModelId",
                table: "Vehicle");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModel_BrandId",
                table: "VehicleModel",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_BrandId",
                table: "Vehicle",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_ModelId",
                table: "Vehicle",
                column: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fault_Customer_CustomerId",
                table: "Fault",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fault_Vehicle_VehicleId",
                table: "Fault",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_FaultPart_Appointment_AppointmentId",
                table: "FaultPart",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Customer_CustomerId",
                table: "Vehicle",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleBrand_BrandId",
                table: "Vehicle",
                column: "BrandId",
                principalTable: "VehicleBrand",
                principalColumn: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleModel_ModelId",
                table: "Vehicle",
                column: "ModelId",
                principalTable: "VehicleModel",
                principalColumn: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModel_VehicleBrand_BrandId",
                table: "VehicleModel",
                column: "BrandId",
                principalTable: "VehicleBrand",
                principalColumn: "BrandId");
        }
    }
}
