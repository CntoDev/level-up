using Microsoft.EntityFrameworkCore.Migrations;

namespace Roster.Web.Migrations.RosterDb
{
    public partial class EventState2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventType",
                table: "EventStates",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventType",
                table: "EventStates");
        }
    }
}
