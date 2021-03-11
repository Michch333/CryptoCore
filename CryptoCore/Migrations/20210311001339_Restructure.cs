using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CryptoCore.Migrations
{
    public partial class Restructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighAlertThreshold",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "LowAlertThreshold",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Coins");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Coins",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AllWalletInfo",
                columns: table => new
                {
                    EntryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    Symbol = table.Column<string>(nullable: true),
                    UserHigh = table.Column<double>(nullable: false),
                    UserLow = table.Column<double>(nullable: false),
                    TimeAddedToWallet = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllWalletInfo", x => x.EntryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllWalletInfo");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Coins");

            migrationBuilder.AddColumn<float>(
                name: "HighAlertThreshold",
                table: "Coins",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "LowAlertThreshold",
                table: "Coins",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Coins",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
