using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballJourney.Data.Migrations
{
    public partial class AddedRelationBetweenUsersAndRuns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Runs_AspNetUsers_UserId",
                table: "Runs");

            migrationBuilder.AddForeignKey(
                name: "FK_Runs_AspNetUsers_UserId",
                table: "Runs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Runs_AspNetUsers_UserId",
                table: "Runs");

            migrationBuilder.AddForeignKey(
                name: "FK_Runs_AspNetUsers_UserId",
                table: "Runs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
