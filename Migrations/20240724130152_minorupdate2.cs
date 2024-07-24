using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KollamAutoEng_web.Migrations
{
    /// <inheritdoc />
    public partial class minorupdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fault_Part_PartId1",
                table: "Fault");

            migrationBuilder.DropIndex(
                name: "IX_Fault_PartId1",
                table: "Fault");

            migrationBuilder.DropColumn(
                name: "PartId",
                table: "Fault");

            migrationBuilder.DropColumn(
                name: "PartId1",
                table: "Fault");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PartId",
                table: "Fault",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PartId1",
                table: "Fault",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Fault_PartId1",
                table: "Fault",
                column: "PartId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Fault_Part_PartId1",
                table: "Fault",
                column: "PartId1",
                principalTable: "Part",
                principalColumn: "PartId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
