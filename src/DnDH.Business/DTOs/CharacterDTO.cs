using System.Collections.Generic;

namespace DnDH.Business.DTOs
{
    /// <summary>
    /// Used for transferring character data between layers of the application
    /// </summary>
    public class CharacterDTO
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; } = 1;
        public string Race { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public int Speed { get; set; }
        public int ArmorClass { get; set; }
        public List<string> Skills { get; set; } = [];
        public List<string> SavingThrows { get; set; } = [];
        public List<string> OtherProficiencies { get; set; } = [];

        public HealthDTO Health { get; set; }
        public InventoryDTO Inventory { get; set; }
        public AbilitiesDTO Abilities { get; set; }
        public SpellcastingDTO? Spellcasting { get; set; }
    }
}
