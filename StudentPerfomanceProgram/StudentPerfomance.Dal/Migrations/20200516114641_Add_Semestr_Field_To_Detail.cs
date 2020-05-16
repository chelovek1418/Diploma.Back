using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentPerfomance.Dal.Migrations
{
    public partial class Add_Semestr_Field_To_Detail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detail_GroupSubjecttId",
                table: "Details");

            migrationBuilder.DropIndex(
                name: "IX_Details_GroupSubjecttId",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "GroupSubjecttId",
                table: "Details");

            migrationBuilder.AddColumn<int>(
                name: "GroupSubjectId",
                table: "Details",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "Semestr",
                table: "Details",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_Details_GroupSubjectId",
                table: "Details",
                column: "GroupSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Detail_GroupSubjectId",
                table: "Details",
                column: "GroupSubjectId",
                principalTable: "GroupSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detail_GroupSubjectId",
                table: "Details");

            migrationBuilder.DropIndex(
                name: "IX_Details_GroupSubjectId",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "GroupSubjectId",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "Semestr",
                table: "Details");

            migrationBuilder.AddColumn<int>(
                name: "GroupSubjecttId",
                table: "Details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Details_GroupSubjecttId",
                table: "Details",
                column: "GroupSubjecttId");

            migrationBuilder.AddForeignKey(
                name: "FK_Detail_GroupSubjecttId",
                table: "Details",
                column: "GroupSubjecttId",
                principalTable: "GroupSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
