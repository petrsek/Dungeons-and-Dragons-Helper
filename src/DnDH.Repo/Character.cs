using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Repo
{
    /// <summary>
    /// Represents a player character or an npc in Dungeons a Dragons
    /// </summary>
    /// <remarks>
    /// The model is simplified in current version and omits details non-essential for gameplay.
    /// </remarks>
    public class Character
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; } = null!;
        public string Name { get; set { field = value.Validated(); } }
        public int Level { get; set; } = 1;
        public string Race { get; set { field = value.Validated(); } } = string.Empty;
        public string Class { get; set { field = value.Validated(); } } = string.Empty;
        public int Speed { get; set; } = 0;
        public int ArmorClass { get; set; } = 0;
        public List<string> Skills { get; set; } = [];
        public List<string> SavingThrows { get; set; } = [];
        public List<string> OtherProficiencies { get; set; } = [];
        public ICollection<CharacterStateInCampaign> CharacterStatesInCampaigns { get; set; } = [];
        public Health Health { get; set; } = null!;
        public Inventory Inventory { get; set; } = null!;   
        public Abilities Abilities { get; set; } = null!;
        public Spellcasting? Spellcasting { get; set; } = null;

        /// <summary>
        /// Initialises a new instance of the Character class with the specified name and default values for other properties.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ownerId"></param>
        public Character(string name, int ownerId)
        {
            Name = name;
            OwnerId = ownerId;
            Health = new();
            Inventory = new();
            Abilities = new();
        }
    }
}
