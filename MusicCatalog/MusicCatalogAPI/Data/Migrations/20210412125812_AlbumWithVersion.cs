using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicCatalogAPI.Migrations
{
    public partial class AlbumWithVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Albums");
        }
    }
}
