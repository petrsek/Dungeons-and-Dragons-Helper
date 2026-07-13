using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDH.Repo.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Spell",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Range = table.Column<string>(type: "TEXT", nullable: false),
                    Components = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spell", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campaigns_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Race = table.Column<string>(type: "TEXT", nullable: false),
                    Class = table.Column<string>(type: "TEXT", nullable: false),
                    Speed = table.Column<int>(type: "INTEGER", nullable: false),
                    ArmorClass = table.Column<int>(type: "INTEGER", nullable: false),
                    Skills = table.Column<string>(type: "TEXT", nullable: false),
                    SavingThrows = table.Column<string>(type: "TEXT", nullable: false),
                    OtherProficiencies = table.Column<string>(type: "TEXT", nullable: false),
                    Health_HitPoints = table.Column<int>(type: "INTEGER", nullable: false),
                    Health_MaxHitPoints = table.Column<int>(type: "INTEGER", nullable: false),
                    Health_HitDice = table.Column<string>(type: "TEXT", nullable: false),
                    Health_MaxHitDice = table.Column<string>(type: "TEXT", nullable: false),
                    Inventory_Gold = table.Column<int>(type: "INTEGER", nullable: false),
                    Inventory_EquippedItems = table.Column<string>(type: "TEXT", nullable: false),
                    Inventory_OtherItems = table.Column<string>(type: "TEXT", nullable: false),
                    Abilities_Strength = table.Column<int>(type: "INTEGER", nullable: false),
                    Abilities_Dexterity = table.Column<int>(type: "INTEGER", nullable: false),
                    Abilities_Constitution = table.Column<int>(type: "INTEGER", nullable: false),
                    Abilities_Intelligence = table.Column<int>(type: "INTEGER", nullable: false),
                    Abilities_Wisdom = table.Column<int>(type: "INTEGER", nullable: false),
                    Abilities_Charisma = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimeOfCreation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeOfUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    CampaignId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterSpellcasting",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpellcastingAbility = table.Column<string>(type: "TEXT", nullable: false),
                    SpellSlots = table.Column<string>(type: "TEXT", nullable: false),
                    MaxSpellSlots = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSpellcasting", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_CharacterSpellcasting_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterStatesInCampaigns",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    CampaignId = table.Column<int>(type: "INTEGER", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterStatesInCampaigns", x => new { x.CharacterId, x.CampaignId });
                    table.ForeignKey(
                        name: "FK_CharacterStatesInCampaigns_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterStatesInCampaigns_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpellSpellcasting",
                columns: table => new
                {
                    CharacterSpellcastingCharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    KnownSpellsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellSpellcasting", x => new { x.CharacterSpellcastingCharacterId, x.KnownSpellsId });
                    table.ForeignKey(
                        name: "FK_SpellSpellcasting_CharacterSpellcasting_CharacterSpellcastingCharacterId",
                        column: x => x.CharacterSpellcastingCharacterId,
                        principalTable: "CharacterSpellcasting",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpellSpellcasting_Spell_KnownSpellsId",
                        column: x => x.KnownSpellsId,
                        principalTable: "Spell",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_AuthorId",
                table: "Campaigns",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_OwnerId",
                table: "Characters",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterStatesInCampaigns_CampaignId",
                table: "CharacterStatesInCampaigns",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CampaignId",
                table: "Notes",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_SpellSpellcasting_KnownSpellsId",
                table: "SpellSpellcasting",
                column: "KnownSpellsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterStatesInCampaigns");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "SpellSpellcasting");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "CharacterSpellcasting");

            migrationBuilder.DropTable(
                name: "Spell");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
