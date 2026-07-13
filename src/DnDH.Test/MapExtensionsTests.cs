using DnDH.Business.DTOs;
using DnDH.Business.UserHandling.PermisionStrategy;
using DnDH.Repo;
using System.Collections.Generic;
using System.Linq;

namespace DnDH.Test
{
    [TestClass]
    public class MapExtensionsTests
    {
        [TestMethod]
        public void MapToDTO_User_ReturnsCorrectDTO()
        {
            var user = new User("testuser", UserRole.Player) { Id = 1 };

            var dto = user.MapToDTO();

            Assert.AreEqual(1, dto.Id);
            Assert.AreEqual("testuser", dto.Username);
            Assert.IsInstanceOfType(dto.Permissions, typeof(PlayerPermissionStrategy));
        }

        [TestMethod]
        public void MapToDTO_Campaign_ReturnsCorrectDTO()
        {
            var campaign = new Campaign("MyCampaign", 1) { Id = 10 };

            var dto = campaign.MapToDTO();

            Assert.AreEqual(10, dto.Id);
            Assert.AreEqual("MyCampaign", dto.Name);
            Assert.AreEqual(1, dto.AuthorId);
        }

        [TestMethod]
        public void MapToDTO_Abilities_ReturnsCorrectDTO()
        {
            var abilities = new Abilities
            {
                Strength = 10,
                Dexterity = 12,
                Constitution = 14,
                Intelligence = 16,
                Wisdom = 18,
                Charisma = 20
            };

            var dto = abilities.MapToDTO();

            Assert.AreEqual(10, dto.Strength);
            Assert.AreEqual(12, dto.Dexterity);
            Assert.AreEqual(14, dto.Constitution);
            Assert.AreEqual(16, dto.Intelligence);
            Assert.AreEqual(18, dto.Wisdom);
            Assert.AreEqual(20, dto.Charisma);

            Assert.AreEqual(abilities.StrengthModifier, dto.StrengthModifier);
            Assert.AreEqual(abilities.DexterityModifier, dto.DexterityModifier);
            Assert.AreEqual(abilities.ConstitutionModifier, dto.ConstitutionModifier);
            Assert.AreEqual(abilities.IntelligenceModifier, dto.IntelligenceModifier);
            Assert.AreEqual(abilities.WisdomModifier, dto.WisdomModifier);
            Assert.AreEqual(abilities.CharismaModifier, dto.CharismaModifier);
        }

        [TestMethod]
        public void MapToDTO_Character_ReturnsCorrectDTO()
        {
            var character = new Character("Hero", 1)
            {
                Id = 101,
                Level = 5,
                Race = "Human",
                Class = "Fighter",
                Speed = 30,
                ArmorClass = 15,
                Skills = new List<string> { "Athletics", "Perception" },
                SavingThrows = new List<string> { "Strength", "Constitution" },
                OtherProficiencies = new List<string> { "Simple Weapons" }
            };

            var dto = character.MapToDTO();

            Assert.AreEqual(101, dto.Id);
            Assert.AreEqual(1, dto.OwnerId);
            Assert.AreEqual("Hero", dto.Name);
            Assert.AreEqual(5, dto.Level);
            Assert.AreEqual("Human", dto.Race);
            Assert.AreEqual("Fighter", dto.Class);
            Assert.AreEqual(30, dto.Speed);
            Assert.AreEqual(15, dto.ArmorClass);
            CollectionAssert.AreEqual(new List<string> { "Athletics", "Perception" }, dto.Skills);
            CollectionAssert.AreEqual(new List<string> { "Strength", "Constitution" }, dto.SavingThrows);
            CollectionAssert.AreEqual(new List<string> { "Simple Weapons" }, dto.OtherProficiencies);

            Assert.IsNull(dto.Spellcasting);
        }
        [TestMethod]
        public void MapToDTO_Note_ReturnsCorrectDTO()
        {
            var timeOfCreation = DateTime.Now;
            var timeOfUpdate = DateTime.Now.AddHours(1);
            var note = new Note
            {
                Id = 5,
                TimeOfCreation = timeOfCreation,
                TimeOfUpdate = timeOfUpdate,
                Title = "Session 1",
                Text = "Notes from session 1",
                CampaignId = 10
            };

            var dto = note.MapToDTO();

            Assert.AreEqual(5, dto.Id);
            Assert.AreEqual(timeOfCreation, dto.TimeOfCreation);
            Assert.AreEqual(timeOfUpdate, dto.TimeOfUpdate);
            Assert.AreEqual("Session 1", dto.Title);
            Assert.AreEqual("Notes from session 1", dto.Text);
            Assert.AreEqual(10, dto.CampaignId);
        }

