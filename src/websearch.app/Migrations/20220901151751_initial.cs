using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace websearch.app.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebSearches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Query = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebSearches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebSearchResultSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebSearchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebSearchResultSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebSearchResultSets_WebSearches_WebSearchId",
                        column: x => x.WebSearchId,
                        principalTable: "WebSearches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebSearchResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    WebSearchResultSetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebSearchResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebSearchResults_WebSearchResultSets_WebSearchResultSetId",
                        column: x => x.WebSearchResultSetId,
                        principalTable: "WebSearchResultSets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WebSearchResults_WebSearchResultSetId",
                table: "WebSearchResults",
                column: "WebSearchResultSetId");

            migrationBuilder.CreateIndex(
                name: "IX_WebSearchResultSets_WebSearchId",
                table: "WebSearchResultSets",
                column: "WebSearchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebSearchResults");

            migrationBuilder.DropTable(
                name: "WebSearchResultSets");

            migrationBuilder.DropTable(
                name: "WebSearches");
        }
    }
}
