namespace DnDH.Business.DTOs
{
    /// <summary>
    /// Used to transfer inventory infromation for a character
    /// </summary>
    /// <param name="Gold"></param>
    /// <param name="EquippedItems"></param>
    /// <param name="OtherItems"></param>
    public record struct InventoryDTO(
        int Gold,
        List<string> EquippedItems,
        List<string> OtherItems);
}