        [TestMethod]
        public void MapToDTO_Health_ReturnsCorrectDTO()
        {
            var health = new Health
            {
                HitPoints = 20,
                MaxHitPoints = 25,
                HitDice = "2d6",
                MaxHitDice = "3d6"
            };

            var dto = health.MapToDTO();

            Assert.AreEqual(20, dto.HitPoints);
            Assert.AreEqual(25, dto.MaxHitPoints);
            Assert.AreEqual("2d6", dto.HitDice);
            Assert.AreEqual("3d6", dto.MaxHitDice);
        }

        [TestMethod]
        public void MapToDTO_Inventory_ReturnsCorrectDTO()
        {
            var inventory = new Inventory
            {
                Gold = 100,
                EquippedItems = new List<string> { "Sword", "Shield" },
                OtherItems = new List<string> { "Potion" }
            };

            var dto = inventory.MapToDTO();

            Assert.AreEqual(100, dto.Gold);
            CollectionAssert.AreEqual(new List<string> { "Sword", "Shield" }, dto.EquippedItems);
            CollectionAssert.AreEqual(new List<string> { "Potion" }, dto.OtherItems);
        }

        [TestMethod]
        public void MapToDTO_Spellcasting_ReturnsCorrectDTO()
        {
            var spellcasting = new Spellcasting
            {
                SpellcastingAbility = "Intelligence",
                SpellSlots = new List<int> { 4, 3, 2 },
                MaxSpellSlots = new List<int> { 4, 3, 2 },
                KnownSpells = new List<Spell>
                    {
                        new Spell { Name = "Fireball", Level = 3, Range = "150 feet" }
                    }
            };

            var dto = spellcasting.MapToDTO();

            Assert.AreEqual("Intelligence", dto.SpellcastingAbility);
            CollectionAssert.AreEqual(new List<int> { 4, 3, 2 }, dto.SpellSlots);
            CollectionAssert.AreEqual(new List<int> { 4, 3, 2 }, dto.MaxSpellSlots);
            Assert.HasCount(1, dto.KnownSpells);
            Assert.AreEqual("Fireball", dto.KnownSpells.First().Name);
        }

        [TestMethod]
        public void MapToDTO_Spell_ReturnsCorrectDTO()
        {
            var spell = new Spell
            {
                Id = 1,
                Name = "Magic Missile",
                Level = 1,
                Range = "120 feet",
                Components = "V, SM",
                Description = "A magical missile"
            };

            var dto = spell.MapToDTO();

            Assert.AreEqual(1, dto.Id);
            Assert.AreEqual("Magic Missile", dto.Name);
            Assert.AreEqual(1, dto.Level);
            Assert.AreEqual("120 feet", dto.Range);
            Assert.AreEqual("V, SM", dto.Components);
            Assert.AreEqual("A magical missile", dto.Description);
        }

        [TestMethod]
        public void MapToD_Character_ReturnsCorrectCharacterListItemDTO()
        {
            var character = new Character("Hero", 1)
            {
                Id = 101,
                Level = 5,
                Race = "Human",
                Class = "Fighter"
            };

            var dto = character.MapToListItemDTO();

            Assert.AreEqual(101, dto.Id);
            Assert.AreEqual(1, dto.OwnerId);
            Assert.AreEqual("Hero", dto.Name);
            Assert.AreEqual(5, dto.Level);
            Assert.AreEqual("Fighter", dto.Class);
            Assert.AreEqual("Human", dto.Race);
        }
    }
}