using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Testamente.Web.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAlive = table.Column<bool>(type: "bit", nullable: false),
                    MotherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FatherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SpouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonEntityId);
                    table.ForeignKey(
                        name: "FK_People_People_FatherId",
                        column: x => x.FatherId,
                        principalTable: "People",
                        principalColumn: "PersonEntityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_People_MotherId",
                        column: x => x.MotherId,
                        principalTable: "People",
                        principalColumn: "PersonEntityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_People_SpouseId",
                        column: x => x.SpouseId,
                        principalTable: "People",
                        principalColumn: "PersonEntityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportSections",
                columns: table => new
                {
                    ReportSectionEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportSections", x => x.ReportSectionEntityId);
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "ReportSections");
        }
    }
}
