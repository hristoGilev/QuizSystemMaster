using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizSystem.Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ExamUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "ExamUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ExamUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "ExamUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamUsers_IsDeleted",
                table: "ExamUsers",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExamUsers_IsDeleted",
                table: "ExamUsers");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ExamUsers");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "ExamUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ExamUsers");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "ExamUsers");
        }
    }
}
