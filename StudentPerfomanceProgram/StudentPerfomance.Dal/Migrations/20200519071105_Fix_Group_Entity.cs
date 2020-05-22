using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentPerfomance.Dal.Migrations
{
    public partial class Fix_Group_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Students_StudentId1",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_StudentId1",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "Groups");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId1",
                table: "Groups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_StudentId1",
                table: "Groups",
                column: "StudentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Students_StudentId1",
                table: "Groups",
                column: "StudentId1",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
