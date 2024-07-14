using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecommendationEngineServer.DAL.Migrations
{
    public partial class AddedFoodTypeCusinieTypeIsSweetSpiceLevelCoulmnToMenuTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CuisineTypeId",
                table: "Menu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FoodTypeId",
                table: "Menu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsSweet",
                table: "Menu",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SpiceLevelId",
                table: "Menu",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CuisineTypeId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "FoodTypeId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "IsSweet",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "SpiceLevelId",
                table: "Menu");
        }
    }
}
