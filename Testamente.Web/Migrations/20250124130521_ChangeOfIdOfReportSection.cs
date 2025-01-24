using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Testamente.Web.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOfIdOfReportSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.AlterColumn<int>(
            //     name: "ReportSectionEntityId",
            //     table: "ReportSections",
            //     type: "int",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(450)")
            //     .Annotation("SqlServer:Identity", "1, 1");

            // Drop existing primary key
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReportSections",
                table: "ReportSections");

            // Rename existing column
            migrationBuilder.RenameColumn(
                name: "ReportSectionEntityId",
                table: "ReportSections",
                newName: "OldReportSectionEntityId");

            // Add new identity column
            migrationBuilder.AddColumn<int>(
                name: "ReportSectionEntityId",
                table: "ReportSections",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");


            // Drop old column
            migrationBuilder.DropColumn(
                name: "OldReportSectionEntityId",
            table: "ReportSections");

            // Add new primary key
            migrationBuilder.AddPrimaryKey(
                name: "ReportSectionEntityId",
                table: "ReportSections",
                column: "ReportSectionEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReportSectionEntityId",
                table: "ReportSections",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
