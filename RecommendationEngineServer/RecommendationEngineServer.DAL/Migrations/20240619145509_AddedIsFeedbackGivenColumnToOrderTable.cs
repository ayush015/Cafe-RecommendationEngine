using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecommendationEngineServer.DAL.Migrations
{
    public partial class AddedIsFeedbackGivenColumnToOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_FoodItems_FoodItemId",
                table: "Feedbacks");

            migrationBuilder.RenameColumn(
                name: "FoodItemId",
                table: "Feedbacks",
                newName: "MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_FoodItemId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_MenuId");

            migrationBuilder.AddColumn<bool>(
                name: "IsFeedbackGiven",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Menu_MenuId",
                table: "Feedbacks",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Menu_MenuId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "IsFeedbackGiven",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                table: "Feedbacks",
                newName: "FoodItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_MenuId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_FoodItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_FoodItems_FoodItemId",
                table: "Feedbacks",
                column: "FoodItemId",
                principalTable: "FoodItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
