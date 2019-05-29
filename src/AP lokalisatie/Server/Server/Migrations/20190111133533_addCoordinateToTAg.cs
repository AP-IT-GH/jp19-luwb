using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class addCoordinateToTAg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "XPos",
                table: "Tags",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YPos",
                table: "Tags",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "XPos",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "YPos",
                table: "Tags");
        }
    }
}
