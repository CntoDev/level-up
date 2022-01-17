using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roster.Web.Migrations.RosterDb
{
    public partial class LanguageSkillLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageSkillLevel",
                table: "ApplicationForms",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageSkillLevel",
                table: "ApplicationForms");
        }
    }
}
