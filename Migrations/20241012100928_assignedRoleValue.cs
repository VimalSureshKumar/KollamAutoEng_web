using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KollamAutoEng_web.Migrations
{
    /// <inheritdoc />
    public partial class assignedRoleValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2df3a3a8-3fd4-4801-bf9e-8b6440903330");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "895f10ba-5e89-4a78-aeb3-dde654921eae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c27090b2-8deb-46c2-a3be-a9dc3a30c658");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RoleValue",
                table: "AspNetRoles",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "RoleValue" },
                values: new object[,]
                {
                    { "1", null, "ApplicationRole", "Admin", "ADMIN", 1 },
                    { "2", null, "ApplicationRole", "Employee", "EMPLOYEE", 2 },
                    { "3", null, "ApplicationRole", "User", "USER", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "RoleValue",
                table: "AspNetRoles");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2df3a3a8-3fd4-4801-bf9e-8b6440903330", null, "User", "User" },
                    { "895f10ba-5e89-4a78-aeb3-dde654921eae", null, "Employee", "Employee" },
                    { "c27090b2-8deb-46c2-a3be-a9dc3a30c658", null, "Admin", "Admin" }
                });
        }
    }
}
