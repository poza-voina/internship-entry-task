using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipEntryTask.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LastMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastMoveHash",
                table: "games");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastMoveHash",
                table: "games",
                type: "text",
                nullable: true);
        }
    }
}
