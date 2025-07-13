using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InternshipEntryTask.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_moves",
                table: "moves");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "moves",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_moves",
                table: "moves",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_moves_game_id",
                table: "moves",
                column: "game_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_moves",
                table: "moves");

            migrationBuilder.DropIndex(
                name: "IX_moves_game_id",
                table: "moves");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "moves");

            migrationBuilder.AddPrimaryKey(
                name: "PK_moves",
                table: "moves",
                columns: new[] { "game_id", "access_key_player" });
        }
    }
}
