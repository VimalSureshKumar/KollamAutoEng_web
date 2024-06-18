using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KollamAutoEng_web.Migrations
{
    /// <inheritdoc />
    public partial class foreignkeyFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Customer_CustomerId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Employee_EmployeeId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Vehicle_VehicleId",
                table: "Appointment");

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
                name: "FK_FaultPart_Fault_FaultId",
                table: "FaultPart");

            migrationBuilder.DropForeignKey(
                name: "FK_FaultPart_Part_PartId",
                table: "FaultPart");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Appointment_AppointmentId",
                table: "Payment");

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

            migrationBuilder.DropColumn(
                name: "Payment_Date",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "Customer_DateOfBirth",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Appointment_Date",
                table: "Appointment");

            migrationBuilder.RenameColumn(
                name: "Payment_Amount",
                table: "Payment",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Part_Reference",
                table: "Part",
                newName: "Reference");

            migrationBuilder.RenameColumn(
                name: "Part_Name",
                table: "Part",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Part_Cost",
                table: "Part",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "Employee_Status",
                table: "Employee",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Employee_PhoneNumber",
                table: "Employee",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Employee_Pay",
                table: "Employee",
                newName: "Pay");

            migrationBuilder.RenameColumn(
                name: "Employee_LastName",
                table: "Employee",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Employee_Hours",
                table: "Employee",
                newName: "Hours");

            migrationBuilder.RenameColumn(
                name: "Employee_FirstName",
                table: "Employee",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Customer_Reference",
                table: "Customer",
                newName: "Reference");

            migrationBuilder.RenameColumn(
                name: "Customer_PhoneNumber",
                table: "Customer",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Customer_LastName",
                table: "Customer",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Customer_Gender",
                table: "Customer",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "Customer_FirstName",
                table: "Customer",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Customer_Email",
                table: "Customer",
                newName: "Email");

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Customer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "Appointment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
                name: "FK_Appointment_Customer_CustomerId",
                table: "Appointment",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Employee_EmployeeId",
                table: "Appointment",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Vehicle_VehicleId",
                table: "Appointment",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_FaultPart_Fault_FaultId",
                table: "FaultPart",
                column: "FaultId",
                principalTable: "Fault",
                principalColumn: "FaultId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FaultPart_Part_PartId",
                table: "FaultPart",
                column: "PartId",
                principalTable: "Part",
                principalColumn: "PartId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Appointment_AppointmentId",
                table: "Payment",
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
                name: "FK_Vehicle_VehicleBrand_BrandId",
                table: "Vehicle",
                column: "BrandId",
                principalTable: "VehicleBrand",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleModel_ModelId",
                table: "Vehicle",
                column: "ModelId",
                principalTable: "VehicleModel",
                principalColumn: "ModelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModel_VehicleBrand_BrandId",
                table: "VehicleModel",
                column: "BrandId",
                principalTable: "VehicleBrand",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Customer_CustomerId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Employee_EmployeeId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Vehicle_VehicleId",
                table: "Appointment");

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
                name: "FK_FaultPart_Fault_FaultId",
                table: "FaultPart");

            migrationBuilder.DropForeignKey(
                name: "FK_FaultPart_Part_PartId",
                table: "FaultPart");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Appointment_AppointmentId",
                table: "Payment");

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

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "Appointment");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Payment",
                newName: "Payment_Amount");

            migrationBuilder.RenameColumn(
                name: "Reference",
                table: "Part",
                newName: "Part_Reference");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Part",
                newName: "Part_Name");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Part",
                newName: "Part_Cost");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Employee",
                newName: "Employee_Status");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Employee",
                newName: "Employee_PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Pay",
                table: "Employee",
                newName: "Employee_Pay");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Employee",
                newName: "Employee_LastName");

            migrationBuilder.RenameColumn(
                name: "Hours",
                table: "Employee",
                newName: "Employee_Hours");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Employee",
                newName: "Employee_FirstName");

            migrationBuilder.RenameColumn(
                name: "Reference",
                table: "Customer",
                newName: "Customer_Reference");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Customer",
                newName: "Customer_PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Customer",
                newName: "Customer_LastName");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Customer",
                newName: "Customer_Gender");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Customer",
                newName: "Customer_FirstName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Customer",
                newName: "Customer_Email");

            migrationBuilder.AddColumn<int>(
                name: "VehicleBrandBrandId",
                table: "VehicleModel",
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

            migrationBuilder.AddColumn<string>(
                name: "Payment_Date",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Customer_DateOfBirth",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Appointment_Date",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                name: "FK_Appointment_Customer_CustomerId",
                table: "Appointment",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Employee_EmployeeId",
                table: "Appointment",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Vehicle_VehicleId",
                table: "Appointment",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fault_Customer_CustomerId",
                table: "Fault",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fault_Vehicle_VehicleId",
                table: "Fault",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FaultPart_Appointment_AppointmentId",
                table: "FaultPart",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "AppointmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FaultPart_Fault_FaultId",
                table: "FaultPart",
                column: "FaultId",
                principalTable: "Fault",
                principalColumn: "FaultId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FaultPart_Part_PartId",
                table: "FaultPart",
                column: "PartId",
                principalTable: "Part",
                principalColumn: "PartId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Appointment_AppointmentId",
                table: "Payment",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "AppointmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Customer_CustomerId",
                table: "Vehicle",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleBrand_VehicleBrandBrandId",
                table: "Vehicle",
                column: "VehicleBrandBrandId",
                principalTable: "VehicleBrand",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleModel_VehicleModelModelId",
                table: "Vehicle",
                column: "VehicleModelModelId",
                principalTable: "VehicleModel",
                principalColumn: "ModelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModel_VehicleBrand_VehicleBrandBrandId",
                table: "VehicleModel",
                column: "VehicleBrandBrandId",
                principalTable: "VehicleBrand",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
