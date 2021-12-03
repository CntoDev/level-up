using Microsoft.EntityFrameworkCore.Migrations;

namespace Roster.Web.Migrations.RosterDb
{
    public partial class PromoteMember2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "RankId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Ranks",
                type: "integer",
                nullable: false,
                defaultValueSql: "nextval('\"RankId\"')",
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "RankId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Ranks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValueSql: "nextval('\"RankId\"')");
        }
    }
}
