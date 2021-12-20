using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Roster.Web.Migrations.RosterDb
{
    public partial class AddDlc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arma3Dlc");

            migrationBuilder.CreateTable(
                name: "Dlcs",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dlcs", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "OwnedDlc",
                columns: table => new
                {
                    ApplicationFormNickname = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnedDlc", x => new { x.ApplicationFormNickname, x.Id });
                    table.ForeignKey(
                        name: "FK_OwnedDlc_ApplicationForms_ApplicationFormNickname",
                        column: x => x.ApplicationFormNickname,
                        principalTable: "ApplicationForms",
                        principalColumn: "Nickname",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dlcs");

            migrationBuilder.DropTable(
                name: "OwnedDlc");

            migrationBuilder.CreateTable(
                name: "Arma3Dlc",
                columns: table => new
                {
                    ApplicationFormNickname = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arma3Dlc", x => new { x.ApplicationFormNickname, x.Id });
                    table.ForeignKey(
                        name: "FK_Arma3Dlc_ApplicationForms_ApplicationFormNickname",
                        column: x => x.ApplicationFormNickname,
                        principalTable: "ApplicationForms",
                        principalColumn: "Nickname",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
