using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Repo
{
    /// <summary>
    /// Represents a character's ability to cast spells
    /// </summary>
    [PrimaryKey(nameof(CharacterId))]
    public class Spellcasting
    {
        public int CharacterId { get; set; }
        public Character Character { get; set; } = null!;
        public string SpellcastingAbility { get; set { field = value.Validated(); } } = string.Empty;
        public List<int> SpellSlots { get; set; } = [];
        public List<int> MaxSpellSlots { get; set; } = [];
        public ICollection<Spell> KnownSpells { get; set; } = [];
    }
}
