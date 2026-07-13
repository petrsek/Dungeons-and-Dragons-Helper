using DnDH.Business.DTOs;
using DnDH.Business.UserHandling;
using DnDH.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Business.Services
{
    /// <summary>
    /// A concrete implementation of ICharacterService using Entity Framework Core.
    /// </summary>
    public class CharacterService : AbstractService, ICharacterService
    {
        public CharacterService(IUserContext userContext, AppDbContext dbContext) : base(userContext, dbContext)
        {
        }

        public CharacterDTO Add(string name)
        {
            UserLoginCheck();
            if (!_userContext.User!.Permissions.CanCreateCharacters) 
                throw new InvalidOperationException("User does not have permission to create characters");

            var character = new Character(name, _userContext.User!.Id);
            _dbContext.Characters.Add(character);
            _dbContext.SaveChangesAsync();
            return character.MapToDTO();
        }

        public void Delete(int id)
        {
            UserLoginCheck();

            var character = _userContext.User!.Permissions.CanDelete(_dbContext.Characters.Where(c => c.Id == id)).FirstOrDefault();
            if (character is null)
            {
                throw new InvalidOperationException($"Character with id {id} not found or user does not have permission to delete it.");
            }
            _dbContext.Characters.Remove(character);
            _dbContext.SaveChangesAsync();
        }

        public IReadOnlyList<CharacterListItemDTO> GetAllAvailable()
        {
            UserLoginCheck();
            var characters = _userContext.User!.Permissions.CanView(_dbContext.Characters);
            return characters.Select(c => c.MapToListItemDTO()).ToList();
        }

        public CharacterDTO GetDetail(int id)
        {
            UserLoginCheck();

            var character = _userContext.User!.Permissions.CanViewDetail(_dbContext.Characters.Where(c => c.Id == id))
                .Include(c => c.Spellcasting).ThenInclude(s => s!.KnownSpells)
                .FirstOrDefault();

            if (character is null)
            {
                throw new InvalidOperationException($"Character with id {id} not found or user does not have permission to view its detail.");
            }
            return character.MapToDTO();
        }

        private Character CheckPermissionsBeforeUpdate(int id)
        {
            UserLoginCheck();
            var character = _userContext.User!.Permissions.CanEdit(_dbContext.Characters.Where(c => c.Id == id)).FirstOrDefault();
            if (character is null)
            {
                throw new InvalidOperationException($"Character with id {id} not found or user does not have permission to edit it.");
            }

            return character;
        }

        public CharacterDTO UpdateAbilities(int id, int? strength = null, int? dexterity = null, int? constitution = null, int? intelligence = null, int? wisdom = null, int? charisma = null)
        {
            var character = CheckPermissionsBeforeUpdate(id);

            if (strength is not null) character.Abilities.Strength = strength.Value;
            if (dexterity is not null) character.Abilities.Dexterity = dexterity.Value;
            if (constitution is not null) character.Abilities.Constitution = constitution.Value;
            if (intelligence is not null) character.Abilities.Intelligence = intelligence.Value;
            if (wisdom is not null) character.Abilities.Wisdom = wisdom.Value;
            if (charisma is not null) character.Abilities.Charisma = charisma.Value;

            _dbContext.SaveChangesAsync();
            return character.MapToDTO();
        }


        public CharacterDTO UpdateBase(int id, string? name = null, int? level = null, string? race = null, string? c_class = null, int? speed = null, int? armor_class = null, List<string>? skills = null, List<string>? SavingThrows = null, List<string>? OtherProficiencies = null)
        {
            var character = CheckPermissionsBeforeUpdate(id);

            if (name is not null) character.Name = name;
            if (level is not null) character.Level = level.Value;
            if (race is not null) character.Race = race;
            if (c_class is not null) character.Class = c_class;
            if (speed is not null) character.Speed = speed.Value;
            if (armor_class is not null) character.ArmorClass = armor_class.Value;
            if (skills is not null) character.Skills = skills;
            if (SavingThrows is not null) character.SavingThrows = SavingThrows;
            if (OtherProficiencies is not null) character.OtherProficiencies = OtherProficiencies;

            _dbContext.SaveChangesAsync();
            return character.MapToDTO();
        }

        public CharacterDTO UpdateHealth(int id, int? currentHealth = null, int? maxHealth = null, string? currentHitDie = null, string? maxHitDie = null)
        {
            var character = CheckPermissionsBeforeUpdate(id);

            if (currentHealth is not null) character.Health.HitPoints = currentHealth.Value;
            if (maxHealth is not null) character.Health.MaxHitPoints = maxHealth.Value;
            if (currentHitDie is not null) character.Health.HitDice = currentHitDie;
            if (maxHitDie is not null) character.Health.MaxHitDice = maxHitDie;

            _dbContext.SaveChangesAsync();
            return character.MapToDTO();
        }

        public CharacterDTO UpdateInventory(int id, int? gold = null, List<string>? equippedItems = null, List<string>? otherItems = null)
        {
            var character = CheckPermissionsBeforeUpdate(id);

            if (gold is not null) character.Inventory.Gold = gold.Value;
            if (equippedItems is not null) character.Inventory.EquippedItems = equippedItems;
            if (otherItems is not null) character.Inventory.OtherItems = otherItems;

            _dbContext.SaveChangesAsync();
            return character.MapToDTO();
        }

        public CharacterDTO AddSpellcasting(int id)
        {
            var character = CheckPermissionsBeforeUpdate(id);

            if (character.Spellcasting is not null)
            {
                throw new InvalidOperationException($"Character with id {id} already has spellcasting initialized.");
            }

            character.Spellcasting = new Spellcasting();
            _dbContext.SaveChangesAsync();
            return character.MapToDTO();
        }

        public CharacterDTO UpdateSpellcasting(int id, string? spellcastingAbility = null, List<int>? spellSlots = null, List<int>? maxSpellSlots = null)
        {
            var character = CheckPermissionsBeforeUpdate(id);

            if (character.Spellcasting is null)
            {
                throw new InvalidOperationException($"Character with id {id} does not have spellcasting initialized.");
            }

            if (spellcastingAbility is not null) character.Spellcasting.SpellcastingAbility = spellcastingAbility;
            if (spellSlots is not null) character.Spellcasting.SpellSlots = spellSlots;
            if (maxSpellSlots is not null) character.Spellcasting.MaxSpellSlots = maxSpellSlots;

            _dbContext.SaveChangesAsync();
            return character.MapToDTO();
        }

        public CharacterDTO AddSpell(int characterId, int spellId)
        {
            var character = CheckPermissionsBeforeUpdate(characterId);

            if (character.Spellcasting is null)
            {
                throw new InvalidOperationException($"Character with id {characterId} does not have spellcasting initialized.");
            }

            var spell = _dbContext.Spells.Find(spellId);
            if (spell is null)
            {
                throw new InvalidOperationException($"Spell with id {spellId} does not exist.");
            }
            if (character.Spellcasting.KnownSpells.Any(s => s.Id == spellId))
            {
                throw new InvalidOperationException($"Spell with id {spellId} is already known by the character.");
            }

            character.Spellcasting.KnownSpells.Add(spell);
            _dbContext.SaveChangesAsync();
            return character.MapToDTO();
        }

        public CharacterDTO RemoveSpellcasting(int id)
        {
            var character = CheckPermissionsBeforeUpdate(id);

            if (character.Spellcasting is null)
            {
                throw new InvalidOperationException($"Character with id {id} does not have spellcasting initialized.");
            }

            _dbContext.CharacterSpellcasting.Remove(character.Spellcasting);
            character.Spellcasting = null;
            
            _dbContext.SaveChangesAsync();
            return character.MapToDTO();
        }


        public CharacterDTO RemoveSpell(int characterId, int spellId)
        {
            var character = CheckPermissionsBeforeUpdate(characterId);

            if (character.Spellcasting is null)
            {
                throw new InvalidOperationException($"Character with id {characterId} does not have spellcasting initialized.");
            }

            var spell = character.Spellcasting.KnownSpells.FirstOrDefault(s => s.Id == spellId);
            if (spell is null)
            {
                throw new InvalidOperationException($"Spell with id {spellId} is not known by the character.");
            }

            character.Spellcasting.KnownSpells.Remove(spell);
            _dbContext.SaveChangesAsync();
            return character.MapToDTO();
        }
    }
}
