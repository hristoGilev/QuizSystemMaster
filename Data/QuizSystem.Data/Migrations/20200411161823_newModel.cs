using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizSystem.Data.Migrations
{
    public partial class newModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionMultiSelectId",
                table: "Answers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuestionMultiSelects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    AnswerTypeA = table.Column<string>(nullable: true),
                    AnswerTypeB = table.Column<string>(nullable: true),
                    AnswerTypeC = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false),
                    ExamId = table.Column<string>(nullable: true),
                    ExamId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionMultiSelects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionMultiSelects_Exams_ExamId1",
                        column: x => x.ExamId1,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionMultiSelects_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionMultiSelectId",
                table: "Answers",
                column: "QuestionMultiSelectId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionMultiSelects_ExamId1",
                table: "QuestionMultiSelects",
                column: "ExamId1");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionMultiSelects_IsDeleted",
                table: "QuestionMultiSelects",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionMultiSelects_UserId",
                table: "QuestionMultiSelects",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_QuestionMultiSelects_QuestionMultiSelectId",
                table: "Answers",
                column: "QuestionMultiSelectId",
                principalTable: "QuestionMultiSelects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_QuestionMultiSelects_QuestionMultiSelectId",
                table: "Answers");

            migrationBuilder.DropTable(
                name: "QuestionMultiSelects");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionMultiSelectId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "QuestionMultiSelectId",
                table: "Answers");
        }
    }
}
