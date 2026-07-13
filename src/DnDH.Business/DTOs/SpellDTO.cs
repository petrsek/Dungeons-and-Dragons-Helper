namespace DnDH.Business.DTOs
{
    /// <summary>
    /// Used to transfer information about a spell
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Level"></param>
    /// <param name="Range"></param>
    /// <param name="Components"></param>
    /// <param name="Description"></param>
    public record struct SpellDTO(
        int Id,
        string Name,
        int Level,
        string Range,
        string Components,
        string Description);
}
