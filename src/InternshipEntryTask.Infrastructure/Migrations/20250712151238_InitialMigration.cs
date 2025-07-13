using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InternshipEntryTask.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    anonymous_player_x_id = table.Column<Guid>(type: "uuid", nullable: true),
                    anonymous_player_o_id = table.Column<Guid>(type: "uuid", nullable: true),
                    next_move = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    width = table.Column<int>(type: "integer", nullable: false),
                    height = table.Column<int>(type: "integer", nullable: false),
                    join_key = table.Column<Guid>(type: "uuid", nullable: false),
                    LastMoveHash = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_games", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "moves",
                columns: table => new
                {
                    access_key_player = table.Column<Guid>(type: "uuid", nullable: false),
                    game_id = table.Column<long>(type: "bigint", nullable: false),
                    cell_value = table.Column<int>(type: "integer", nullable: false),
                    move_index = table.Column<int>(type: "integer", nullable: false),
                    row = table.Column<int>(type: "integer", nullable: false),
                    column = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moves", x => new { x.game_id, x.access_key_player });
                    table.ForeignKey(
                        name: "FK_moves_games_game_id",
                        column: x => x.game_id,
                        principalTable: "games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_games_anonymous_player_o_id",
                table: "games",
                column: "anonymous_player_o_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_games_anonymous_player_x_id",
                table: "games",
                column: "anonymous_player_x_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_games_join_key",
                table: "games",
                column: "join_key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "moves");

            migrationBuilder.DropTable(
                name: "games");
        }
    }
}
