using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokerGameRSF.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Suit = table.Column<int>(type: "integer", nullable: false),
                    Rank = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<byte[]>(type: "bytea", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", nullable: true),
                    Rating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerId1 = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerId2 = table.Column<Guid>(type: "uuid", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    WinnerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Session_User_PlayerId1",
                        column: x => x.PlayerId1,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Session_User_PlayerId2",
                        column: x => x.PlayerId2,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Session_User_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PlacedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bets_Session_GameSessionId",
                        column: x => x.GameSessionId,
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bets_User_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerAction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Action = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: true),
                    PerformedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerAction_Session_GameSessionId",
                        column: x => x.GameSessionId,
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerAction_User_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerCard",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CardId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsInHand = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerCard_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerCard_Session_GameSessionId",
                        column: x => x.GameSessionId,
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerCard_User_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bets_GameSessionId",
                table: "Bets",
                column: "GameSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_PlayerId",
                table: "Bets",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAction_GameSessionId",
                table: "PlayerAction",
                column: "GameSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAction_PlayerId",
                table: "PlayerAction",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCard_CardId",
                table: "PlayerCard",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCard_GameSessionId",
                table: "PlayerCard",
                column: "GameSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCard_PlayerId",
                table: "PlayerCard",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_PlayerId1",
                table: "Session",
                column: "PlayerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Session_PlayerId2",
                table: "Session",
                column: "PlayerId2");

            migrationBuilder.CreateIndex(
                name: "IX_Session_WinnerId",
                table: "Session",
                column: "WinnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bets");

            migrationBuilder.DropTable(
                name: "PlayerAction");

            migrationBuilder.DropTable(
                name: "PlayerCard");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
