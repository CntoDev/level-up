using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roster.Web.Migrations.RosterDb
{
    public partial class Rejoin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAlumni",
                table: "MemberDischarge",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAlumni",
                table: "MemberDischarge");
        }
    }
}
