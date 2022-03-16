using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommentManagement.Infrastructure.EFCore.Migrations
{
    public partial class Fixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comments",
                newName: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Comments",
                newName: "UserId");
        }
    }
}
