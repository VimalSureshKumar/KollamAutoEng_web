using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KollamAutoEng_web.Migrations
{
    /// <inheritdoc />
    public partial class newnewrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "31843de6-5fd2-4d72-9451-cbaf35069597", null, "User", "User" },
                    { "3caee2c9-b1cf-434c-bc64-55100c152010", null, "Admin", "Admin" },
                    { "969984f0-34b0-4c4c-99f2-9c29fc0a01c8", null, "Employee", "Employee" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31843de6-5fd2-4d72-9451-cbaf35069597");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3caee2c9-b1cf-434c-bc64-55100c152010");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "969984f0-34b0-4c4c-99f2-9c29fc0a01c8");
        }
    }
}
