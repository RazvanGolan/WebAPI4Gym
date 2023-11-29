using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4Gym.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Members",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Members");

            migrationBuilder.AddColumn<string>(
                name: "CoachId",
                table: "Members",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_CoachId",
                table: "Members",
                column: "CoachId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Coaches_CoachId",
                table: "Members",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id");
        }
    }
}
