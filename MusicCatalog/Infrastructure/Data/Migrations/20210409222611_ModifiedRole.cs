using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class ModifiedRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Suppliers");

            migrationBuilder.RenameColumn(
                name: "Login",
                table: "Users",
                newName: "Username");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "Login");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Suppliers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
