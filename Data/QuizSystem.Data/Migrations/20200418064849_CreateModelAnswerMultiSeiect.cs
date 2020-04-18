using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizSystem.Data.Migrations
{
    public partial class CreateModelAnswerMultiSeiect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_QuestionMultiSelects_QuestionMultiSelectId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionMultiSelectId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "QuestionMultiSelectId",
                table: "Answers");

            migrationBuilder.CreateTable(
                name: "AnswerMultiSelects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false),
                    QuestionMultiSelectId = table.Column<string>(nullable: true),
                    QuestionMultiSelectId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerMultiSelects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerMultiSelects_QuestionMultiSelects_QuestionMultiSelectId1",
                        column: x => x.QuestionMultiSelectId1,
                        principalTable: "QuestionMultiSelects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnswerMultiSelects_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerMultiSelects_IsDeleted",
                table: "AnswerMultiSelects",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerMultiSelects_QuestionMultiSelectId1",
                table: "AnswerMultiSelects",
                column: "QuestionMultiSelectId1");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerMultiSelects_UserId",
                table: "AnswerMultiSelects",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerMultiSelects");

            migrationBuilder.AddColumn<int>(
                name: "QuestionMultiSelectId",
                table: "Answers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionMultiSelectId",
                table: "Answers",
                column: "QuestionMultiSelectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_QuestionMultiSelects_QuestionMultiSelectId",
                table: "Answers",
                column: "QuestionMultiSelectId",
                principalTable: "QuestionMultiSelects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
