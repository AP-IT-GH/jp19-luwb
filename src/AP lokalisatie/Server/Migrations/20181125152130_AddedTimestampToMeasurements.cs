using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class AddedTimestampToMeasurements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YPos",
                table: "Anchors",
                newName: "YPos");

            migrationBuilder.RenameColumn(
                name: "XPos",
                table: "Anchors",
                newName: "XPos");

            migrationBuilder.AddColumn<string>(
                name: "Unix_Timestamp",
                table: "Measurements",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unix_Timestamp",
                table: "Measurements");

            migrationBuilder.RenameColumn(
                name: "YPos",
                table: "Anchors",
                newName: "YPos");

            migrationBuilder.RenameColumn(
                name: "XPos",
                table: "Anchors",
                newName: "XPos");
        }
    }
}
