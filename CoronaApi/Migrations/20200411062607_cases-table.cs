using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoronaApi.Migrations
{
    public partial class casestable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GovId = table.Column<int>(nullable: false),
                    CaseNumber = table.Column<int>(nullable: false),
                    IsLocal = table.Column<bool>(nullable: false),
                    DetectedFrom = table.Column<string>(nullable: true),
                    GovCreated = table.Column<DateTime>(nullable: false),
                    Local = table.Column<bool>(nullable: false),
                    ReportedDate = table.Column<DateTime>(nullable: false),
                    Area = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cases");
        }
    }
}
