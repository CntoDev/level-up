using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Roster.Web.Migrations.ProcessDb
{
    public partial class RecruitmentSaga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecruitmentSaga",
                columns: table => new
                {
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nickname = table.Column<string>(type: "text", nullable: true),
                    RecruitmentStartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModsCheckDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    BootcampCompletionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EnoughAttendedEvents = table.Column<bool>(type: "boolean", nullable: false),
                    TrialSucceeded = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitmentSaga", x => x.CorrelationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecruitmentSaga");
        }
    }
}
