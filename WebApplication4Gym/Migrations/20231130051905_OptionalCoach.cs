﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4Gym.Migrations
{
    /// <inheritdoc />
    public partial class OptionalCoach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Coaches_CoachId",
                table: "Members");

            migrationBuilder.AlterColumn<string>(
                name: "CoachId",
                table: "Members",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Coaches_CoachId",
                table: "Members",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Coaches_CoachId",
                table: "Members");

            migrationBuilder.AlterColumn<string>(
                name: "CoachId",
                table: "Members",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Coaches_CoachId",
                table: "Members",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
