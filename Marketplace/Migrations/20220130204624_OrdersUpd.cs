using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.Migrations
{
    public partial class OrdersUpd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_BuyerId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_BuyerId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress",
                table: "Orders",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                table: "Orders");

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

            migrationBuilder.AddColumn<string>(
                name: "State",
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
    }
}
