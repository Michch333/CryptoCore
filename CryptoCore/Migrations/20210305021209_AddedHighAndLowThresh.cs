using Microsoft.EntityFrameworkCore.Migrations;

namespace CryptoCore.Migrations
{
    public partial class AddedHighAndLowThresh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "HighAlertThreshold",
                table: "Coins",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "LowAlertThreshold",
                table: "Coins",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighAlertThreshold",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "LowAlertThreshold",
                table: "Coins");
        }
    }
}
