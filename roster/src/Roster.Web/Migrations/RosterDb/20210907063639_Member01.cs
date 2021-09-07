using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Roster.Web.Migrations.RosterDb
{
    public partial class Member01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Nickname = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    BiNickname = table.Column<string>(type: "text", nullable: true),
                    SteamId = table.Column<string>(type: "text", nullable: true),
                    Gmail = table.Column<string>(type: "text", nullable: true),
                    GithubNickname = table.Column<string>(type: "text", nullable: true),
                    DiscordId = table.Column<string>(type: "text", nullable: true),
                    TeamspeakId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Nickname);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_Email",
                table: "Members",
                column: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
