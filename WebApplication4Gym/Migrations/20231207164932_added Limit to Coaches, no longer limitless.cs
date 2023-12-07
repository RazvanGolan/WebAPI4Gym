using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4Gym.Migrations
{
    /// <inheritdoc />
    public partial class addedLimittoCoachesnolongerlimitless : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Limit",
                table: "Coaches",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Limit",
                table: "Coaches");
        }
    }
}
