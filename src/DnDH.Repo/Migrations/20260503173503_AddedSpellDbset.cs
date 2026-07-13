using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDH.Repo.Migrations
{
    /// <inheritdoc />
    public partial class AddedSpellDbset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpellSpellcasting_Spell_KnownSpellsId",
                table: "SpellSpellcasting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Spell",
                table: "Spell");

            migrationBuilder.RenameTable(
                name: "Spell",
                newName: "Spells");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Spells",
                table: "Spells",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpellSpellcasting_Spells_KnownSpellsId",
                table: "SpellSpellcasting",
                column: "KnownSpellsId",
                principalTable: "Spells",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpellSpellcasting_Spells_KnownSpellsId",
                table: "SpellSpellcasting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Spells",
                table: "Spells");

            migrationBuilder.RenameTable(
                name: "Spells",
                newName: "Spell");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Spell",
                table: "Spell",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpellSpellcasting_Spell_KnownSpellsId",
                table: "SpellSpellcasting",
                column: "KnownSpellsId",
                principalTable: "Spell",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
