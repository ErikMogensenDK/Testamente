using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Testamente.Web.Migrations
{
    /// <inheritdoc />
    public partial class Removedforeignkeysandmodelbuilderstuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_People_FatherId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_People_People_MotherId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_People_People_SpouseId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_FatherId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_MotherId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_SpouseId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "FatherId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "MotherId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "SpouseId",
                table: "People");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FatherId",
                table: "People",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MotherId",
                table: "People",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SpouseId",
                table: "People",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_FatherId",
                table: "People",
                column: "FatherId");

            migrationBuilder.CreateIndex(
                name: "IX_People_MotherId",
                table: "People",
                column: "MotherId");

            migrationBuilder.CreateIndex(
                name: "IX_People_SpouseId",
                table: "People",
                column: "SpouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_People_FatherId",
                table: "People",
                column: "FatherId",
                principalTable: "People",
                principalColumn: "PersonEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_People_MotherId",
                table: "People",
                column: "MotherId",
                principalTable: "People",
                principalColumn: "PersonEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_People_SpouseId",
                table: "People",
                column: "SpouseId",
                principalTable: "People",
                principalColumn: "PersonEntityId");
        }
    }
}
