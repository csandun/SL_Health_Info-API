using Microsoft.EntityFrameworkCore.Migrations;

namespace CoronaApi.Migrations
{
    public partial class changemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "CoronaRecords");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "CoronaRecords");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "CoronaRecords");

            migrationBuilder.AddColumn<long>(
                name: "CasesCount",
                table: "CoronaRecords",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DeathCount",
                table: "CoronaRecords",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RecoverCount",
                table: "CoronaRecords",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SuspectCount",
                table: "CoronaRecords",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CasesCount",
                table: "CoronaRecords");

            migrationBuilder.DropColumn(
                name: "DeathCount",
                table: "CoronaRecords");

            migrationBuilder.DropColumn(
                name: "RecoverCount",
                table: "CoronaRecords");

            migrationBuilder.DropColumn(
                name: "SuspectCount",
                table: "CoronaRecords");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "CoronaRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Count",
                table: "CoronaRecords",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "CoronaRecords",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
