using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Roster.Web.Migrations.RosterDb
{
    public partial class DischargeSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberDischarge",
                columns: table => new
                {
                    MemberNickname = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateOfDischarge = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DischargePath = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberDischarge", x => new { x.MemberNickname, x.Id });
                    table.ForeignKey(
                        name: "FK_MemberDischarge_Members_MemberNickname",
                        column: x => x.MemberNickname,
                        principalTable: "Members",
                        principalColumn: "Nickname",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberDischarge");
        }
    }
}
