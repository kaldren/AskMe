using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AskMe.Data.Migrations
{
    public partial class AnswersFix3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_AspNetUsers_NickName",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "NickName",
                table: "Answers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_NickName",
                table: "Answers",
                newName: "IX_Answers_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_AspNetUsers_UserId",
                table: "Answers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_AspNetUsers_UserId",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Answers",
                newName: "NickName");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_UserId",
                table: "Answers",
                newName: "IX_Answers_NickName");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_AspNetUsers_NickName",
                table: "Answers",
                column: "NickName",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
