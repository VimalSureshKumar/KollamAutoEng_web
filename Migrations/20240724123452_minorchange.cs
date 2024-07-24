using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KollamAutoEng_web.Migrations
{
    /// <inheritdoc />
    public partial class minorchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PartName",
                table: "Fault",
                newName: "PartId");

            migrationBuilder.AlterColumn<string>(
                name: "FaultName",
                table: "Fault",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fault_Part_PartId1",
                table: "Fault");

            migrationBuilder.DropIndex(
                name: "IX_Fault_PartId1",
                table: "Fault");

            migrationBuilder.DropColumn(
                name: "PartId1",
                table: "Fault");

            migrationBuilder.RenameColumn(
                name: "PartId",
                table: "Fault",
                newName: "PartName");

            migrationBuilder.AlterColumn<string>(
                name: "FaultName",
                table: "Fault",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);
        }
    }
}
