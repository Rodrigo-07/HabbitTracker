using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabbitTrackerRodrigo.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoObjeto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habits_ApplicationUsers_UserId",
                table: "Habits");

            migrationBuilder.DropIndex(
                name: "IX_Habits_UserId",
                table: "Habits");

            migrationBuilder.AddColumn<int>(
                name: "CurrentStreak",
                table: "Habits",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxStreak",
                table: "Habits",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentStreak",
                table: "Habits");

            migrationBuilder.DropColumn(
                name: "MaxStreak",
                table: "Habits");

            migrationBuilder.CreateIndex(
                name: "IX_Habits_UserId",
                table: "Habits",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Habits_ApplicationUsers_UserId",
                table: "Habits",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
