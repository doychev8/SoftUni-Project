using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballJourney.Data.Migrations
{
    public partial class AddedCurrentOpponentToRun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentOpponentId",
                table: "Runs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Runs_CurrentOpponentId",
                table: "Runs",
                column: "CurrentOpponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Runs_Teams_CurrentOpponentId",
                table: "Runs",
                column: "CurrentOpponentId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Runs_Teams_CurrentOpponentId",
                table: "Runs");

            migrationBuilder.DropIndex(
                name: "IX_Runs_CurrentOpponentId",
                table: "Runs");

            migrationBuilder.DropColumn(
                name: "CurrentOpponentId",
                table: "Runs");
        }
    }
}
