using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class mmmmmm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserImg_Users_UserId",
                table: "UserImg");

            migrationBuilder.DropIndex(
                name: "IX_UserImg_UserId",
                table: "UserImg");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserImg");

            migrationBuilder.AddColumn<int>(
                name: "UserImgId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserImgId",
                table: "Users",
                column: "UserImgId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserImg_UserImgId",
                table: "Users",
                column: "UserImgId",
                principalTable: "UserImg",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserImg_UserImgId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserImgId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserImgId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserImg",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserImg_UserId",
                table: "UserImg",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserImg_Users_UserId",
                table: "UserImg",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
