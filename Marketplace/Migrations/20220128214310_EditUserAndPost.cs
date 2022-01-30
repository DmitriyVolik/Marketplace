using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.Migrations
{
    public partial class EditUserAndPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "Posts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress",
                table: "Posts",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BuyerId",
                table: "Posts",
                column: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_BuyerId",
                table: "Posts",
                column: "BuyerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_BuyerId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_BuyerId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                table: "Posts");
        }
    }
}
