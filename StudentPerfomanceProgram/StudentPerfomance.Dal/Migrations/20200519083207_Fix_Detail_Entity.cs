using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentPerfomance.Dal.Migrations
{
    public partial class Fix_Detail_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Detail_Pair",
                table: "Details");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_Detail_Pair",
                table: "Details",
                sql: "[Pair] >= 0 AND [Pair] <= 4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Detail_Pair",
                table: "Details");

            migrationBuilder.CreateCheckConstraint(
                name: "CK_Detail_Pair",
                table: "Details",
                sql: "[Pair] >= 1 AND [Pair] <= 5");
        }
    }
}
