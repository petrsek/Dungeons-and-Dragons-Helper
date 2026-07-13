using System;

namespace DnDH.Business.DTOs
{
    /// <summary>
    /// Used to transfer data about a campaign note
    /// </summary>
    public class NoteDTO
    {
        public int Id { get; set; }
        public DateTime TimeOfCreation { get; set; }
        public DateTime TimeOfUpdate { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int CampaignId { get; set; }
    }
}
