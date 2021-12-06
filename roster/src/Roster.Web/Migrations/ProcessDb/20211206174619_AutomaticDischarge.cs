using Microsoft.EntityFrameworkCore.Migrations;

namespace Roster.Web.Migrations.ProcessDb
{
    public partial class AutomaticDischarge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AutomaticDischarge",
                table: "RecruitmentSaga",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutomaticDischarge",
                table: "RecruitmentSaga");
        }
    }
}
