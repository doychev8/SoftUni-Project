using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballJourney.Data.Migrations
{
    public partial class RemovedMoneyFromRun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Money",
                table: "Runs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Money",
                table: "Runs",
                nullable: false,
                defaultValue: 0);
        }
    }
}
