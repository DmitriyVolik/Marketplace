using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.Migrations
{
    public partial class EditUser1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActivated",
                table: "ConfirmationCodes");

            migrationBuilder.AddColumn<bool>(
                name: "IsActivated",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActivated",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsActivated",
                table: "ConfirmationCodes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
