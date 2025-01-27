using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Testamente.Web.Migrations
{
    /// <inheritdoc />
    public partial class Changeddeletebehavior : Migration
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_People_People_FatherId",
                table: "People",
                column: "FatherId",
                principalTable: "People",
                principalColumn: "PersonEntityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_People_People_MotherId",
                table: "People",
                column: "MotherId",
                principalTable: "People",
                principalColumn: "PersonEntityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_People_People_SpouseId",
                table: "People",
                column: "SpouseId",
                principalTable: "People",
                principalColumn: "PersonEntityId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
