using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountManagement.Infrastructure.EFCore.Migrations
{
    public partial class AccountClaimTblAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimType",
                table: "AccountClaims");

            migrationBuilder.DropColumn(
                name: "ClaimValue",
                table: "AccountClaims");

            migrationBuilder.AddColumn<bool>(
                name: "AccountManagement",
                table: "AccountClaims",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CommentManagement",
                table: "AccountClaims",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DiscountManagement",
                table: "AccountClaims",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InventoryManagement",
                table: "AccountClaims",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShopManagement",
                table: "AccountClaims",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountManagement",
                table: "AccountClaims");

            migrationBuilder.DropColumn(
                name: "CommentManagement",
                table: "AccountClaims");

            migrationBuilder.DropColumn(
                name: "DiscountManagement",
                table: "AccountClaims");

            migrationBuilder.DropColumn(
                name: "InventoryManagement",
                table: "AccountClaims");

            migrationBuilder.DropColumn(
                name: "ShopManagement",
                table: "AccountClaims");

            migrationBuilder.AddColumn<string>(
                name: "ClaimType",
                table: "AccountClaims",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClaimValue",
                table: "AccountClaims",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
