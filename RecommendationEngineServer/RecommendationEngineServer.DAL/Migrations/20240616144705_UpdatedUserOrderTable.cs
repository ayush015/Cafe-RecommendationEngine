using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecommendationEngineServer.DAL.Migrations
{
    public partial class UpdatedUserOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DailyMenus_DailyMenuId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DailyMenuId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DailyMenuId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "UserOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserOrders_OrderId",
                table: "UserOrders",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserOrders_Orders_OrderId",
                table: "UserOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOrders_Orders_OrderId",
                table: "UserOrders");

            migrationBuilder.DropIndex(
                name: "IX_UserOrders_OrderId",
                table: "UserOrders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "UserOrders");

            migrationBuilder.AddColumn<int>(
                name: "DailyMenuId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DailyMenuId",
                table: "Orders",
                column: "DailyMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DailyMenus_DailyMenuId",
                table: "Orders",
                column: "DailyMenuId",
                principalTable: "DailyMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
