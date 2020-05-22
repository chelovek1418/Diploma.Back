using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentPerfomance.Dal.Migrations
{
    public partial class Update_Detail_Add_FK_Detail_TeacherId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Details",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_GroupSubjects_GroupId_SubjectId",
                table: "GroupSubjects",
                columns: new[] { "GroupId", "SubjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_Details_TeacherId",
                table: "Details",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Detail_TeacherId",
                table: "Details",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detail_TeacherId",
                table: "Details");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_GroupSubjects_GroupId_SubjectId",
                table: "GroupSubjects");

            migrationBuilder.DropIndex(
                name: "IX_Details_TeacherId",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Details");
        }
    }
}
