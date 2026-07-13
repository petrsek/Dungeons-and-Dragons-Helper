using DnDH.Business.UserHandling.PermisionStrategy;
using DnDH.Repo;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DnDH.Business.DTOs
{
    /// <summary>
    /// Provides extension methods for mapping entities to data transfer objects (DTOs).
    /// </summary>
    public static class MapExtensions
    {
        /// <summary>
        /// Creates a UserDTO from a User entity
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static UserDTO MapToDTO(this User user) => new UserDTO(user.Id, user.Username,
                PermissionStrategyFactory.CreatePermissionStrategy(user.Id, user.Role));

        /// <summary>
        /// Creates a CampaignDTO from a Campaign entity
        /// </summary>
        /// <param name="campaign"></param>
        /// <returns></returns>
        public static CampaignDTO MapToDTO(this Campaign campaign) => new CampaignDTO(campaign.Id, campaign.Name, campaign.AuthorId);

        /// <summary>
        /// Creates a NoteDTO from a Note entity
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public static NoteDTO MapToDTO(this Note note) => new NoteDTO
        {
            Id = note.Id,
            TimeOfCreation = note.TimeOfCreation,
            TimeOfUpdate = note.TimeOfUpdate,
            Title = note.Title,
            Text = note.Text,
            CampaignId = note.CampaignId
        };


        /// <summary>
        /// Creates a CharacterDTO from a Character entity
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static CharacterDTO MapToDTO(this Character character) => new CharacterDTO
        {
            Id = character.Id,
            OwnerId = character.OwnerId,
            Name = character.Name,
            Level = character.Level,
            Race = character.Race,
            Class = character.Class,
            Speed = character.Speed,
            ArmorClass = character.ArmorClass,
            Skills = character.Skills,
            SavingThrows = character.SavingThrows,
            OtherProficiencies = character.OtherProficiencies,
            Health = character.Health?.MapToDTO() ?? new HealthDTO(),
            Inventory = character.Inventory?.MapToDTO() ?? new InventoryDTO(),
            Abilities = character.Abilities?.MapToDTO() ?? new AbilitiesDTO(),
            Spellcasting = character.Spellcasting?.MapToDTO()
        };


        /// <summary>
        /// Creates a HealthDTO from a Health entity
        /// </summary>
        /// <param name="health"></param>
        /// <returns></returns>
        public static HealthDTO MapToDTO(this Health health) => new HealthDTO(
            health.HitPoints,
            health.MaxHitPoints,
            health.HitDice,
            health.MaxHitDice
        );


        /// <summary>
        /// Creates an InventoryDTO from an Inventory entity
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public static InventoryDTO MapToDTO(this Inventory inventory) => new InventoryDTO(
            inventory.Gold,
            inventory.EquippedItems,
            inventory.OtherItems
        );


        /// <summary>
        /// Creates an AbilitiesDTO from an Abilities entity
        /// </summary>
        /// <param name="abilities"></param>
        /// <returns></returns>
        public static AbilitiesDTO MapToDTO(this Abilities abilities) => new AbilitiesDTO(
            abilities.Strength,
            abilities.Dexterity,
            abilities.Constitution,
            abilities.Intelligence,
            abilities.Wisdom,
            abilities.Charisma,
            abilities.StrengthModifier,
            abilities.DexterityModifier,
            abilities.ConstitutionModifier,
            abilities.IntelligenceModifier,
            abilities.WisdomModifier,
            abilities.CharismaModifier
        );

        /// <summary>
        /// Creates a SpellcastingDTO from a Spellcasting entity
        /// </summary>
        /// <param name="spellcasting"></param>
        /// <returns></returns>
        public static SpellcastingDTO MapToDTO(this Spellcasting spellcasting) => new SpellcastingDTO
        {
            SpellcastingAbility = spellcasting.SpellcastingAbility,
            SpellSlots = spellcasting.SpellSlots,
            MaxSpellSlots = spellcasting.MaxSpellSlots,
            KnownSpells = spellcasting.KnownSpells.Select(s => s.MapToDTO()).ToList()
        };


        /// <summary>
        /// Creates a SpellDTO from a Spell entity
        /// </summary>
        /// <param name="spell"></param>
        /// <returns></returns>
        public static SpellDTO MapToDTO(this Spell spell) => new SpellDTO(
            spell.Id,
            spell.Name,
            spell.Level,
            spell.Range,
            spell.Components,
            spell.Description
        );

        /// <summary>
        /// Creates a CharacterListItemDTO from a Character entity
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static CharacterListItemDTO MapToListItemDTO(this Character character) => new CharacterListItemDTO(
            character.Id,
            character.OwnerId,
            character.Name,
            character.Level,
            character.Class,
            character.Race
            );

        /// <summary>
        /// Creates a CharacterStateDTO from a CharacterStateInCampaign entity
        /// </summary>
        /// <param name="characterState"></param>
        /// <returns></returns>
        public static CharacterStateDTO MapToDTO(this CharacterStateInCampaign characterState) => new CharacterStateDTO (
            characterState.CharacterId, 
            characterState.Character.Name, 
            characterState.CampaignId, 
            characterState.State
            );
    }
}
