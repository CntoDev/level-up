using Microsoft.EntityFrameworkCore.Migrations;

namespace Roster.Web.Migrations.RosterDb
{
    public partial class Member03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailVerified",
                table: "Members",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailVerified",
                table: "Members");
        }
    }
}
