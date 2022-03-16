using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountManagement.Infrastructure.EFCore.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreationDate", "Email", "EmailConfirmed", "FullName", "Password", "PhoneNumber", "ProfilePhoto", "Token", "UserName" },
                values: new object[] { 1, new DateTime(2022, 3, 1, 15, 4, 17, 587, DateTimeKind.Local).AddTicks(8842), "aspemail007@gmail.com", true, "مدیر سایت", "10000.YNmL5o6NPRQENvKVLgQaww==.enVFgrgZstDmlnWaXv7o/jjjL8e8F75AUgAk5ZmQEH4=", "09876543210", null, "ihkecrTZxEe/wqmVG8wN/w==", "OwnerSite" });

            migrationBuilder.InsertData(
                table: "AccountClaims",
                columns: new[] { "Id", "AccountId", "AccountManagement", "CommentManagement", "CreationDate", "DiscountManagement", "InventoryManagement", "ShopManagement" },
                values: new object[] { 1, 1, true, true, new DateTime(2022, 3, 1, 15, 4, 17, 587, DateTimeKind.Local).AddTicks(7305), true, true, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
