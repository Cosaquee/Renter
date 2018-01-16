using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class CurrentStatusForBookRent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Received",
                table: "RentBooks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Received",
                table: "RentBooks");
        }
    }
}
