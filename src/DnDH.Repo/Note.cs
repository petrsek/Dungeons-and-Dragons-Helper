using System;
using System.Collections.Generic;
using System.Text;

namespace DnDH.Repo
{
    /// <summary>
    /// Represents a dungeon master's note associated with a campaign
    /// </summary>
    public class Note
    {
        public int Id { get; set; }
        public DateTime TimeOfCreation {  get; set; }
        public DateTime TimeOfUpdate { get; set; }
        public string Title { get; set { field = value.Validated(); } } = string.Empty;
        public string Text { get; set { field = value.Validated(); } } = string.Empty;
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; } = null!;

        /// <summary>
        /// Constructor for EF. Do not use.
        /// </summary>
        public Note() { }

        /// <summary>
        /// Initializes a new instance of the Note class with a specified title. The time of creation and update are set to the current time
        /// </summary>
        /// <param name="title"></param>
        /// <param name="campaignId"></param>
        public Note(string title, int campaignId)
        {
            this.Title = title; 
            this.CampaignId = campaignId;
            TimeOfCreation = DateTime.Now;
            TimeOfUpdate = DateTime.Now;
        }

        public void UpdateTimeOfUpdate()
        {
            TimeOfUpdate = DateTime.Now;
        }
    }
}
