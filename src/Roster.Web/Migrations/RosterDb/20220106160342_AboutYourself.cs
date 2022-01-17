using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roster.Web.Migrations.RosterDb
{
    public partial class AboutYourself : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutYourself",
                table: "ApplicationForms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DesiredCommunityRole",
                table: "ApplicationForms",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutYourself",
                table: "ApplicationForms");

            migrationBuilder.DropColumn(
                name: "DesiredCommunityRole",
                table: "ApplicationForms");
        }
    }
}
