using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountManagement.Infrastructure.EFCore.Migrations
{
    public partial class RelationsEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountClaimItems");

            migrationBuilder.AddColumn<int>(
                name: "ClaimType",
                table: "AccountClaims",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimType",
                table: "AccountClaims");

            migrationBuilder.CreateTable(
                name: "AccountClaimItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountClaimId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountClaimItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountClaimItems_AccountClaims_AccountClaimId",
                        column: x => x.AccountClaimId,
                        principalTable: "AccountClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountClaimItems_AccountClaimId",
                table: "AccountClaimItems",
                column: "AccountClaimId");
        }
    }
}
