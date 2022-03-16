using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommentManagement.Infrastructure.EFCore.Migrations
{
    public partial class UserIdEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Comments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
