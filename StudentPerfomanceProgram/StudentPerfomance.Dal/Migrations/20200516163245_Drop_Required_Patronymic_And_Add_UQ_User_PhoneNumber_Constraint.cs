using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentPerfomance.Dal.Migrations
{
    public partial class Drop_Required_Patronymic_And_Add_UQ_User_PhoneNumber_Constraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UQ_User_Email",
                table: "Users",
                newName: "UQ_User_PhoneNumber");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                unicode: false,
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UQ_User_PhoneNumber",
                table: "Users",
                newName: "UQ_User_Email");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 15);
        }
    }
}
