using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class AddMapToTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MapId",
                table: "Tags",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_MapId",
                table: "Tags",
                column: "MapId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Maps_MapId",
                table: "Tags",
                column: "MapId",
                principalTable: "Maps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Maps_MapId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_MapId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "MapId",
                table: "Tags");
        }
    }
}
