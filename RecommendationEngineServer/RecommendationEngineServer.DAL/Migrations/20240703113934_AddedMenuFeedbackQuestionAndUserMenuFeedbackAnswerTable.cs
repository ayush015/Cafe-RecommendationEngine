using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecommendationEngineServer.DAL.Migrations
{
    public partial class AddedMenuFeedbackQuestionAndUserMenuFeedbackAnswerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuFeedbackQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuFeedbackQuestion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserMenuFeedbackAsnwer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ImprovementQuestionId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    ImprovementQuestionsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMenuFeedbackAsnwer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMenuFeedbackAsnwer_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMenuFeedbackAsnwer_MenuFeedbackQuestion_ImprovementQuestionsId",
                        column: x => x.ImprovementQuestionsId,
                        principalTable: "MenuFeedbackQuestion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserMenuFeedbackAsnwer_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuFeedbackAsnwer_ImprovementQuestionsId",
                table: "UserMenuFeedbackAsnwer",
                column: "ImprovementQuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuFeedbackAsnwer_MenuId",
                table: "UserMenuFeedbackAsnwer",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuFeedbackAsnwer_UserId",
                table: "UserMenuFeedbackAsnwer",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMenuFeedbackAsnwer");

            migrationBuilder.DropTable(
                name: "MenuFeedbackQuestion");
        }
    }
}
