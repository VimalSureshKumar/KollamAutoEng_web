using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KollamAutoEng_web.Migrations
{
    /// <inheritdoc />
    public partial class modifyfieldnames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Payment",
                newName: "Payment_Date");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Payment",
                newName: "Payment_Amount");

            migrationBuilder.RenameColumn(
                name: "Reference",
                table: "Part",
                newName: "Part_Reference");

            migrationBuilder.RenameColumn(
                name: "PartName",
                table: "Part",
                newName: "Part_Name");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Part",
                newName: "Part_Cost");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Fault",
                newName: "Fault_Description");

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
                newName: "Customer_Reference");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Customer",
                newName: "Customer_Email");

            migrationBuilder.RenameColumn(
                name: "AppointmentDate",
                table: "Appointment",
                newName: "Appointment_Date");

            migrationBuilder.AddColumn<string>(
                name: "Customer_DateOfBirth",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Customer_DateOfBirth",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "Payment_Date",
                table: "Payment",
                newName: "Date");

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
                newName: "PartName");

            migrationBuilder.RenameColumn(
                name: "Part_Cost",
                table: "Part",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "Fault_Description",
                table: "Fault",
                newName: "Description");

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
                newName: "Email");

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
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "Appointment_Date",
                table: "Appointment",
                newName: "AppointmentDate");

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Customer",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }
    }
}
