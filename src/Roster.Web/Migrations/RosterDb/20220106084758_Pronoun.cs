using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roster.Web.Migrations.RosterDb
{
    public partial class Pronoun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreferredPronouns",
                table: "ApplicationForms",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferredPronouns",
                table: "ApplicationForms");
        }
    }
}
