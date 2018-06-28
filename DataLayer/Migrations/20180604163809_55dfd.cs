using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class _55dfd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Chat_UserSendId",
                table: "Chat",
                column: "UserSendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Contacts_UserSendId",
                table: "Chat",
                column: "UserSendId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Contacts_UserSendId",
                table: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Chat_UserSendId",
                table: "Chat");
        }
    }
}
