using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Roster.Web.Migrations.RosterDb
{
    public partial class Member02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VerificationCode",
                table: "Members",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VerificationTime",
                table: "Members",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "VerificationTime",
                table: "Members");
        }
    }
}
