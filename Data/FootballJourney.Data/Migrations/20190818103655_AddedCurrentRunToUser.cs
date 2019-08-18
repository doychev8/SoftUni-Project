using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballJourney.Data.Migrations
{
    public partial class AddedCurrentRunToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentRunId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CurrentRunId",
                table: "AspNetUsers",
                column: "CurrentRunId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Runs_CurrentRunId",
                table: "AspNetUsers",
                column: "CurrentRunId",
                principalTable: "Runs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Runs_CurrentRunId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CurrentRunId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CurrentRunId",
                table: "AspNetUsers");
        }
    }
}
