using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballJourney.Data.Migrations
{
    public partial class UpdatedRunModelWithOpponentsBeatenCOunt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Runs_RunId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_RunId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "RunId",
                table: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "BeatenOpponents",
                table: "Runs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeatenOpponents",
                table: "Runs");

            migrationBuilder.AddColumn<string>(
                name: "RunId",
                table: "Teams",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_RunId",
                table: "Teams",
                column: "RunId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Runs_RunId",
                table: "Teams",
                column: "RunId",
                principalTable: "Runs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
