using Microsoft.EntityFrameworkCore.Migrations;

namespace TimetableManager.Repositories.Migrations
{
    public partial class EventEntityExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DaysOfWeek",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndTime",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "DaysOfWeek",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Events");
        }
    }
}
