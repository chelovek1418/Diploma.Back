using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentPerfomance.Dal.Migrations
{
    public partial class Add_Teacher_Filter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detail_TeacherId",
                table: "Details");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Teachers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Details_Teachers_TeacherId",
                table: "Details",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Details_Teachers_TeacherId",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Teachers");

            migrationBuilder.AddForeignKey(
                name: "FK_Detail_TeacherId",
                table: "Details",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
