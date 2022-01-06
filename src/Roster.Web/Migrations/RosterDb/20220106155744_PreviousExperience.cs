using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roster.Web.Migrations.RosterDb
{
    public partial class PreviousExperience : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreviousArmaExperience",
                table: "ApplicationForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousArmaModExperience",
                table: "ApplicationForms",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousArmaExperience",
                table: "ApplicationForms");

            migrationBuilder.DropColumn(
                name: "PreviousArmaModExperience",
                table: "ApplicationForms");
        }
    }
}
