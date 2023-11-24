using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4Gym.Migrations
{
    /// <inheritdoc />
    public partial class Member : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoachId",
                table: "Members",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Members_CoachId",
                table: "Members",
                column: "CoachId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Coaches_CoachId",
                table: "Members",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Coaches_CoachId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_CoachId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "Members");
        }
    }
}
