using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Repo
{
    /// <summary>
    /// Represents a spell that can be cast by spellcasters
    /// </summary>
    public class Spell
    {
        public int Id { get; set; }
        public string Name { get; set { field = value.Validated(); } } = string.Empty;
        public int Level { get; set; }
        public string Range { get; set { field = value.Validated(); } } = string.Empty;
        public string Components { get; set { field = value.Validated(); } } = string.Empty;
        public string Description { get; set { field = value.Validated(); } } = string.Empty;
        public ICollection<Spellcasting> CharacterSpellcasting { get; set; } = [];
    }
}
