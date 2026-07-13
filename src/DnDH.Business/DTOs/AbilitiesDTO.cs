namespace DnDH.Business.DTOs
{
    /// <summary>
    /// Used to transfer ability data for a character
    /// </summary>
    /// <param name="Strength"></param>
    /// <param name="Dexterity"></param>
    /// <param name="Constitution"></param>
    /// <param name="Intelligence"></param>
    /// <param name="Wisdom"></param>
    /// <param name="Charisma"></param>
    /// <param name="StrengthModifier"></param>
    /// <param name="DexterityModifier"></param>
    /// <param name="ConstitutionModifier"></param>
    /// <param name="IntelligenceModifier"></param>
    /// <param name="WisdomModifier"></param>
    /// <param name="CharismaModifier"></param>
    public record struct AbilitiesDTO(
        int Strength,
        int Dexterity,
        int Constitution,
        int Intelligence,
        int Wisdom,
        int Charisma,
        int StrengthModifier,
        int DexterityModifier,
        int ConstitutionModifier,
        int IntelligenceModifier,
        int WisdomModifier,
        int CharismaModifier);
}
