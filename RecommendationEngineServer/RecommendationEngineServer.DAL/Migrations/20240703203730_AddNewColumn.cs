using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecommendationEngineServer.DAL.Migrations
{
    public partial class AddNewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserMenuFeedbackAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MenuFeedbackQuestionId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMenuFeedbackAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMenuFeedbackAnswer_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMenuFeedbackAnswer_MenuFeedbackQuestion_MenuFeedbackQuestionId",
                        column: x => x.MenuFeedbackQuestionId,
                        principalTable: "MenuFeedbackQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMenuFeedbackAnswer_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuFeedbackAnswer_MenuFeedbackQuestionId",
                table: "UserMenuFeedbackAnswer",
                column: "MenuFeedbackQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuFeedbackAnswer_MenuId",
                table: "UserMenuFeedbackAnswer",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuFeedbackAnswer_UserId",
                table: "UserMenuFeedbackAnswer",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
   
            migrationBuilder.CreateTable(
                name: "UserMenuFeedbackAsnwer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuFeedbackQuestionId = table.Column<int>(type: "int", nullable: true),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImprovementQuestionId = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_UserMenuFeedbackAsnwer_MenuFeedbackQuestion_MenuFeedbackQuestionId",
                        column: x => x.MenuFeedbackQuestionId,
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
                name: "IX_UserMenuFeedbackAsnwer_MenuFeedbackQuestionId",
                table: "UserMenuFeedbackAsnwer",
                column: "MenuFeedbackQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuFeedbackAsnwer_MenuId",
                table: "UserMenuFeedbackAsnwer",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuFeedbackAsnwer_UserId",
                table: "UserMenuFeedbackAsnwer",
                column: "UserId");
        }
    }
}
