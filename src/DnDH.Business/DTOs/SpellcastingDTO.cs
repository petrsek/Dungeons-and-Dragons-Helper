namespace DnDH.Business.DTOs
{
    /// <summary>
    /// Used to transfer spellcasting information for a character
    /// </summary>
    public class SpellcastingDTO
    {
        public string SpellcastingAbility { get; set; } = string.Empty;
        public List<int> SpellSlots { get; set; } = [];
        public List<int> MaxSpellSlots { get; set; } = [];
        public List<SpellDTO> KnownSpells { get; set; } = [];
    }
}
