using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KollamAutoEng_web.Migrations
{
    /// <inheritdoc />
    public partial class cascading : Migration
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
                name: "FK_FaultPart_Appointment_AppointmentId",
                table: "FaultPart",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "AppointmentId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Customer_CustomerId",
                table: "Vehicle",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleBrand_BrandId",
                table: "Vehicle",
                column: "BrandId",
                principalTable: "VehicleBrand",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleModel_ModelId",
                table: "Vehicle",
                column: "ModelId",
                principalTable: "VehicleModel",
                principalColumn: "ModelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModel_VehicleBrand_BrandId",
                table: "VehicleModel",
                column: "BrandId",
                principalTable: "VehicleBrand",
                principalColumn: "BrandId",
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
