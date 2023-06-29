using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MatchInfo.API.Migrations
{
    public partial class InitialDBCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeamA = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TeamB = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Sport = table.Column<byte>(type: "tinyint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.ID);
                    table.CheckConstraint("CK_Matches_Sport", "[Sport] = 1 OR [Sport] = 2");
                });

            migrationBuilder.CreateTable(
                name: "MatchOdds",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    Specifier = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Odd = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchOdds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MatchOdds_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "ID", "Description", "MatchDateTime", "Sport", "TeamA", "TeamB" },
                values: new object[] { 1, "OSFP-PAO", new DateTime(2023, 7, 30, 13, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, "OSFP", "PAO" });

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "ID", "Description", "MatchDateTime", "Sport", "TeamA", "TeamB" },
                values: new object[] { 2, "AEK-PAO", new DateTime(2023, 6, 29, 13, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, "AEK", "PAO" });

            migrationBuilder.InsertData(
                table: "MatchOdds",
                columns: new[] { "ID", "MatchId", "Odd", "Specifier" },
                values: new object[] { 1, 1, 1.5, "X" });

            migrationBuilder.InsertData(
                table: "MatchOdds",
                columns: new[] { "ID", "MatchId", "Odd", "Specifier" },
                values: new object[] { 2, 2, 2.2999999999999998, "1" });

            migrationBuilder.InsertData(
                table: "MatchOdds",
                columns: new[] { "ID", "MatchId", "Odd", "Specifier" },
                values: new object[] { 3, 2, 3.1000000000000001, "2" });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MatchDateTime_TeamA_TeamB_Sport",
                table: "Matches",
                columns: new[] { "MatchDateTime", "TeamA", "TeamB", "Sport" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchOdds_MatchId_Specifier",
                table: "MatchOdds",
                columns: new[] { "MatchId", "Specifier" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchOdds");

            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
