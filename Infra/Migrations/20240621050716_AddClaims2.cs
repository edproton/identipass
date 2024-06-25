using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddClaims2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claim_Users_UserId",
                table: "Claim");

            migrationBuilder.DropIndex(
                name: "IX_Claim_UserId",
                table: "Claim");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Claim");

            migrationBuilder.CreateTable(
                name: "ClaimUser",
                columns: table => new
                {
                    ClaimsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimUser", x => new { x.ClaimsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ClaimUser_Claim_ClaimsId",
                        column: x => x.ClaimsId,
                        principalTable: "Claim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClaimUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimUser_UsersId",
                table: "ClaimUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimUser");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Claim",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Claim_UserId",
                table: "Claim",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Claim_Users_UserId",
                table: "Claim",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
