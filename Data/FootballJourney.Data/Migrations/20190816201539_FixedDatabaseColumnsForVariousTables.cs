using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballJourney.Data.Migrations
{
    public partial class FixedDatabaseColumnsForVariousTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRuns");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRuns",
                columns: table => new
                {
                    RunId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRuns", x => new { x.RunId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserRuns_Runs_RunId",
                        column: x => x.RunId,
                        principalTable: "Runs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRuns_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRuns_UserId",
                table: "UserRuns",
                column: "UserId");
        }
    }
}
