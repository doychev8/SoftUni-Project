using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballJourney.Data.Migrations
{
    public partial class AddedAdditionalPropertiesToRunModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasBoughtConsumable",
                table: "Runs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasMadeTransfer",
                table: "Runs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBoughtConsumable",
                table: "Runs");

            migrationBuilder.DropColumn(
                name: "HasMadeTransfer",
                table: "Runs");
        }
    }
}
