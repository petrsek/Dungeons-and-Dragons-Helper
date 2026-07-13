using DnDH.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Business.Services
{
    /// <summary>
    /// Defines the contract for character-related operations within the application.
    /// </summary>
    public interface ICharacterService
    {
        /// <summary>
        /// Creates a new character with the specified name belonging to the current user.
        /// </summary>
        /// <param name="name"></param>
        /// <remarks>
        /// Automatically commits to the database.
        /// </remarks>
        CharacterDTO Add(string name);

        /// <summary>
        /// Deletes the character with the specified id
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// Automatically commits to the database.
        /// </remarks>
        void Delete(int id);

        /// <summary>
        /// Lists all characters that are available to the current user
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Keeps only limited information about characters, such as id, name, level
        /// </remarks>
        IReadOnlyList<CharacterListItemDTO> GetAllAvailable();

        /// <summary>
        /// Finds character by its id. Returns CharacterDTO.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CharacterDTO GetDetail(int id);

        /// <summary>
        /// Updates the character with the specified id. Returns the updated CharacterDTO.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="level"></param>
        /// <param name="race"></param>
        /// <param name="c_class"></param>
        /// <param name="speed"></param>
        /// <param name="armor_class"></param>
        /// <param name="skills"></param>
        /// <param name="SavingThrows"></param>
        /// <param name="OtherProficiencies"></param>
        /// <returns></returns>
        /// <remarks>
        /// Automatically commits to database.
        /// </remarks>
        CharacterDTO UpdateBase(int id, 
            string? name = null,
            int? level = null,
            string? race = null,
            string? c_class = null,
            int? speed = null,
            int? armor_class = null,
            List<string>? skills = null,
            List<string>? SavingThrows = null,
            List<string>? OtherProficiencies = null);

        /// <summary>
        /// Updates the health-related properties of the character with the specified id. Returns the updated CharacterDTO.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="currentHealth"></param>
        /// <param name="maxHealth"></param>
        /// <param name="currentHitDie"></param>
        /// <param name="maxHitDie"></param>
        /// <returns></returns>
        /// <remarks>
        /// Automatically commits to database.
        /// </remarks>
        CharacterDTO UpdateHealth(int id, 
            int? currentHealth = null, 
            int? maxHealth = null, 
            string? currentHitDie = null, 
            string? maxHitDie = null);

        /// <summary>
        /// Updates the abilities of the character with the specified id. Returns the updated CharacterDTO.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="strength"></param>
        /// <param name="dexterity"></param>
        /// <param name="constitution"></param>
        /// <param name="intelligence"></param>
        /// <param name="wisdom"></param>
        /// <param name="charisma"></param>
        /// <returns></returns>
        /// <remarks>
        /// Automatically commits to database.
        /// </remarks>
        CharacterDTO UpdateAbilities(int id,
            int? strength = null,
            int? dexterity = null,
            int? constitution = null,
            int? intelligence = null,
            int? wisdom = null,
            int? charisma = null);

        /// <summary>
        /// Updates the inventory of the character with the specified id. Returns the updated CharacterDTO.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gold"></param>
        /// <param name="equippedItems"></param>
        /// <param name="otherItems"></param>
        /// <returns></returns>
        /// <remarks>
        /// Automatically commits to database.
        /// </remarks>
        CharacterDTO UpdateInventory(int id, 
            int? gold = null,
            List<string>? equippedItems = null,
            List<string>? otherItems = null);

        /// <summary>
        /// Adds spellcasting ability to the character with the specified id. Returns the updated CharacterDTO.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CharacterDTO AddSpellcasting(int id);

        /// <summary>
        /// Updates the spellcasting attributes of the character with the specified id. Returns the updated CharacterDTO.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="spellcastingAbility"></param>
        /// <param name="spellSlots"></param>
        /// <param name="maxSpellSlots"></param>
        /// <returns></returns>
        CharacterDTO UpdateSpellcasting(int id,
            string? spellcastingAbility = null,
            List<int>? spellSlots = null,
            List<int>? maxSpellSlots = null);

        /// <summary>
        /// Adds a spell to the character's spell list. Returns the updated CharacterDTO.
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="spellId"></param>
        /// <returns></returns>
        CharacterDTO AddSpell(int characterId, int spellId);

        /// <summary>
        /// Removes a spell from the character's spell list. Returns the updated CharacterDTO.
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="spellId"></param>
        /// <returns></returns>
        CharacterDTO RemoveSpell(int characterId, int spellId);

        /// <summary>
        /// Removes spellcasting abilities from the character with the specified identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CharacterDTO RemoveSpellcasting(int id);
    }
}
