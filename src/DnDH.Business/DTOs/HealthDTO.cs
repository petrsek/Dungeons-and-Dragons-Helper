namespace DnDH.Business.DTOs
{
    /// <summary>
    /// Used to transfer health information of a character
    /// </summary>
    /// <param name="HitPoints"></param>
    /// <param name="MaxHitPoints"></param>
    /// <param name="HitDice"></param>
    /// <param name="MaxHitDice"></param>
    public record struct HealthDTO(
        int HitPoints,
        int MaxHitPoints,
        string HitDice,
        string MaxHitDice);
}
